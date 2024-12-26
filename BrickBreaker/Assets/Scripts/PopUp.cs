using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PopUp : MonoBehaviour
{
    protected GameObject go;
    protected bool doseGameManagerExist;
    protected GameManager gameManager;
    protected StageManager stageManager;
    public Button[] buttons;

    public abstract void Initialize(StageManager stageManager = null);

}
