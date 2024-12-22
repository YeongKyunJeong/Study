using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Paddle paddle;
    public Ball ball;
    public Brick[] bricks;
    public BrickData brickData;
    public DeadZone[] walls;
    public bool doesGameManagerExists = true;

    private LayerMask ballLayer;

    public int tempScore;
    public int lives;

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
        ballLayer = LayerMask.NameToLayer("Ball");
        doesGameManagerExists = false;
        lives = 3;
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
        Debug.Log($"StageManager : {tempScore}");
    }
    
    public void TempDeadZoneOut()
    {
        
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
