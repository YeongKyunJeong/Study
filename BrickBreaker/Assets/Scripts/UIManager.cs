using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameButtonManager gsButtonManager;
    [SerializeField] private PanelButtonManager panelButtonManager;

    public void Initialize(StageManager stageManager = null)
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
            Debug.LogError("GameScreenButtonManager Not Detected");
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
            Debug.LogError("GameScreenButtonManager Not Detected");
        }

    }

    public void ChangeNumber(int inputInt, UINumberCategory changedNumber = UINumberCategory.Score)
    {
        scoreManager.ChangeNumber(inputInt, changedNumber);
    }

    public void ResetStage(/* Data of this stage at start*/)
    {
        panelButtonManager.ResetStage();
    }

    public void GameOver()
    {
        panelButtonManager.GameOver();
    }


}
