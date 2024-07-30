using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIScreen
{
    protected VisualElement m_RootElement;

    public UIScreen(VisualElement parentElement)
    {
        m_RootElement = parentElement;
    }

    public void Show()
    {
        m_RootElement.parent.style.display = DisplayStyle.Flex;
    }

    public void ShowWithTransition(float duration = 0f, float delay = 0f)
    {
        m_RootElement.parent.style.transitionDuration = new List<TimeValue> { duration, new(delay, TimeUnit.Millisecond) };
        Show();
    }
    public void Hide()
    {
        m_RootElement.parent.style.display = DisplayStyle.None;
    }
    
}
