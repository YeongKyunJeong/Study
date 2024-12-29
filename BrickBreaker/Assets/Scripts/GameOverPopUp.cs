using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPopUp : PopUp
{

    //private TextMeshProUGUI disabledBtnTMP;
    //private Color disabledBtnTMPColor;

    public override void Initialize(StageManager stageManager = null)
    {
        go = this.gameObject;
        go.SetActive(false);
        if (stageManager == null)
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
        if (isInitializing)
        {
            isInitializing = false;
            buttons[0].interactable = true;
            buttons[1].interactable = doseGameManagerExist;
            if (doseGameManagerExist)
            {
                TextMeshProUGUI disabledBtnTMP = buttons[1].GetComponentInChildren<TextMeshProUGUI>();
                Color disabledBtnTMPColor = disabledBtnTMP.color ;
                disabledBtnTMPColor.a = 0.4f;
                disabledBtnTMP.color = disabledBtnTMPColor;
            }
        }
    }

    public void ClickResetButton(bool isFullReset)
    {
        if (doseGameManagerExist)
            gameManager.ResetCall(isFullReset);
        else
            stageManager.ResetCall();
    }
}
