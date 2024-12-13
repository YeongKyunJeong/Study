using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { instance = value; }  }

    [SerializeField]
    private JewelBoard jewelBoard;
    [SerializeField]
    private JewelData jewelData;
    [SerializeField]
    private UIManager uIManager;

    public int cycleCount = 10;

    public int score;

    private void Awake()
    {
        instance = this;
        jewelData.Initialize();

        jewelBoard.Initialize(jewelData);

        uIManager.Initialize();
    }


}
