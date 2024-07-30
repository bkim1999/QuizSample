using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizPlayScreen : UIScreen
{
    const string k_QuitButton = "quiz_button-quit";
    const string k_ProgressBar = "quiz_progress-bar";
    const string k_RemainingLifeCount = "quiz_remaining-life-count";
    const string k_QuestionNumber = "quiz_question-number";
    const string k_QuestionText = "quiz_question-text";
    const string k_ChoiceContainer = "quiz_choice-container";
    const string k_ContinueButton = "quiz_button-continue";

    Button m_QuitButton;
    ProgressBar m_Progressbar;
    Label m_RemainingLifeCount;
    Label m_QuestionNumber;
    Label m_QuestionText;
    VisualElement m_ChoiceContainer;
    List<Button> m_ChoiceButtons;
    Button m_ContinueButton;

    public List<Button> ChoiceButtons => m_ChoiceButtons;

    public QuizPlayScreen(VisualElement rootElement) : base(rootElement)
    {
        SetVisualElements();
        RegisterCallBacks();
        QuizPlayEvents.QuestionUpdated += QuizPlayEvents_QuestionUpdated;
        QuizPlayEvents.QuizProgressUpdated += QuizPlayEvents_QuizProgressUpdated;
        QuizPlayEvents.LifeCountUpdated += QuizPlayEvents_LifeCountUpdated;
        QuizPlayEvents.CorrectlyAnswered += QuizPlayEvents_CorrectlyAnswered;
        QuizPlayEvents.IncorrectlyAnswered += QuizPlayEvents_IncorrectlyAnswered;
    }

    // event handlers

    private void QuizPlayEvents_QuestionUpdated(QuizQuestionSO quizQuestion)
    {
        SetQuestion(quizQuestion);
    }

    private void QuizPlayEvents_QuizProgressUpdated(int index, int questionCount)
    {
        SetQuestionProgress(index, questionCount);
    }

    private void QuizPlayEvents_LifeCountUpdated(int remainingLifeCount)
    {
        SetLifeCount(remainingLifeCount);
    }

    private void QuizPlayEvents_CorrectlyAnswered(int correctChoice)
    {
        m_ContinueButton.SetEnabled(true);
        DisableChoiceButtons();
        HighlightCorrectChoice(correctChoice);
    }

    private void QuizPlayEvents_IncorrectlyAnswered(int incorrectChoice, int correctChoice)
    {
        m_ContinueButton.SetEnabled(true);
        DisableChoiceButtons();
        HighlightIncorrectChoice(incorrectChoice);
        HighlightCorrectChoice(correctChoice);
    }

    // methods
    private void SetVisualElements()
    {
        m_QuitButton = m_RootElement.Q<Button>(k_QuitButton);
        m_Progressbar = m_RootElement.Q<ProgressBar>(k_ProgressBar);
        m_RemainingLifeCount = m_RootElement.Q<Label>(k_RemainingLifeCount);
        m_QuestionNumber = m_RootElement.Q<Label>(k_QuestionNumber);
        m_QuestionText = m_RootElement.Q<Label>(k_QuestionText);
        m_ChoiceContainer = m_RootElement.Q<VisualElement>(k_ChoiceContainer);
        m_ContinueButton = m_RootElement.Q<Button>(k_ContinueButton);
        m_ContinueButton.SetEnabled(false);
    }

    private void RegisterCallBacks()
    {
        m_QuitButton.RegisterCallback<ClickEvent>(evt => UIEvents.DialogueShown?.Invoke());
        m_ContinueButton.RegisterCallback<ClickEvent>(evt => QuizPlayEvents.QuizContinued?.Invoke());
    }

    private void SetQuestion(QuizQuestionSO quizQuestion)
    {
        m_ChoiceButtons = new List<Button>();
        m_QuestionText.text = quizQuestion.Question;
        m_ChoiceContainer.Clear();
        string[] choices = quizQuestion.Choices;
        for (int i = 0; i < choices.Length; i++)
        {
            Button choiceButton = new Button() { text = choices[i] };
            choiceButton.AddToClassList("quiz-choice-button");
            choiceButton.userData = i;
            m_ChoiceContainer.Add(choiceButton);
            m_ChoiceButtons.Add(choiceButton);
            choiceButton.RegisterCallback<ClickEvent>(evt => QuizPlayEvents.ChoiceSelected?.Invoke((int)(evt.target as Button).userData));
        }
    }

    private void SetQuestionProgress(int index, int questionCount)
    {
        m_QuestionNumber.text = $"Question {index + 1} of {questionCount}";
        m_Progressbar.value = (float)(index) / (float) questionCount * 100;
        Debug.Log(m_Progressbar.value);
    }

    private void SetLifeCount(int remainingLifeCount)
    {
        m_RemainingLifeCount.text = remainingLifeCount.ToString();
    }

    private void DisableChoiceButtons()
    {   
        for(int i = 0; i < m_ChoiceButtons.Count; i++)
        {
            m_ChoiceButtons[i].SetEnabled(false);
        }
    }

    private void HighlightCorrectChoice(int index)
    {
        m_ChoiceButtons[index].AddToClassList("correct-choice-button");
    }

    private void HighlightIncorrectChoice(int index)
    {
        m_ChoiceButtons[index].AddToClassList("incorrect-choice-button");
    }

}
