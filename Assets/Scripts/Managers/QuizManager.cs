using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    QuizSO m_QuizData;
    QuizSelectionPresenter m_QuizSelectionPresenter;

    private void OnEnable()
    {
        QuizPlayEvents.QuizStarted += QuizPlayEvents_QuizStarted;
        QuizPlayEvents.QuizPaused += QuizPlayEvents_QuizPaused;
        QuizPlayEvents.QuizContinued += QuizPlayEvents_QuizContinued;
        QuizPlayEvents.AnswerSelected += QuizPlayEvents_AnswerSelected;
        QuizPlayEvents.CorrectlyAnswered += QuizPlayEvents_CorrectlyAnswered;
        QuizPlayEvents.IncorrectlyAnswered += QuizPlayEvents_IncorrectlyAnswered;
        QuizPlayEvents.QuizFailed += QuizPlayEvents_QuizFailed;
        QuizPlayEvents.QuizPassed += QuizPlayEvents_QuizPassed;

        QuizSelectionEvents.Initialized += QuizSelectionEvents_Initialized;
    }

    private void OnDisable()
    {
        QuizPlayEvents.QuizStarted -= QuizPlayEvents_QuizStarted;
        QuizPlayEvents.QuizPaused -= QuizPlayEvents_QuizPaused;
        QuizPlayEvents.QuizContinued -= QuizPlayEvents_QuizContinued;
        QuizPlayEvents.AnswerSelected -= QuizPlayEvents_AnswerSelected;
        QuizPlayEvents.CorrectlyAnswered -= QuizPlayEvents_CorrectlyAnswered;
        QuizPlayEvents.IncorrectlyAnswered -= QuizPlayEvents_IncorrectlyAnswered;
        QuizPlayEvents.QuizFailed -= QuizPlayEvents_QuizFailed;
        QuizPlayEvents.QuizPassed -= QuizPlayEvents_QuizPassed;

        QuizSelectionEvents.Initialized -= QuizSelectionEvents_Initialized;
    }

    private void QuizPlayEvents_QuizStarted(QuizSO quiz)
    {
        m_QuizData = quiz;
    }

    private void QuizPlayEvents_QuizPaused()
    {

    }

    private void QuizPlayEvents_QuizContinued()
    {

    }

    private void QuizPlayEvents_AnswerSelected(int answerIndex)
    {

    }

    private void QuizPlayEvents_CorrectlyAnswered()
    {

    }

    private void QuizPlayEvents_IncorrectlyAnswered()
    {

    }

    private void QuizPlayEvents_QuizFailed()
    {

    }

    private void QuizPlayEvents_QuizPassed()
    {

    }

    private void QuizSelectionEvents_Initialized(QuizSelectionScreen quizSelectionScreen)
    {
        m_QuizSelectionPresenter = new QuizSelectionPresenter(quizSelectionScreen);
    }

    private void QuizSelectionEvents_QuizSelected(int index)
    {
        UIEvents.QuizSelectionShown?.Invoke();
    }
}
