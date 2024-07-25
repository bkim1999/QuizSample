using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizPlayEvents : MonoBehaviour
{
    public static Action<QuizSO> QuizStarted;
    public static Action QuizPaused;
    public static Action QuizContinued;
    public static Action<int> AnswerSelected;
    public static Action CorrectlyAnswered;
    public static Action IncorrectlyAnswered;
    public static Action QuizFailed;
    public static Action QuizPassed;
}
