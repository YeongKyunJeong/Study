using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPopUp : ButtonPopUp
{
    public override void SetActive(bool isOn)
    {
        go.SetActive(isOn);
        if (isInitializing)
        {
            isInitializing = false;
            buttons[0].interactable = true;
            buttons[1].interactable = !isTemporaryGameManager;
            if (isTemporaryGameManager)
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
            gameManager.ResetCall(isFullReset);
    }
}
