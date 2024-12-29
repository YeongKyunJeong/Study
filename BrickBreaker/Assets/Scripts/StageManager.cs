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
    public bool doesGameManagerExist = true;
    private SFXType fallSFXTye;

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
            doesGameManagerExist = true;
        }
    }

    private void ActAsGameManager()
    {
        TimeControler.TimeScaler(1);
        ballLayer = LayerMask.NameToLayer("Ball");
        doesGameManagerExist = false;
        fallSFXTye = SFXType.Fall;
        Initialize(ballLayer, null);
        uiManager = Instantiate(uiPrefab).GetComponent<UIManager>();
        uiManager.Initialize(brickData, this);
        uiManager.ChangeNumber(level, UINumberCategory.Level);
        tempLives = 3;
        uiManager.ChangeNumber(tempLives, UINumberCategory.Life);
    }

    public void Initialize(LayerMask ballLayer, BrickData brickData = null)
    {
        if (paddle == null)
        {
            paddle = FindFirstObjectByType<Paddle>();
        }
        paddle.Initialize();


        if (bricks.Length == 0)
        {
            bricks = FindObjectsByType<Brick>(0);
        }

        if (doesGameManagerExist)
        {
            ball.Initialize();
        }
        else
        {
            ball.Initialize(this);
            tempLeftBrickCount = bricks.Length;
        }

        if (ball == null)
        {
            ball = FindFirstObjectByType<Ball>();
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
            if (doesGameManagerExist)
                bricks[i].Initialize(ballLayer, brickData);
            else
                bricks[i].Initialize(ballLayer, brickData, this);

        }

        for (int i = 0; i < 4; i++)
        {
            if (doesGameManagerExist)
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
        PlaySFX(fallSFXTye);
        uiManager.ChangeNumber(tempLives, UINumberCategory.Life);
        if (tempLives > 0)
        {
            ResetBallCall();
            // lives UI Change
        }
        else
        {
            TimeControler.TimeScaler(0);
            uiManager.GameOver();
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
        TimeControler.TimeScaler(1);

        paddle.ResetPaddle();
        ResetBallCall();
        foreach (Brick brick in bricks)
        {
            brick.ResetBrick();
        }
        //
    }

    public void PlaySFX(SFXType inputSFXType)
    {
        uiManager.PlaySFX(inputSFXType);
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
