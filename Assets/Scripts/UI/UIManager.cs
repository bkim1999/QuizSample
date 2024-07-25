using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField]UIDocument m_UIDocument;
    UIScreen m_CurrentScreen;
    UIScreen m_MainMenuScreen;
    // UIScreen m_SettingsScreen;
    UIScreen m_QuizSelectionScreen;
    UIScreen m_QuizPlayScreen;
    List<UIScreen> m_UIScreenList;
    Stack<UIScreen> m_UIScreenStack;

    void OnEnable()
    {
        m_UIScreenStack = new Stack<UIScreen>();

        SetUIScreens();
        RegisterUIScreens();
        HideAllScreens();
        SubcribeEvents();

        SetCurrentScreen(m_MainMenuScreen);
        m_UIScreenStack.Push(m_MainMenuScreen);
    }

    private void SetUIScreens()
    {
        VisualElement root = m_UIDocument.rootVisualElement;
        m_MainMenuScreen = new MainMenuScreen(root.Q<VisualElement>("menu_container"));
        m_QuizSelectionScreen = new QuizSelectionScreen(root.Q<VisualElement>("select_container"));
        m_QuizPlayScreen = new QuizPlayScreen(root.Q<VisualElement>("quiz_container"));
    }

    private void RegisterUIScreens()
    {
        m_UIScreenList = new List<UIScreen>{
            m_MainMenuScreen,
            m_QuizSelectionScreen,
            m_QuizPlayScreen
        };
    }

    private void SubcribeEvents()
    {
        UIEvents.MainMenuShown += UIEvents_MainMenuShown;
        UIEvents.QuizSelectionShown += UIEvents_QuizSelectionShown;
        UIEvents.QuizPlayShown += UIEvents_QuizPlayShown;
        UIEvents.BackButtonClicked += UIEvents_BackButtonClicked;
    }

    private void HideAllScreens()
    {
        foreach(UIScreen screen in m_UIScreenList)
        {
            screen.Hide();
        }
    }

    private void SetCurrentScreen(UIScreen newCurrentScreen, bool saveHistory = true)
    {
        if(m_CurrentScreen != null)
        {
            if(saveHistory) { m_UIScreenStack.Push(m_CurrentScreen); }
            m_CurrentScreen.Hide();
        }
        m_CurrentScreen = newCurrentScreen;
        m_CurrentScreen.Show();
        Debug.Log("UIManager: CurrentScreen Set");
    }

    private void UIEvents_MainMenuShown()
    {
        SetCurrentScreen(m_CurrentScreen, false);
    }

    private void UIEvents_QuizSelectionShown()
    {
        SetCurrentScreen(m_QuizSelectionScreen);
    }

    private void UIEvents_QuizPlayShown()
    {
        SetCurrentScreen(m_QuizPlayScreen);
    }

    private void UIEvents_BackButtonClicked()
    {
        SetCurrentScreen(m_UIScreenStack.Pop());
    }
}
