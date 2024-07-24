using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "MenuButton_Data", menuName = "Quiz/MenuButtonData", order = 1)]
public abstract class MainMenuSO : ScriptableObject
{
    [SerializeField] string m_ElementID;
    [TextArea(3, 10)] string m_Description;
    Button m_MenuButton;

    public string ElementID => m_ElementID;
    public string Description => m_Description;
    public Button MenuButton { get => m_MenuButton; set => m_MenuButton = value;}
}