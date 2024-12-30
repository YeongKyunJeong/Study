using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonPopUp : PopUp
{
    public override void Initialize(StageManager stageManager = null)
    {
        isInitializing = true;
        go = this.gameObject;
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
        SetActive(false);
    }

    public virtual void SetActive(bool isOn)
    {
        go.SetActive(isOn);
        if (isInitializing)
        {
            isInitializing = false;
            buttons[0].interactable = true;
            buttons[1].interactable = doseGameManagerExist;
            if (!doseGameManagerExist)
            {
                TextMeshProUGUI disabledBtnTMP = buttons[1].GetComponentInChildren<TextMeshProUGUI>();
                Color disabledBtnTMPColor = disabledBtnTMP.color;
                disabledBtnTMPColor.a = 0.4f;
                disabledBtnTMP.color = disabledBtnTMPColor;
            }
        }
    }
}
