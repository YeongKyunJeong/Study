using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private string levelCallingStringWith0 = "Level0";
    private int totalLevelCount = 0;
    private string levelCallingString = "Level";
    private string tempString = null;
    public int level = 1;
    public int myScore = 0;
    public int lives = 3;
    private bool isMainMenuOn = false;

    public int myScoreAtStart = 0;
    public int livesAtStart = 0;


    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private bool isTemporaryGameManager = true;

    [SerializeField] private bool isNewGame = true;
    [SerializeField] private Paddle paddle;
    [SerializeField] private Ball ball;
    [SerializeField] private Brick[] bricks;
    [SerializeField] private BrickData brickData;
    //[SerializeField] private StageManager stageManager;
    [SerializeField] private StageManagerNeo stageManagerNeo;

    private SFXType fallSFXTye = SFXType.Fall;

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

        if (isTemporaryGameManager)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            totalLevelCount = SceneManager.sceneCountInBuildSettings;
            Debug.Log(totalLevelCount);
        }
        ballLayer = LayerMask.NameToLayer("Ball");
        isMainMenuOn = false;
        //Addressables.LoadAssetAsync<BrickData>("Assets/Scripts/BrickData.asset").Completed += OnBrickDataLoad;

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
        uiManager.Initialize(brickData);
        InputManager.OnESCInput += ESCCall;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (isTemporaryGameManager)
        {
            return;
        }
        else
        {
            if (isNewGame)
                StartNewGame();
            else
            { }// # To do: Add other game starting options;
        }
    }

    public void SetStageDataAndSetting(StageManagerNeo stageManagerNeo)
    {
        this.stageManagerNeo = stageManagerNeo;
        level = stageManagerNeo.GetLevel;
        bricks = stageManagerNeo.bricks;
        ball = stageManagerNeo.ball;
        paddle = stageManagerNeo.paddle;

        DoUIManagerSetting();
    }

    private void DoUIManagerSetting()
    {
        this.uiManager.ChangeNumber(level, UINumberCategory.Level);
        this.uiManager.ChangeNumber(lives, UINumberCategory.Life);
        this.uiManager.ChangeNumber(myScore, UINumberCategory.Score);
        this.uiManager.ChangeStage();
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

        SceneManager.LoadScene(tempString);

        myScoreAtStart = myScore;
        livesAtStart = lives;

        uiManager.ChangeNumber(this.level, UINumberCategory.Level);
        uiManager.ResetStage(myScore, lives);
        //uiManager.ChangeNumber(lives, UINumberCategory.Life);
        //uiManager.ChangeNumber(myScore, UINumberCategory.Score);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        if (loadedScene == SceneManager.GetSceneByBuildIndex(0))
        {
            Debug.Log("Main Scene");
        }
        //else
        //    FindStageManagerAndInitialize();
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

    public void PlaySFX(SFXType inputSFX)
    {
        uiManager.PlaySFX(inputSFX);
    }

    public void ESCCall()
    {
        if (isMainMenuOn)
        {
            isMainMenuOn = false;
            ResumeCall();
        }
        else
        {
            isMainMenuOn = true;
            PauseGame();
        }
    }

    public void ResumeCall()
    {
        ResumeGame();
    }

    private void ResumeGame()
    {
        TimeControler.TimeScaler(1);
        uiManager.ResumeGame();
    }

    public void PauseCall()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        TimeControler.TimeScaler(0);
        uiManager.PauseGame();
    }

    public void ResetCall(bool isFullReset)
    {
        ResetGame(isFullReset);
    }

    private void ResetGame(bool isFullRest)
    {
        if (isFullRest)
        {
            StartNewGame();
        }
        else
        {
            myScore = myScoreAtStart;
            lives = livesAtStart;

            TimeControler.TimeScaler(1);
            uiManager.ResetStage(myScore, lives);
            ResetPaddleCall();
            ResetBallCall();
            foreach (Brick brick in bricks)
            {
                brick.ResetBrick();
            }
        }
    }

    public void ResetBallCall(bool reshootBall = true)
    {
        ball.ResetBall(reshootBall);
    }

    public void ResetPaddleCall()
    {
        paddle.ResetPaddle();
    }

    public void DeadZoneOut()
    {
        lives--;
        PlaySFX(fallSFXTye);
        uiManager.ChangeNumber(lives, UINumberCategory.Life);

        if (lives > 0)
        {
            stageManager.ResetBallCall();
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

