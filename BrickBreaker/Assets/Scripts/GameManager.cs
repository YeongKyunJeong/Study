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
        if (aspectRatioEnforcer == null)
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
        TimeControler.TimeScaler(1);
        this.level = level;
        if (level > 9)
        {
            tempString = $"{levelCallingString}{level}";
        }
        else
        {
            tempString = $"{levelCallingStringWith0}{level}";
        }


        uiManager.ChangeNumber(this.level, UINumberCategory.Level);
        uiManager.ChangeNumber(lives, UINumberCategory.Life);
        uiManager.ChangeNumber(myScore, UINumberCategory.Score);
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
        uiManager.ChangeNumber(myScore);
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
                uiManager.ChangeNumber(level, UINumberCategory.Level);
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

    public void ResetCall(bool isFullReset)
    {
        ResetGame(isFullReset);
    }

    private void ResetGame(bool isFullRest)
    {
        uiManager.ResetStage();
        if (isFullRest)
        {
            StartNewGame();
        }
        else
        {
            stageManager.ResetCall();
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
        lives--;
        uiManager.ChangeNumber(lives, UINumberCategory.Life);
        if (lives > 0)
        {
            ResetBall();
        }
        else
        {
            TimeControler.TimeScaler(0);
            uiManager.GameOver();
            Debug.Log("Game Over");
        }
    }
}

public static class TimeControler
{
    public static void TimeScaler(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}

