using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public ButtonManager ButtonManager;

    public void Initialize(int initGoalScore = 1000, int initChance = 10, int initMyScore = 0)
    {
        scoreManager.Initialize(initGoalScore, initChance, initMyScore);
        ButtonManager.Initializie();
    }

    public void ScoreChangeCall(int resultScore)
    {
        scoreManager.myScoreSetter = resultScore;
        scoreManager.chanceSetter = scoreManager.chanceSetter - 1;
    }
}
