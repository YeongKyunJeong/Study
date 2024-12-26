using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private int level;
    public Paddle paddle;
    public Ball ball;
    public Brick[] bricks;
    public BrickData brickData;
    public DeadZone[] walls;
    public bool doesGameManagerExists = true;

    private LayerMask ballLayer;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private UIManager uiManager;

    public int tempScore;
    public int tempLives;
    public int tempLeftBrickCount;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            Debug.Log("No GameManager Detected");
            ActAsGameManager();
        }
        else
        {
            Debug.Log("GameManager Detected");
            doesGameManagerExists = true;
        }
    }

    private void ActAsGameManager()
    {
        TimeControler.TimeScaler(1);
        ballLayer = LayerMask.NameToLayer("Ball");
        doesGameManagerExists = false;
        uiManager = Instantiate(uiPrefab).GetComponent<UIManager>();
        uiManager.Initialize(this);
        uiManager.ChangeNumber(level, UINumberCategory.Level);
        tempLives = 3;
        uiManager.ChangeNumber(tempLives, UINumberCategory.Life);
        Initialize(ballLayer, null);
    }

    public void Initialize(LayerMask ballLayer, BrickData brickData = null)
    {
        if (paddle == null)
        {
            paddle = FindFirstObjectByType<Paddle>();
        }
        paddle.Initialize();

        if (ball == null)
        {
            ball = FindFirstObjectByType<Ball>();
        }
        ball.Initialize();

        if (bricks.Length == 0)
        {
            bricks = FindObjectsByType<Brick>(0);
        }

        if (!doesGameManagerExists)
        {
            tempLeftBrickCount = bricks.Length;
        }

        if (brickData == null)
        {
            //Addressables.LoadAssetAsync<BrickData>("Assets/Scripts/BrickData.asset").Completed += OnBrickDataLoad;
            this.brickData = Resources.Load<BrickData>("Data/BrickData");
            this.brickData.Initialize();
            brickData = this.brickData;
        }

        for (int i = 0; i < bricks.Length; i++)
        {
            if (doesGameManagerExists)
                bricks[i].Initialize(ballLayer, brickData);
            else
                bricks[i].Initialize(ballLayer, brickData, this);

        }

        for (int i = 0; i < 4; i++)
        {
            if (doesGameManagerExists)
                walls[i].Initialize(ballLayer);
            else
                walls[i].Initialize(ballLayer, this);
        }
    }

    public void TempScoreUp(int score, bool isBroken = false)
    {
        tempScore += score;
        uiManager.ChangeNumber(tempScore);
        if (isBroken)
        {
            Debug.Log("Broken");

            tempLeftBrickCount--;
            if (tempLeftBrickCount == 0)
            {
                Debug.Log("GameClaer");
            }
        }
    }

    public void TempDeadZoneOut()
    {
        tempLives--;
        uiManager.ChangeNumber(tempLives, UINumberCategory.Life);
        if (tempLives > 0)
        {
            ResetBallCall();
            // lives UI Change
        }
        else
        {
            TimeControler.TimeScaler(0);
            Debug.Log("Game Over");
        }
    }

    public void ResetBallCall(bool reshootBall = true)
    {
        ball.ResetBall();
        if (reshootBall)
            ball.ShootBallAtStart();
    }

    public void ResetPaddleCall()
    {
        paddle.ResetPaddle();
    }

    public void ResetCall()
    {
        ResetStage();
    }

    public void ResetStage()
    {
        Debug.Log("Reset Stage");
        //
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
    //        Debug.LogError("StageManager : BrickData load error");
    //    }
    //}
}
