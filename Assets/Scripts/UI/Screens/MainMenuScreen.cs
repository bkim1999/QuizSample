using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : UIScreen
{
    Button m_PlayButton;
    Button m_SettingsButton;
    Button m_MoreButton;
    Button m_BackButton;
    VisualElement m_ButtonContainer1;
    VisualElement m_ButtonContainer2;
    Label m_Description;
    MainMenuSO[] m_MainMenuData;

    public MainMenuScreen(VisualElement rootElement) : base(rootElement)
    {
        SetVisualElements();
        RegisterCallBacks();
        SetButtonContainer(1);
    }

    private void SetVisualElements()
    {
        m_PlayButton = m_RootElement.Q<Button>("menu_button-play");
        m_SettingsButton = m_RootElement.Q<Button>("menu_button-settings");
        m_MoreButton = m_RootElement.Q<Button>("menu_button-more");
        m_BackButton = m_RootElement.Q<Button>("menu_button-back");
        m_ButtonContainer1 = m_RootElement.Q<VisualElement>("menu_button-container--1");
        m_ButtonContainer2 = m_RootElement.Q<VisualElement>("menu_button-container--2");
        
        m_MainMenuData = Resources.LoadAll<MainMenuSO>("MainMenuButtonData");
        for(int i = 0; i < m_MainMenuData.Length; i++)
        {
            m_MainMenuData[i].MenuButton = m_RootElement.Q<Button>(m_MainMenuData[i].ElementID);
            m_MainMenuData[i].MenuButton.userData = m_MainMenuData[i].Description;
        }
    }

    private void RegisterCallBacks()
    {
        m_PlayButton.RegisterCallback<ClickEvent>(evt => UIEvents.QuizSelectionShown?.Invoke());
        m_SettingsButton.RegisterCallback<ClickEvent>(evt => UIEvents.SettingsShown?.Invoke());
        m_MoreButton.RegisterCallback<ClickEvent>(evt => SetButtonContainer(2));
        m_BackButton.RegisterCallback<ClickEvent>(evt => SetButtonContainer(1));

        for(int i = 0; i < m_MainMenuData.Length; i++)
        {
            m_MainMenuData[i].MenuButton.RegisterCallback<MouseEnterEvent>(evt => MenuMouseEnterHandler(evt.target as Button));
            m_MainMenuData[i].MenuButton.RegisterCallback<MouseLeaveEvent>(evt => MenuMouseLeaveHandler());
        }
    }

    private void SetButtonContainer(int index)
    {
        if(index == 1)
        {
            m_ButtonContainer1.style.display = DisplayStyle.Flex;
            m_ButtonContainer2.style.display = DisplayStyle.None;
        }
        else if(index == 2)
        {
            m_ButtonContainer1.style.display = DisplayStyle.None;
            m_ButtonContainer2.style.display = DisplayStyle.Flex;
        }
    }

    private void MenuMouseEnterHandler(Button menuButton)
    {
        m_Description.text = (string)menuButton.userData;
    }

    private void MenuMouseLeaveHandler()
    {
        m_Description.text = string.Empty;
    }


}
