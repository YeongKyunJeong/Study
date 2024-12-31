using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PausePopUp : ButtonPopUp
{
    public override void SetActive(bool isOn)
    {
        go.SetActive(isOn);
        if (isInitializing)
        {
            isInitializing = false;
            buttons[0].interactable = true;
            buttons[1].interactable = true;
            buttons[2].interactable = !isTemporaryGameManager;
            if (isTemporaryGameManager)
            {
                TextMeshProUGUI disabledBtnTMP = buttons[2].GetComponentInChildren<TextMeshProUGUI>();
                Color disabledBtnTMPColor = disabledBtnTMP.color;
                disabledBtnTMPColor.a = 0.4f;
                disabledBtnTMP.color = disabledBtnTMPColor;
            }
        }
        base.SetActive(isOn);
    }

    public void ClickResetButton(bool isFullReset)
    {
            gameManager.ResetCall(isFullReset);
    }

    public void ClickResumeButton()
    {
            gameManager.ResumeCall();
    }
}
