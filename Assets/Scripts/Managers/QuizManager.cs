using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    QuizSO m_QuizData;
    QuizSelectionPresenter m_QuizSelectionPresenter;

    QuizQuestionSO m_CurrentQuizQuestion;
    int m_CurrentQuizQuestionIndex;
    int m_RemainingLifeCount;
    int m_TotalQuestionCount;
    int m_CorrectAnswerCount;


    private void OnEnable()
    {
        QuizPlayEvents.QuizStarted += QuizPlayEvents_QuizStarted;
        QuizPlayEvents.QuizPaused += QuizPlayEvents_QuizPaused;
        QuizPlayEvents.QuizContinued += QuizPlayEvents_QuizContinued;
        QuizPlayEvents.ChoiceSelected += QuizPlayEvents_ChoiceSelected;

        QuizSelectionEvents.Initialized += QuizSelectionEvents_Initialized;
    }

    private void OnDisable()
    {
        QuizPlayEvents.QuizStarted -= QuizPlayEvents_QuizStarted;
        QuizPlayEvents.QuizPaused -= QuizPlayEvents_QuizPaused;
        QuizPlayEvents.QuizContinued -= QuizPlayEvents_QuizContinued;
        QuizPlayEvents.ChoiceSelected -= QuizPlayEvents_ChoiceSelected;

        QuizSelectionEvents.Initialized -= QuizSelectionEvents_Initialized;
    }

    private void QuizPlayEvents_QuizStarted(QuizSO quiz)
    {
        m_QuizData = quiz;
        m_CurrentQuizQuestionIndex = 0;
        m_CurrentQuizQuestion = quiz.QuizQuestionSOs[m_CurrentQuizQuestionIndex];
        m_RemainingLifeCount = quiz.LifeCount;
        m_TotalQuestionCount = quiz.QuizQuestionSOs.Length;
        m_CorrectAnswerCount = 0;
        OnGameContinued();
    }

    private void QuizPlayEvents_QuizPaused()
    {

    }

    private void QuizPlayEvents_QuizContinued()
    { 
        m_CurrentQuizQuestionIndex++;
        if(m_CurrentQuizQuestionIndex >= m_TotalQuestionCount)
        {
            UIEvents.QuizResultShown?.Invoke();
            QuizPlayEvents.QuizEnded?.Invoke(m_QuizData);
            QuizPlayEvents.QuizPassed?.Invoke($"{m_CorrectAnswerCount} / {m_TotalQuestionCount}");
            return;
        }
        OnGameContinued();
    }

    private void QuizPlayEvents_ChoiceSelected(int choiceIndex)
    {
        if (choiceIndex == m_CurrentQuizQuestion.AnswerIndex)
        {
            QuizPlayEvents.CorrectlyAnswered?.Invoke(choiceIndex);
            m_CorrectAnswerCount++;
        }
        else
        {
            QuizPlayEvents.IncorrectlyAnswered?.Invoke(choiceIndex, m_CurrentQuizQuestion.AnswerIndex);
            m_RemainingLifeCount--;
            if (m_RemainingLifeCount <= 0)
            {
                Debug.Log("QuizManager: Life is zero or less than zero");
                QuizPlayEvents.QuizEnded?.Invoke(m_QuizData);
                QuizPlayEvents.QuizFailed?.Invoke($"{m_CorrectAnswerCount} / {m_TotalQuestionCount}");
                UIEvents.QuizResultShown?.Invoke();
            }
            else
            {
                QuizPlayEvents.LifeCountUpdated?.Invoke(m_RemainingLifeCount);
            }
        }
    }

    private void QuizSelectionEvents_Initialized(QuizSelectionScreen quizSelectionScreen)
    {
        m_QuizSelectionPresenter = new QuizSelectionPresenter(quizSelectionScreen);
    }

    private void QuizSelectionEvents_QuizSelected(int index)
    {
        UIEvents.QuizSelectionShown?.Invoke();
    }


    private void OnGameContinued()
    {
        m_CurrentQuizQuestion = m_QuizData.QuizQuestionSOs[m_CurrentQuizQuestionIndex];
        QuizPlayEvents.QuestionUpdated?.Invoke(m_CurrentQuizQuestion);
        QuizPlayEvents.QuizProgressUpdated?.Invoke(m_CurrentQuizQuestionIndex, m_TotalQuestionCount);
        QuizPlayEvents.LifeCountUpdated?.Invoke(m_RemainingLifeCount);
    }
}
