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
    [SerializeField]
    #endregion

    #region Fixed Parameter
    private static SFXType fallSFXType;
    private static LayerMask ballLayer;
    #endregion

    #region External Reference
    private GameManager gameManager;
    private GameObject GameManagerPrefab;
    #endregion

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("No GameManager Detected");
            Instantiate(GameManagerPrefab);
        }
        else
        {
            Debug.Log("GameManager Detected");
            return;
        }
        gameManager = GameManager.Instance;
        SendStageDataToGameManager();
    }

    private void SendStageDataToGameManager()
    {
        gameManager.SetStageDataAndSetting(this);
    }
}


