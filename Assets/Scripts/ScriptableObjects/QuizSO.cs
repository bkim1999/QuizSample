using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Quiz_Data", menuName = "Quiz/QuizData", order = 1)]
public class QuizSO : ScriptableObject
{
    [SerializeField] string m_Title;
    [SerializeField] QuizQuestionSO[] m_QuizQuestions;
    [SerializeField] int m_LifeCount;
    [SerializeField] string m_Description;
    [SerializeField] Sprite m_Graphic;

    Button m_QuizButton;

    public string Title => m_Title;
    public QuizQuestionSO[] QuizQuestionSOs => m_QuizQuestions;
    public int LifeCount => m_LifeCount;
    public string Description => m_Description;
    public Sprite Graphic => m_Graphic;
    public Button QuizButton { get => m_QuizButton; set => m_QuizButton = value; }
}
