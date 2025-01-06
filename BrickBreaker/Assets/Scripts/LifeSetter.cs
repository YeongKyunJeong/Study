using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSetter : Setter
{
    private int lifeValue;
    private const int MAX_LIFE = 99;
    private const int MIN_LIFE = 1;

    public void Initialize()
    {
        gameManager = GameManager.Instance;
        lifeValue = gameManager.lives;
    }

    public override void OnValueChangeButtonClick(bool isUp)
    {
        if (isUp)
        {
            lifeValue++;
            if(lifeValue >= MAX_LIFE)
            {
                buttons[1].interactable = false;
            }
            else if(lifeValue > MIN_LIFE)
            {
                buttons[0].interactable = true;
            }

            valueTMP.text = lifeValue.ToString();            
        }
        else
        {
            lifeValue--;
            if (lifeValue < MAX_LIFE)
            {
                buttons[1].interactable = true;
            }
            else if (lifeValue <= MIN_LIFE)
            {
                buttons[0].interactable = false;
            }
            

            valueTMP.text = lifeValue.ToString();
        }
    }

    public override void TakeValue()
    {
        
    }
}
