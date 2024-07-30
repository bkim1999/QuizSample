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
    UIScreen m_QuizResultScreen;
    UIScreen m_DialogueScreen;
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
        m_QuizResultScreen = new QuizResultScreen(root.Q<VisualElement>("result_container"));
        m_DialogueScreen = new DialogueScreen(root.Q<VisualElement>("dialogue_container"));
    }

    private void RegisterUIScreens()
    {
        m_UIScreenList = new List<UIScreen>{
            m_MainMenuScreen,
            m_QuizSelectionScreen,
            m_QuizPlayScreen,
            m_QuizResultScreen,
            m_DialogueScreen
        };
    }

    private void SubcribeEvents()
    {
        UIEvents.MainMenuShown += UIEvents_MainMenuShown;
        UIEvents.QuizSelectionShown += UIEvents_QuizSelectionShown;
        UIEvents.QuizPlayShown += UIEvents_QuizPlayShown;
        UIEvents.BackButtonClicked += UIEvents_BackButtonClicked;
        UIEvents.QuizResultShown += UIEvents_QuizResultShown;
        UIEvents.DialogueShown += UIEvents_DialogueShown;
    }

    private void HideAllScreens()
    {
        foreach(UIScreen screen in m_UIScreenList)
        {
            screen.Hide();
        }
    }

    private void SetCurrentScreen(UIScreen newCurrentScreen, bool saveHistory = true, bool hideScreen = true)
    {
        if(m_CurrentScreen != null)
        {
            if (saveHistory) m_UIScreenStack.Push(m_CurrentScreen);
            if (hideScreen) HideAllScreens();
            Debug.Log("UIManager: current screen hidden");
        }
        m_CurrentScreen = newCurrentScreen;
        m_CurrentScreen.ShowWithTransition(0.2f, 0);
        Debug.Log("UIManager: CurrentScreen Set");
    }

    private void UIEvents_MainMenuShown()
    {
        m_UIScreenStack = new Stack<UIScreen>();
        SetCurrentScreen(m_MainMenuScreen);
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

    private void UIEvents_QuizResultShown()
    {
        SetCurrentScreen(m_QuizResultScreen);
    }

    private void UIEvents_DialogueShown()
    {
        SetCurrentScreen(m_DialogueScreen, hideScreen: false);
    }
}
