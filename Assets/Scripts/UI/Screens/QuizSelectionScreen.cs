using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizSelectionScreen : UIScreen
{
    const string k_SelectButtonContainer = "select_button-container";
    const string k_SelectDescription = "select_description";
    const string k_SelectGraphic = "select_graphic";
    const string k_BackButton = "select_button-back";

    VisualElement m_SelectButtonContainer;
    Label m_SelectDescription;
    Label m_SelectGraphic;
    Button m_BackButton;

    public Button BackButton => m_BackButton;

    public QuizSelectionScreen(VisualElement rootElement) : base(rootElement)
    {
        QuizSelectionEvents.QuizButtonsInserted += QuizSelectionEvents_QuizButtonsInserted;
        QuizSelectionEvents.QuizDataLoaded += QuizSelectionEvents_QuizDataLoaded;
        QuizSelectionEvents.QuizSelected += QuizSelectionEvents_QuizSelected;

        SetVisualElements();
        CleanView();
    }

    // event handlers
    private void QuizSelectionEvents_QuizButtonsInserted(QuizSO[] quizArray)
    {
        InsertQuizButton(quizArray);
    }

    private void QuizSelectionEvents_QuizDataLoaded(QuizSO quiz)
    {
        LoadQuizData(quiz.Description, quiz.Graphic);
    }

    private void QuizSelectionEvents_QuizSelected(QuizSO quiz)
    {
        UIEvents.QuizPlayShown?.Invoke();
        QuizPlayEvents.QuizStarted?.Invoke(quiz);
    }

    // methods
    private void SetVisualElements()
    {
        m_SelectButtonContainer = m_RootElement.Q<VisualElement>(k_SelectButtonContainer);
        m_SelectDescription = m_RootElement.Q<Label>(k_SelectDescription);
        m_SelectGraphic = m_RootElement.Q<Label>(k_SelectGraphic);
        m_BackButton = m_RootElement.Q<Button>(k_BackButton);
    }

    private void CleanView()
    {
        m_SelectButtonContainer.Clear();
        m_SelectDescription.text = string.Empty;
        m_SelectGraphic.style.backgroundImage = new StyleBackground();
    }

    private void InsertQuizButton(QuizSO[] quizArray)
    {
        for(int i = 0; i < quizArray.Length; i++)
        {
            Button quizButton = new Button() { text = quizArray[i].Title };
            quizButton.userData = i;
            quizButton.AddToClassList("select-button");
            m_SelectButtonContainer.Add(quizButton);
        }
    }

    private void LoadQuizData(string description, Sprite graphic)
    {
        if (description != null)
        {
            m_SelectDescription.text = description;
        }
        if (graphic != null)
        {
            m_SelectGraphic.style.backgroundImage = new StyleBackground(graphic.texture);
        }
    }
}