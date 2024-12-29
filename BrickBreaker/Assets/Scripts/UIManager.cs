using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameButtonManager gsButtonManager;
    [SerializeField] private PanelButtonManager panelButtonManager;
    [SerializeField] private AspectRatioEnforcer aspectRatioEnforcer;
    [SerializeField] private InputManager inputManager;
    public InputManager GetInputManager{ get { return inputManager; } private set { inputManager = value; } }

    public void Initialize(BrickData brickData, StageManager stageManager = null)
    {

        if (scoreManager != null)
        {
            scoreManager.Initialize();
        }
        else
        {
            Debug.LogError("ScoreManager Not Detected");
        }

        if (gsButtonManager != null)
        {
            if (stageManager == null)
                gsButtonManager.Initialize();
            else
                gsButtonManager.Initialize(stageManager); ;
        }
        else
        {
            Debug.LogError("GameButtonManager Not Detected");
        }

        if (panelButtonManager != null)
        {
            if (stageManager == null)
                panelButtonManager.Initialize();
            else
                panelButtonManager.Initialize(stageManager); ;
        }
        else
        {
            Debug.LogError("PanelButtonManager Not Detected");
        }

        if (soundManager != null)
        {
            soundManager.Initialize(brickData);
        }
        else
        {
            Debug.LogError("SoundManager Not Detected");
        }

        if(inputManager != null)
        {
            inputManager.Initialize();
        }
        else
        {
            Debug.LogError("InputManager Not Detected");
        }

        if (aspectRatioEnforcer != null)
        {
            aspectRatioEnforcer.Initialize(GetComponent<RectTransform>());
        }
    }

    public void PlaySFX(SFXType inputSFXType)
    {
        soundManager.PlaySFX(inputSFXType);
    }

    public void ChangeStage(Paddle newPaddle)
    {
        aspectRatioEnforcer.ChangeSceneWithoutCamera();
        inputManager.ChangePaddle(newPaddle);
    }

    public void ChangeNumber(int inputInt, UINumberCategory changedNumber = UINumberCategory.Score)
    {
        scoreManager.ChangeNumber(inputInt, changedNumber);
    }

    public void ResetStage(int scoreAtStart, int livesAtStart)
    {
        scoreManager.ResetStage(scoreAtStart, livesAtStart);
        panelButtonManager.ResetStage();
    }

    public void GameOver()
    {
        panelButtonManager.GameOver();
    }


}
