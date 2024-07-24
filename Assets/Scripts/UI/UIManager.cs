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
    List<UIScreen> m_UIScreenList;

    void OnEnable()
    {
        SubcribeEvents();
        SetUIScreens();
    }

    private void RegisterUIScreens()
    {
        m_UIScreenList = new List<UIScreen>{
            m_MainMenuScreen,
            m_QuizSelectionScreen
        };
    }
    private void SetUIScreens()
    {
        VisualElement root = m_UIDocument.rootVisualElement;
        m_MainMenuScreen = new MainMenuScreen(root.Q<VisualElement>("menu_container"));
        m_QuizSelectionScreen = new QuizSelectionScreen(root.Q<VisualElement>("select_container"));
       
        RegisterUIScreens();
        HideAllScreens();
        SetCurrentScreen(m_MainMenuScreen);
    }

    private void SubcribeEvents()
    {
        UIEvents.QuizSelectionShown += UIEvents_QuizSelectionShown;
    }

    private void HideAllScreens()
    {
        foreach(UIScreen screen in m_UIScreenList)
        {
            screen.Hide();
        }
    }

    private void SetCurrentScreen(UIScreen newCurrentScreen)
    {
        if(m_CurrentScreen != null)
        {
            m_CurrentScreen.Hide();
        }
        m_CurrentScreen = newCurrentScreen;
        m_CurrentScreen.Show();
        Debug.Log("UIManager: CurrentScreen Set");
    }

    private void UIEvents_QuizSelectionShown()
    {
        SetCurrentScreen(m_QuizSelectionScreen);
    }
}
