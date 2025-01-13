using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    #region Magic Number
    private const string LEVEL_CALLING_STRING_W0 = "Level0";
    private const string LEVEL_CALLING_STRING = "Level";
    private const string TITLE_SCENE_STRING = "Global";
    private const int TITLE_SCENE_INT = 0;
    private const int MAX_LIFE = 99;
    private const int MAX_SCORE = 99999999;
    private const int LIFE_TO_SCORE = 10000;

    private SFXType fallSFXTye = SFXType.Fall;
    private int totalLevelCount = 0;
    private LayerMask ballLayer;
    private LayerMask paddleLayer;
    private LayerMask deadZoneLayer;
    [SerializeField] private float powerUpContinuanceTime = 10f;
    private WaitForSeconds powerUpWaitForSec;
    #endregion

    #region Player Status
    public int myScoreAtStageStart = 0;
    private int myScoreAtGameStart = 0;
    public int livesAtStageStart = 3;
    private int livesAtGameStart = 3;

    public int level = 0;
    public int myScore = 0;
    public int lives = 3;

    public int brickDamage = 1;

    [SerializeField] private int leftBrickCount = -1;
    [SerializeField] private int leftBallCount = -1;
    private bool stageCleared = false;
    #endregion

    #region Boolean
    [SerializeField] private bool isNewGame = true;
    [SerializeField] private bool isTemporaryGameManager = true;
    [SerializeField] private bool isMainMenuOn = false;
    #endregion

    #region Logic Parameter
    private Item[] itemSetting;
    private string tempString = null;
    private int tempInt = 0;
    private int tempInt2 = 0;
    private int totalWeight = 0;

    private Coroutine[] PowerUpCoroutines;
    private List<Coroutine> CoroutineLists = new List<Coroutine>();
    #endregion

    #region External Reference
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject uiManagerPrefab;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject droppingItemPrefab;
    private DroppingItem droppingItemInitializer;
    private List<DroppingItem> enabledDroppingItems = new List<DroppingItem>();
    public TitleScene titleScene;
    private static int itemTypeNumber;
    private int[] defaultItemProbability;

    #region MultiBall Logic Parameter
    private float highestBallHeight;
    private float highestHeight;
    private int highestBallIndex;
    private int tempInt3;
    private float angle;
    private float speed;
    #endregion

    #region Stage Object
    [SerializeField] private Paddle paddle;
    [SerializeField] private Ball[] balls;
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
        powerUpWaitForSec = new WaitForSeconds(powerUpContinuanceTime);
        isMainMenuOn = false;

        PowerUpCoroutines = new Coroutine[5];

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
        itemTypeNumber = brickData.itemTypeNumber;
        defaultItemProbability = brickData.defaultItemProbability;
        totalWeight = 0;
        foreach (int weight in defaultItemProbability)
        {
            totalWeight += weight;
        }

        if (uiManager == null)
        {
            uiManager = Instantiate(uiManagerPrefab).GetComponent<UIManager>();
        }
        uiManager.Initialize(brickData, isTemporaryGameManager);

        if (soundManager == null)
        {
            soundManager = FindFirstObjectByType<SoundManager>();
        }
        soundManager.Initialize(brickData);

        if (inputManager == null)
        {
            inputManager = FindFirstObjectByType<InputManager>();
        }
        inputManager.Initialize();

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
        LoadLevel(TITLE_SCENE_INT);
    }

    public void SetStageDataAndSetting(StageManagerNeo stageManagerNeo)
    {
        for (int i = 0; i < PowerUpCoroutines.Length; i++)
        {
            if (PowerUpCoroutines[i] != null)
            {
                StopCoroutine(PowerUpCoroutines[i]);
                PowerUpCoroutines[i] = null;
            }

        }

        this.stageManagerNeo = stageManagerNeo;
        level = stageManagerNeo.GetLevel;
        bricks = stageManagerNeo.bricks;
        leftBrickCount = bricks.Length;

        MakeItemSetting(stageManagerNeo.isRandomItemSet, stageManagerNeo.itemSettingWeight);
        for (int i = 0; i < leftBrickCount; i++)
        {
            bricks[i].Initialize(ballLayer, brickDamage, brickData, itemSetting[i]);
        }

        walls = stageManagerNeo.walls;
        foreach (DeadZone wall in walls)
        {
            wall.Initialize(ballLayer);
        }
        balls = stageManagerNeo.balls;
        for (int i = 0; i < balls.Length; i++)
        {
            if (i == 0)
            {
                balls[i].Initialize(brickData, brickDamage, true);
            }
            else
            {
                balls[i].Initialize(brickData, brickDamage);
            }

            //for (int j = i; j < balls.Length; j++)
            //{
            //    Physics2D.IgnoreCollision(balls[i].GetComponent<CircleCollider2D>(), balls[j].GetComponent<CircleCollider2D>());
            //}
        }
        leftBallCount = 1;
        highestBallHeight = -16f;
        paddle = stageManagerNeo.paddle;
        paddle.Initialize();

        DoUIManagerSetting(level, lives, myScore);
    }

    private void MakeItemSetting(bool isRandomSetting, int[] givenItemProbability)
    {
        if (givenItemProbability.Length != itemTypeNumber)
        {
            givenItemProbability = defaultItemProbability;
        }

        itemSetting = new Item[leftBrickCount];
        if (isRandomSetting)
            for (int i = 0; i < leftBrickCount; i++)
            {
                itemSetting[i] = (Item)WeightedRandom(givenItemProbability);
            }
    }

    private int WeightedRandom(int[] weights)
    {
        tempInt2 = Random.Range(0, totalWeight);
        tempInt = 0;
        for (int i = 0; i < itemTypeNumber; i++)
        {
            tempInt += weights[i];
            if (tempInt2 < tempInt)
            {
                return i;
            }
        }
        Debug.LogError("Item setting probability error");
        return -1; // errror
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

        if (this.level == 0)
        {
            myScore = myScoreAtGameStart;
            myScoreAtStageStart = myScoreAtGameStart;
            lives = livesAtGameStart;
            livesAtStageStart = livesAtGameStart;
        }

        this.level = level;
        if (level == 0)
        {
            myScore = myScoreAtGameStart;
            myScoreAtStageStart = myScoreAtGameStart;
            lives = livesAtGameStart;
            livesAtStageStart = livesAtGameStart;
            tempString = TITLE_SCENE_STRING;
        }
        else
        {
            if (level > 9)
            {
                tempString = $"{LEVEL_CALLING_STRING}{level}";
            }
            else
            {
                tempString = $"{LEVEL_CALLING_STRING_W0}{level}";
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

    public void PlaySFX(SFXType inputSFXType)
    {
        soundManager.PlaySFX(inputSFXType);
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
        ResetCoroutines();
        ResetItemDropBoxes();
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
            ResetBricks();
        }
    }

    private void ResetItemDropBoxes()
    {
        for (int i = 0; i < enabledDroppingItems.Count; i++)
        {
            enabledDroppingItems[i].DisableByReset();
        }
    }

    private void ResetBricks()
    {
        leftBrickCount = bricks.Length;
        foreach (Brick brick in bricks)
        {
            brick.ResetBrick();
        }
    }

    public void ResetBallCall(bool reshootBall = true)
    {
        leftBallCount = 1;
        highestBallHeight = -16;
        for (int i = 0; i < balls.Length; i++)
        {
            if (i == 0)
            {
                balls[i].ResetBall(reshootBall, true);
            }
            else
            {
                balls[i].ResetBall(reshootBall, false);
            }
        }

    }

    public void ResetPaddleCall()
    {
        paddle.ResetPaddle();
    }

    public void DeadZoneOut(GameObject maybeBall)
    {
        if (leftBallCount > 1)
        {
            leftBallCount--;
            maybeBall.GetComponent<Ball>().isActive = false;
            maybeBall.gameObject.SetActive(false);
        }
        else
        {
            lives--;
            leftBallCount = 1;
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
    }

    public void ItemGettodaze(Item item)
    {
        switch (item)
        {
            case Item.PowerUp:
                {
                    for (int i = 0; i < balls.Length; i++)
                    {
                        //if (balls[i].isActive)
                            PowerUpCoroutines[i] = StartCoroutine(BrickDamagerUp());
                    }
                    break;
                }
            case Item.LifeUp:
                {
                    LifeUp();
                    break;
                }
            case Item.MultiBall:
                {
                    MakeMultiBall();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void MakeMultiBall()
    {
        highestHeight = -16;
        highestBallIndex = 0;
        leftBallCount = balls.Length;
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].isActive)
                if (balls[i].transform.position.y > highestHeight)
                {
                    highestHeight = balls[i].transform.position.y;
                    highestBallIndex = i;
                }
        }

        balls[highestBallIndex].ReadyMultiBall();
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].MakeMultiBall();
        }





        //for (int i = 0; i < balls.Length; i++)
        //{
        //    balls[i].MakeMultiBall(balls[highestBallIndex]);
        //}

    }

    private void LifeUp(int deltaLife = 1)
    {
        if (lives < MAX_LIFE)
        {
            lives += deltaLife;
            if (lives > MAX_LIFE)
            {
                lives = MAX_LIFE;
            }
            uiManager.ChangeNumber(lives, UINumberCategory.Life);
        }
        else if (myScore < MAX_SCORE)
        {
            myScore += LIFE_TO_SCORE;
            uiManager.ChangeNumber(myScore);
        }
    }

    IEnumerator BrickDamagerUp(int upedPower = 2)
    {
        if (upedPower > brickDamage)
        {
            brickDamage = upedPower;
        }
        bricks[0].BrickDamagerSetter = brickDamage;
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].isActive)
            {
                balls[i].ChangeColor(brickDamage);
            }
        }


        yield return powerUpWaitForSec;

        brickDamage = 1;
        bricks[0].BrickDamagerSetter = brickDamage;
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].isActive)
            {
                balls[i].ChangeColor(brickDamage);
            }
        }

        yield return null;
    }

    private void ResetCoroutines()
    {
        if (CoroutineLists.Count == 0)
        {
            for (int i = 0; i < PowerUpCoroutines.Length; i++)
            {
                CoroutineLists.Add(PowerUpCoroutines[i]);
            }
        }

        for (int i = 0; i < CoroutineLists.Count; i++)
        {
            if (CoroutineLists[i] != null)
            {
                StopCoroutine(CoroutineLists[i]);
                //CoroutineLists[i] = null;
            }
        }
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

    public void TakeSettingValue(int value, TitleSceneSetterType setterType)
    {
        switch (setterType)
        {
            case TitleSceneSetterType.Life:
                {
                    livesAtGameStart = value;
                    break;
                }
            default:
                break;
        }
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

