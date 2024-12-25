using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameButtonManager gsButtonManager;
    [SerializeField] private MenuButtonManager msButtonManager;
    [SerializeField] private GameObject screenPanel;

    public void Initialize()
    {
        if(screenPanel != null)
        {
            screenPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("ScorePanel Not Detected");
        }

        if (scoreManager != null)
        {
            scoreManager.Initialize();
        }
        else
        {
            Debug.LogError("ScoreManager Not Detected");
        }

        if(gsButtonManager != null)
        {
            gsButtonManager.Initialize();
        }
        else
        {
            Debug.LogError("GameScreenButtonManager Not Detected");
        }

    }

    public void ChangeScore(int inputInt,bool isLevelChanged = false)
    {
        scoreManager.ChangeScore(inputInt, isLevelChanged);
    }
}
