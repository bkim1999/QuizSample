using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "QuizQuestion_Data", menuName = "Quiz/QuizQuestionData", order = 1)]
public class MainMenuSO : ScriptableObject
{
    [SerializeField] string m_Question;
    [TextArea(3, 10)]
    [SerializeField] string[] m_Answers;

    Button m_MenuButton;

    public string Question => m_ElementID;
    public string Description => m_Description;
   // public Button MenuButton { get => m_MenuButton; set => m_MenuButton = value;}
}