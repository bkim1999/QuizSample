using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizResultScreen : UIScreen
{ 
    const string k_ResultTitle = "result_title";
    const string k_ResultGraphic = "result_graphic";
    const string k_ReusltText = "result_text";
    const string k_ResultScore = "result_score";
    const string k_ResultMainButton = "result_button-main";

    VisualElement m_RootElement;
    Label m_ResultTitle;
    Label m_ResultGraphic;
    Label m_ResultText;
    Label m_ResultScore;
    Button m_ResultMainButton;

    public QuizResultScreen(VisualElement rootElement) : base(rootElement)
    {
        m_RootElement = rootElement;
        SetVisualElements(); 
        RegisterCallbacks();

        QuizPlayEvents.QuizPassed += QuizPlayEvents_QuizPassed;
        QuizPlayEvents.QuizFailed += QuizPlayEvents_QuizFailed;
        QuizPlayEvents.QuizEnded += QuizPlayEvents_QuizEnded;
    }

    // event handlers
    private void QuizPlayEvents_QuizPassed(string score)
    {
        SetQuizScore(score, true);
    }

    private void QuizPlayEvents_QuizFailed(string score)
    {
        SetQuizScore(score, false);
    }

    private void QuizPlayEvents_QuizEnded(QuizSO quiz)
    {
        SetQuizInfo(quiz.Title, quiz.Graphic);
    }

    private void SetVisualElements()
    {
        m_ResultTitle = m_RootElement.Q<Label>(k_ResultTitle);
        m_ResultGraphic = m_RootElement.Q<Label>(k_ResultGraphic);
        m_ResultText = m_RootElement.Q<Label>(k_ReusltText);
        m_ResultScore = m_RootElement.Q<Label>(k_ResultScore);
        m_ResultMainButton = m_RootElement.Q<Button>(k_ResultMainButton);
    }

    private void RegisterCallbacks()
    {
        m_ResultMainButton.RegisterCallback<ClickEvent>(evt => UIEvents.MainMenuShown?.Invoke());
    }

    private void SetQuizScore(string score, bool passed)
    {
        m_ResultScore.text = score;
        if (passed)
        {
            m_ResultText.AddToClassList("quiz-passed");
            m_ResultText.text = "Quiz Passed";
        }
        else
        {
            m_ResultText.AddToClassList("quiz-failed");
            m_ResultText.text = "Quiz Failed";
        }
    }

    private void SetQuizInfo(string title, Sprite graphic)
    {
        m_ResultTitle.text = title;
        m_ResultGraphic.style.backgroundImage = new StyleBackground(graphic.texture);
    }

}
