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
    public int[] itemSettingWeight = new int[0];
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


    #region StageEditing
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private GameObject brickRowPrefab;
    [SerializeField] private Transform[] brickRows;
    [SerializeField] private Transform brickRowParents;

    public float brickSpaceX = 0.25f;
    public float brickSpaceY = 0.25f;

    private float defaultBrickHeight = 5.5f;
    private float brickSizeX = 4;
    private float brickSizeY = 1;

    public void GenerateBricks(int brickRowCount = 5, int brickPerRow = 7, float brickSpaceX = 0.25f, float brickSpaceY = 0.25f)
    {
        ClearBricks(brickRowCount, brickPerRow);
        brickRows = new Transform[brickRowCount];
        for (int i = 0; i < brickRowCount; i++)
        {
            brickRows[i] = Instantiate(brickRowPrefab, brickRowParents).transform;
            brickRows[i].position = new Vector3(0, defaultBrickHeight + ((brickRowCount-1)/2 - i)* (brickSpaceY + brickSizeY), 0);
            ///// ## To Do : Add brick Initailize
        }
    }

    private void ClearBricks(int brickRowCount, int brickPerRow)
    {
        if (brickRows.Length != 0)
        {
            for (int q = brickRows.Length - 1; q > -1; q--)
            {
                DestroyImmediate(brickRows[q].gameObject);
            }
        }
        brickRows = new Transform[brickRowCount];
        bricks = new Brick[brickRowCount * brickPerRow];

    }

    #endregion
}


