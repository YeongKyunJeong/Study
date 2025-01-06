using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Setter : MonoBehaviour
{
    [SerializeField] protected Button[] buttons;
    //[SerializeField] Slider slider;
    [SerializeField] protected TextMeshProUGUI valueTMP;
    // Start is called before the first frame update
    protected GameManager gameManager;

    public abstract void OnValueChangeButtonClick(bool isUp);

    public abstract void TakeValue();

}
