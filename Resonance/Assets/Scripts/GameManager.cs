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

    public int myScore = 0;

    private void Awake()
    {
        instance = this;

        uIManager.Initialize();

        jewelData.Initialize(); 
        jewelBoard.Initialize(jewelData);
    }

    public void ScoreChangeCall(int resultScore)
    {
        myScore += resultScore;
        uIManager.ScoreChangeCall(myScore);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }

}
