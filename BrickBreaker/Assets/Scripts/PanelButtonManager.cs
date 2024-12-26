using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButtonManager : MonoBehaviour
{
    public GameOverPopUp gameOverPopUp;

    public void Initialize(StageManager stageManager = null)
    {
        if (gameOverPopUp == null)
        {
            gameOverPopUp = FindFirstObjectByType<GameOverPopUp>();
        }
        if (stageManager == null)
            gameOverPopUp.Initialize();
        else
            gameOverPopUp.Initialize(stageManager);

        SetActive(false);
    }

    public void SetActive(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    public void ResetStage()
    {
        SetActive(false);
    }

    public void GameOver()
    {
        SetActive(true);
        gameOverPopUp.SetActive(true);
    }
}
