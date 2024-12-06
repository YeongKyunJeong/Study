using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } set { instance = value; }  }

    public int CycleCount = 10;

    private void Awake()
    {
        Instance = this;
    }


}
