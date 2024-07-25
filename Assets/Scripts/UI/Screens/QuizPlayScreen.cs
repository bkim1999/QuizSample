using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizPlayScreen : UIScreen
{
    const string k_QuitButton = "quiz_button-quit";
    const string k_ProgressBar = "quiz_progress-bar";
    const string k_RemainingLifeCount = "quiz_remaining-life-count";
    const string k_QuestionNumber = "quiz_question-number";
    const string k_QuestionText = "quiz_question-text";

    Button m_QuitButton;
    ProgressBar m_Progressbar;
    Label m_RemainingLifeCount;
    Label m_QuestionNumber;
    Label m_QuestionText;

    public QuizPlayScreen(VisualElement rootElement) : base(rootElement)
    {
        SetVisualElements();
        RegisterCallBacks();
        SetCurrentQuestion(0);
    }

    private void SetVisualElements()
    {
        m_QuitButton = m_RootElement.Q<Button>(k_QuestionText);
        m_Progressbar = m_RootElement.Q<ProgressBar>(k_ProgressBar);
        m_RemainingLifeCount = m_RootElement.Q<Label>(k_RemainingLifeCount);
        m_QuestionNumber = m_RootElement.Q<Label>(k_QuestionText);
        m_QuestionText = m_RootElement.Q<Label>(k_QuestionText);
    }

    private void RegisterCallBacks()
    {
        (evt.target as Button).userData as QuizSO);
    }

    private void SetCurrentQuestion(int index)
    {
        if(index >= 0 && index <m_QuestionCount)
        {

        }
    }

}
