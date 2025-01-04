using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    #region Magic Number
    private string levelCallingStringWith0 = "Level0";
    private int totalLevelCount = 0;
    private string levelCallingString = "Level";
    private string titleSceneString = "Global";
    private int titleSceneNumber = 0;
    private SFXType fallSFXTye = SFXType.Fall;
    private LayerMask ballLayer;
    private LayerMask paddleLayer;
    private LayerMask deadZoneLayer;
    #endregion

    #region Player Status
    public int myScoreAtStageStart = 0;
    private int myScoreAtGameStart = 0;
    public int livesAtStageStart = 3;
    private int livesAtGameStart = 3;

    public int level = 1;
    public int myScore = 0;
    public int lives = 3;

    public int brickDamage = 1;

    [SerializeField] private int leftBrickCount = -1;
    private bool stageCleared = false;
    #endregion

    #region Boolean
    [SerializeField] private bool isNewGame = true;
    [SerializeField] private bool isTemporaryGameManager = true;
    [SerializeField] private bool isMainMenuOn = false;
    #endregion

    #region Logic Parameter
    private string tempString = null;
    #endregion

    #region External Reference
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private GameObject droppingItemPrefab;
    private DroppingItem droppingItemInitializer;
    private List<DroppingItem> enabledDroppingItems = new List<DroppingItem>();
    public TitleScene titleScene;

    #region Stage Object
    [SerializeField] private Paddle paddle;
    [SerializeField] private Ball ball;
    [SerializeField] private Brick[] bricks;
    [SerializeField] private BrickData brickData;
    [SerializeField] private DeadZone[] walls;
    [SerializeField] private StageManagerNeo stageManagerNeo;
    #endregion

    #endregion

    public static GameManager Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            instance.titleScene = this.titleScene;
            titleScene.Initialize();
            Destroy(this.gameObject);
            return;
        }

        if (isTemporaryGameManager)
        {

        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            totalLevelCount = SceneManager.sceneCountInBuildSettings;
            Debug.Log(totalLevelCount);
        }
        ballLayer = LayerMask.NameToLayer("Ball");
        paddleLayer = LayerMask.NameToLayer("Paddle");
        deadZoneLayer = LayerMask.NameToLayer("DeadZone");

        isMainMenuOn = false;

        Initialize();
    }

    private void Initialize()
    {
        //brickData = Resources.Load<BrickData>("Address");
        if (brickData == null)
        {
            Debug.LogError("BrickData asset not detected");
        }
        brickData.Initialize();

        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }
        uiManager.Initialize(brickData, isTemporaryGameManager);

        if (droppingItemPrefab == null)
        {
            Debug.LogError("DropingItemPrefab not detected");
        }
        droppingItemInitializer = droppingItemPrefab.GetComponent<DroppingItem>();
        droppingItemInitializer.GlobalInitialize(brickData, paddleLayer, deadZoneLayer);
        droppingItemInitializer = null;

        InputManager.OnESCInput += ESCCall;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (isTemporaryGameManager)
        {
            uiManager.gameObject.SetActive(true);
        }
        else
        {
            uiManager.gameObject.SetActive(false);

            if (titleScene == null)
            {
                titleScene = FindFirstObjectByType<TitleScene>();
            }
            titleScene.gameObject.SetActive(true);
            titleScene.Initialize();
        }
    }

    public void StartGameCall()
    {
        StartGame();
    }

    private void StartGame()
    {
        uiManager.gameObject.SetActive(true);
        if (isNewGame)
        {
            StartNewGame();
        }
        else
        { }// # To do: Add other game starting options;
    }

    public void OpenSetting()
    {

    }

    public void QuitGameCall()
    {
        QuitGame();
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    public void BackToTitleSceneCall()
    {
        uiManager.gameObject.SetActive(false);
        BackToTitleScene();
    }

    private void BackToTitleScene()
    {
        LoadLevel(titleSceneNumber);
    }

    public void SetStageDataAndSetting(StageManagerNeo stageManagerNeo)
    {
        this.stageManagerNeo = stageManagerNeo;
        level = stageManagerNeo.GetLevel;
        bricks = stageManagerNeo.bricks;
        leftBrickCount = bricks.Length;
        foreach (Brick brick in bricks)
        {
            brick.Initialize(ballLayer, brickDamage, brickData);
        }
        walls = stageManagerNeo.walls;
        foreach (DeadZone wall in walls)
        {
            wall.Initialize(ballLayer);
        }
        ball = stageManagerNeo.ball;
        ball.Initialize();
        paddle = stageManagerNeo.paddle;
        paddle.Initialize();

        DoUIManagerSetting(level, lives, myScore);
    }

    private void DoUIManagerSetting(int level, int lives, int score)
    {
        uiManager.DoUIManagerSetting(level, lives, score);
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
        isMainMenuOn = false;
        enabledDroppingItems = new List<DroppingItem>();
        TimeControler.TimeScaler(1);
        this.level = level;
        if (level == 0)
        {
            myScore = myScoreAtGameStart;
            lives = livesAtGameStart;
            tempString = titleSceneString;
        }
        else
        {

            if (level > 9)
            {
                tempString = $"{levelCallingString}{level}";
            }
            else
            {
                tempString = $"{levelCallingStringWith0}{level}";
            }

            myScoreAtStageStart = myScore;
            livesAtStageStart = lives;
        }

        SceneManager.LoadScene(tempString);


        uiManager.ChangeNumber(this.level, UINumberCategory.Level);
        uiManager.ResetStage(myScore, lives);
    }

    private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadSceneMode)
    {
        if (loadedScene == SceneManager.GetSceneByBuildIndex(0))
        {
            //titleScene = FindFirstObjectByType<TitleScene>();
            //titleScene.Initialize();
        }
    }

    public void HitBrick(int score, Vector3 brokenBrickPosition, bool isBroken = false, Item targetItem = Item.None)
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
            else if (targetItem != Item.None)
            {
                CreateDroppingItem(brokenBrickPosition, targetItem);
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
            PauseCall();
        }
    }

    public void ResumeCall()
    {
        ResumeGame();
    }

    private void ResumeGame()
    {
        TimeControler.TimeScaler(1);
        isMainMenuOn = false;
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
            myScore = myScoreAtStageStart;
            lives = livesAtStageStart;
            isMainMenuOn = false;

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
            ResetBallCall();
        }
        else
        {
            TimeControler.TimeScaler(0);
            uiManager.GameOver();
            Debug.Log("Game Over");
        }
    }

    public void ItemGettodaze(Item item)
    {
        switch (item)
        {
            case Item.PowerUp:
                {
                    BrickDamageUp();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void BrickDamageUp(int brickDamageDelta = 1)
    {
        brickDamage = brickDamageDelta;
        bricks[0].BrickDamagerSetter += brickDamage;
    }

    public void CreateDroppingItem(Vector3 creationPosition, Item targetItem)
    {
        for (int i = 0; i < enabledDroppingItems.Count; i++)
        {
            if (!enabledDroppingItems[i].isEnable)
            {
                droppingItemInitializer = enabledDroppingItems[i];
                droppingItemInitializer.SelfInitialize(creationPosition, targetItem);
                return;
            }
        }

        droppingItemInitializer = Instantiate(droppingItemPrefab).GetComponent<DroppingItem>();
        enabledDroppingItems.Add(droppingItemInitializer);
        droppingItemInitializer.SelfInitialize(creationPosition, targetItem);
    }
}

public static class TimeControler
{
    public static void TimeScaler(float timeScale)
    {
        Time.timeScale = timeScale;
        Debug.Log(Time.timeScale.ToString());
    }
}

