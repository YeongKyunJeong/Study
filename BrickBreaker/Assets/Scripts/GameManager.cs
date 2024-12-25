using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int level = 1;
    private int totalLevelCount = 0;
    private string levelCallingStringWith0 = "Level0";
    private string levelCallingString = "Level";
    private string tempString = null;
    public int myScore = 0;
    public int lives = 3;

    [SerializeField] private UIManager uiManager;

    [SerializeField] private bool isNewGame = true;
    [SerializeField] private Paddle paddle;
    [SerializeField] private Ball ball;
    [SerializeField] private BrickData brickData;
    [SerializeField] private StageManager stageManager;

    [SerializeField]
    private AspectRatioEnforcer aspectRatioEnforcer;

    private LayerMask ballLayer;

    private bool stageCleared = false;
    [SerializeField] private int leftBrickCount = -1;

    public static GameManager Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {

        if (instance == null)
        {
            Instance = this;
        }
        totalLevelCount = SceneManager.sceneCountInBuildSettings;
        Debug.Log(totalLevelCount);
        ballLayer = LayerMask.NameToLayer("Ball");
        //Addressables.LoadAssetAsync<BrickData>("Assets/Scripts/BrickData.asset").Completed += OnBrickDataLoad;

        DontDestroyOnLoad(this.gameObject);

        Initialize();
    }

    //private void OnBrickDataLoad(AsyncOperationHandle<BrickData> loadedBrickData)
    //{
    //    if (loadedBrickData.Status == AsyncOperationStatus.Succeeded)
    //    {
    //        brickData = loadedBrickData.Result;
    //        brickData.Initialize();
    //    }
    //    else
    //    {
    //        Debug.LogError("GameManager : BrickData load error");
    //    }
    //}

    private void Initialize()
    {
        brickData = Resources.Load<BrickData>("Data/BrickData");
        brickData.Initialize();


        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }
        if(aspectRatioEnforcer == null)
        {
            uiManager.GetComponent<AspectRatioEnforcer>();
        }
        uiManager.Initialize();
        aspectRatioEnforcer.Initialize(uiManager.GetComponent<RectTransform>());

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (isNewGame)
            StartNewGame();
    }

    private void FindStageManagerAndInitialize()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        if (stageManager == null)
        {
            Debug.LogError("No StageManager detected");
            return;
        }
        else
        {
            stageManager.Initialize(ballLayer, brickData);
            aspectRatioEnforcer.ChangeSceneWithoutCamera();
            leftBrickCount = stageManager.bricks.Length;
        }
    }

    private void StartNewGame()
    {
        myScore = 0;
        lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        stageCleared = false;
        this.level = level;
        if (level > 9)
        {
            tempString = $"{levelCallingString}{level}";
        }
        else
        {
            tempString = $"{levelCallingStringWith0}{level}";
        }


        uiManager.ChangeScore(this.level, true);
        SceneManager.LoadScene(tempString);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        if (loadedScene == SceneManager.GetSceneByBuildIndex(0))
        {
            Debug.Log("Main Scene");
        }
        else
            FindStageManagerAndInitialize();
    }

    public void ScoreUp(int score, bool isBroken = false)
    {
        myScore += score;
        uiManager.ChangeScore(myScore);
        if (isBroken)
        {
            leftBrickCount--;
            if (leftBrickCount == 0)
            {
                stageCleared = true;
            }
        }
        if (stageCleared)
        {
            if (level < totalLevelCount)
            {
                level++;
                uiManager.ChangeScore(level, true);
            }
            LoadLevel(level);
        }
    }

    private bool StageClear()
    {
        return false;
    }

    private void GameOver()
    {
        // StartNewGame();
    }

    private void ResetGame(bool isFullRest)
    {
        if (isFullRest)
        {
            if (lives < 3)
            {
                lives = 3;
            }
            stageManager.ResetPaddleCall();
        }
        stageManager.ResetBallCall();
    }

    private void ResetBall()
    {
        stageManager.ResetBallCall();
    }

    //private void ResetPaddle()
    //{
    //    stageManager.ResetPaddleCall();
    //}

    public void DeadZoneOut()
    {
        if (lives > 1)
        {
            lives--;

            ResetBall();

            // lives UI Change
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
}
