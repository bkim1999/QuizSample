using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizPlayEvents : MonoBehaviour
{
    public static Action<QuizSO> QuizStarted;
    public static Action<QuizQuestionSO> QuestionUpdated;
    public static Action<int, int> QuizProgressUpdated;
    public static Action<int> LifeCountUpdated;
    public static Action QuizPaused;
    public static Action QuizContinued;
    public static Action<int> ChoiceSelected;
    public static Action<int> CorrectlyAnswered;
    public static Action<int, int> IncorrectlyAnswered;
    public static Action<QuizSO> QuizEnded;
    public static Action<string> QuizFailed;
    public static Action<string> QuizPassed;
}
