using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManagerNeo : MonoBehaviour
{
    #region Stage Information
    [SerializeField]
    private int level;
    public int GetLevel { get { return level; } private set { } }
    public Paddle paddle;
    public Ball ball;
    public Brick[] bricks;
    public DeadZone[] walls;
    public bool isRandomItemSet = true;
    #endregion

    #region Fixed Parameter
    private static SFXType fallSFXType;
    private static LayerMask ballLayer;
    #endregion

    #region External Reference
    private GameManager gameManager;
    public GameObject GameManagerPrefab;
    #endregion

    private void Awake()
    {
        CheckGameManager();
        CheckGameElemets();
        SendStageDataToGameManager();
    }

    private void CheckGameElemets()
    {
        if (paddle == null)
        {
            paddle = FindFirstObjectByType<Paddle>();
        }
        if (ball == null)
        {
            ball = FindFirstObjectByType<Ball>();
        }
        if (bricks.Length == 0)
        {
            bricks = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        }
    }

    private void CheckGameManager()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("No GameManager Detected");
            Instantiate(GameManagerPrefab);
        }
        else
        {
            Debug.Log("GameManager Detected");
        }
        gameManager = GameManager.Instance;

    }

    private void SendStageDataToGameManager()
    {
        gameManager.SetStageDataAndSetting(this);
    }
}


