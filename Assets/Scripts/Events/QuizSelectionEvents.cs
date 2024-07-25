using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuizSelectionEvents
{
    public static Action<QuizSelectionScreen> Initialized;
    public static Action<QuizSO[]> QuizButtonsInserted;
    public static Action<QuizSO> QuizDataLoaded;
    public static Action<QuizSO> QuizSelected;
}
