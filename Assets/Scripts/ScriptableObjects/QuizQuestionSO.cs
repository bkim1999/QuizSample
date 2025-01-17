using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

[CreateAssetMenu(fileName = "QuizQuestion_Data", menuName = "Quiz/QuizQuestionData", order = 1)]
public class QuizQuestionSO : ScriptableObject
{
    [SerializeField] string m_Question;
    [TextArea(3, 10)]
    [SerializeField] string[] m_Choices;
    [SerializeField] int m_AnswerIndex;

    public string Question => m_Question;
    public string[] Choices => m_Choices;
    public int AnswerIndex => m_AnswerIndex;
}