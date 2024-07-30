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
        if (m_QuizData == null)
        {
            Debug.LogError("QuizSelectionPresenter: QuizData is null.");
            return;
        }
        QuizSelectionEvents.QuizButtonsInserted?.Invoke(m_QuizData);
    }

    private void RegisterCallBacks()
    {
        List<Button> quizButtons = m_QuizSelectionScreen.QuizButtons;

        if (quizButtons == null || quizButtons.Count == 0)
        {
            Debug.LogError("QuizSelectionPresenter: QuizData is null or length is zero.");
            return;
        }

        foreach (Button quizButton in quizButtons)
        {
            quizButton.RegisterCallback<ClickEvent>(evt => QuizSelectionEvents.QuizSelected.Invoke(m_QuizData[(int) quizButton.userData]));
            quizButton.RegisterCallback<MouseEnterEvent>(evt => MouseEnterQuizHandler(evt.target as Button));
        }
        m_QuizSelectionScreen.BackButton.RegisterCallback<ClickEvent>(evt => UIEvents.BackButtonClicked?.Invoke());
    }

    private void MouseEnterQuizHandler(Button quizButton)
    {
        QuizSO quiz = m_QuizData[(int) quizButton.userData];
        QuizSelectionEvents.QuizDataLoaded?.Invoke(quiz);
    }
}
