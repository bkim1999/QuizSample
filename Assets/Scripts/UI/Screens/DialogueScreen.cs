using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueScreen : UIScreen
{
    const string k_ContentsText = "dialogue_contents-text";
    const string k_CancelButton = "dialogue_button-cancel";
    const string k_ConfirmButton = "dialogue_button-confirm";

    Label m_ContentsText;
    Button m_CancelButton;
    Button m_ConfirmButton;

    public DialogueScreen(VisualElement rootElement) : base(rootElement)
    {
        m_RootElement = rootElement;
        SetVisualElements();
        RegisterCallbacks();
    }

    private void SetVisualElements()
    {
        m_ContentsText = m_RootElement.Q<Label>(k_ContentsText);
        m_CancelButton = m_RootElement.Q<Button>(k_CancelButton);
        m_ConfirmButton = m_RootElement.Q<Button>(k_ConfirmButton);
    }

    private void RegisterCallbacks()
    {
        m_CancelButton.RegisterCallback<ClickEvent>(evt => UIEvents.BackButtonClicked?.Invoke());
        m_ConfirmButton.RegisterCallback<ClickEvent>(evt => UIEvents.MainMenuShown?.Invoke());
    }
}
