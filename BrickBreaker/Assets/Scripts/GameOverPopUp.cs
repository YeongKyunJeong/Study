using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUp : PopUp
{

    public override void Initialize(StageManager stageManager = null)
    {
        go = this.gameObject;
        go.SetActive(false);
        if ( stageManager == null)
        {
            doseGameManagerExist = true;
            gameManager = GameManager.Instance;
        }
        else
        {
            doseGameManagerExist = false;
            this.stageManager = stageManager;
        }
        SetActive(true);

    }

    public void SetActive(bool isOn)
    {
        go.SetActive(isOn);
        buttons[0].interactable = true;
        buttons[1].interactable = doseGameManagerExist;
    }

    public void ClickResetButton(bool isFullReset)
    {
        if (doseGameManagerExist)
            gameManager.ResetCall(isFullReset);
        else
            stageManager.ResetCall();
    }
}
