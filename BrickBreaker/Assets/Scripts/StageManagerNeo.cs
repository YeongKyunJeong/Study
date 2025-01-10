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
    [SerializeField] private BrickData brickData;
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

    [HideInInspector]
    [SerializeField] private string[] rowBrickHealths = new string[0];

    private int[] convertedRowBrickHealths;

    [HideInInspector]
    [SerializeField] private int brickRowCount = 5;
    [HideInInspector]
    [SerializeField] private int brickPerRow = 7;
    [HideInInspector]
    [SerializeField] private float brickSpaceX = 0.25f;
    [HideInInspector]
    [SerializeField] private float brickSpaceY = 0.25f;

    private float defaultBrickHeight = 5.5f;
    private int defaultBrickHealth = 1;
    private float brickSizeX = 4;
    private float brickSizeY = 1;

    public void GenerateBricks(bool isRandomHealth = true)
    {
        MakeBricks(brickRowCount, brickPerRow, brickSpaceX, brickSpaceY, isRandomHealth, rowBrickHealths);
    }

    public void MakeBricks(int brickRowCount, int brickPerRow, float brickSpaceX, float brickSpaceY, bool isRandomHealth, string[] rowHealths)
    {
        brickData.Initialize();
        if (brickRowParents == null)
        {
            brickRowParents = GameObject.Find("Bricks").transform;
        }

        ClearBricks(brickRowCount, brickPerRow);


        for (int i = 0; i < brickRowCount; i++)
        {

            brickRows[i] = Instantiate(brickRowPrefab, brickRowParents).transform;
            brickRows[i].localPosition = new Vector3(0, defaultBrickHeight + ((float)(brickRowCount - 1) / 2 - i) * (brickSpaceY + brickSizeY), 0);


            Brick[] tempBrick = brickRows[i].GetComponentsInChildren<Brick>();
            for (int j = 6; j > -1; j--)
            {
                int health = int.Parse(rowBrickHealths[i][j].ToString());
                if (j < brickPerRow)
                {
                    bricks[brickPerRow * i + j] = tempBrick[j];
                    tempBrick[j].transform.localPosition = new Vector3((-(float)(brickPerRow - 1) / 2 + j) * (brickSpaceX + brickSizeX), 0, 0);
                    tempBrick[j].SetBrickParameter(health, true, brickData);
                }
                else
                {
                    DestroyImmediate(tempBrick[j].gameObject);
                }
            }
        }
    }

    private void ClearBricks(int brickRowCount, int brickPerRow)
    {
        if (brickRows.Length != 0)
        {
            for (int q = brickRows.Length - 1; q > -1; q--)
            {
                if (brickRows[q] != null)
                    DestroyImmediate(brickRows[q].gameObject);
            }
        }
        brickRows = new Transform[brickRowCount];
        bricks = new Brick[brickRowCount * brickPerRow];
    }

    #endregion
}


