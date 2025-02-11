using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:: Manager 라는 명칭의 목적
public class PanelButtonManager : MonoBehaviour
{
    public GameOverPopUp gameOverPopUp;
    public PausePopUp pausePopUp;

    public void Initialize(bool isTemporaryGameManager)
    {
        if (gameOverPopUp == null)
        {
            // TODO:: Child 검사 진행
            gameOverPopUp = FindFirstObjectByType<GameOverPopUp>();
        }
        if (pausePopUp == null)
        {
            pausePopUp = FindFirstObjectByType<PausePopUp>();
        }

        
            gameOverPopUp.Initialize(isTemporaryGameManager);
            pausePopUp.Initialize(isTemporaryGameManager);
        

        ResetPanel();
    }

    public void PauseGame()
    {
        SetActive(true);
        pausePopUp.SetActive(true);
    }

    public void SetActive(bool isOn)
    {
        gameObject.SetActive(isOn);
    }

    public void ResetPanel()
    {
        SetActive(false);
        gameOverPopUp.SetActive(false);
        pausePopUp.SetActive(false);

    }

    public void GameOver()
    {
        SetActive(true);
        gameOverPopUp.SetActive(true);
    }
}
