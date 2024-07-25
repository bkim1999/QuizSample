using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizSelectionPresenter : MonoBehaviour
{
    QuizSelectionScreen m_QuizSelectionScreen;
    QuizSO[] m_QuizData;


    public QuizSelectionPresenter(QuizSelectionScreen quizSelectionScreen)
    {
        m_QuizSelectionScreen = quizSelectionScreen;
        InsertQuizButtons();
        RegisterCallBacks();
    }

    private void InsertQuizButtons()
    {
        m_QuizData = Resources.LoadAll<QuizSO>("QuizData");
        QuizSelectionEvents.QuizButtonsInserted?.Invoke(m_QuizData);
    }

    private void RegisterCallBacks()
    {
        foreach (QuizSO quiz in m_QuizData)
        {
            quiz.QuizButton.RegisterCallback<ClickEvent>(evt => QuizSelectionEvents.QuizSelected.Invoke(quiz));
            quiz.QuizButton.RegisterCallback<MouseEnterEvent>(evt => MouseEnterQuizHandler(evt.target as Button));
        }
        m_QuizSelectionScreen.BackButton.RegisterCallback<ClickEvent>(evt => UIEvents.BackButtonClicked?.Invoke());
    }

    private void MouseEnterQuizHandler(Button quizButton)
    {
        QuizSO quiz = m_QuizData[(int) quizButton.userData];
        QuizSelectionEvents.QuizDataLoaded?.Invoke(quiz);
    }
}
