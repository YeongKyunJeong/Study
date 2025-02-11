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
    #endregion

    #region Player Status
    public int myScoreAtStageStart = 0;
    private int myScoreAtGameStart = 0;
    public int livesAtStageStart = 3;
    private int livesAtGameStart = 3;

    public int level = 0;
    public int myScore = 0;
    public int lives = 3;

    private int brickDamageAtGameStart = 1;
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
    public Action ResetBallAction;
    public Action ResetDroppingItemAction;
    public Action<int> BallPowerChangeAction;

    public Action InternalEventResetAction;
    #endregion

    #region External Reference
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject uiManagerPrefab;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private InputManager inputManager;
    
    public TitleScene titleScene;
    private static int itemTypeNumber;
    private int[] defaultItemProbability;
    private int multiBallNumber = 5;

    [SerializeField] private ObjectPool objectPool;

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
    
    public static string startSceneName = "";
    
    private void Awake()
    {
        if (startSceneName == "")
        {
            startSceneName = SceneManager.GetActiveScene().name;
            // 최초 신 실행
        }
        else
        {
        }

        if (startSceneName == "Global")
        {
        }
        
        
        
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            instance.titleScene = this.titleScene;
            titleScene.Initialize();
            Destroy(this.uiManager.gameObject);
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

        isMainMenuOn = false;

        PowerUpCoroutines = new Coroutine[5];
        objectPool.Initialize();
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
        

        //if (paddlePrefab == null)
        //{
        //    Debug.LogError("PaddlePrefab not detected");
        //}
        //paddleInitializer = paddlePrefab;
        //droppingItemInitializer.GlobalInitialize(brickData, paddleLayer, deadZoneLayer);
        //droppingItemInitializer = null;

        InputManager.OnESCInput -= ESCCall;
        InputManager.OnESCInput += ESCCall;
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        InternalEventResetAction += ResetAction;

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
        BackToTitleScene();
    }

    private void BackToTitleScene()
    {
        uiManager.gameObject.SetActive(false);
        objectPool.Initialize();
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
        brickDamage = brickDamageAtGameStart;

        leftBrickCount = bricks.Length;

        MakeItemSetting(stageManagerNeo.isRandomItemSet, stageManagerNeo.itemSettingWeight);

        balls = stageManagerNeo.balls;
        for (int i = 0; i < balls.Length; i++)
        {
            if (i == 0)
            {
                balls[i].Initialize(brickDamage, true);
            }
            else
            {
                balls[i].Initialize(brickDamage);
            }
        }
        leftBallCount = 1;
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
            brickDamage = brickDamageAtGameStart;
        }

        ////////// Load stage without Scene loading
        SceneManager.LoadScene(tempString);

        InternalEventResetAction?.Invoke();
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

    private void ResetAction()
    {
        if (BallPowerChangeAction != null)
        {
            BallPowerChangeAction = null;
        }
        if (ResetBallAction != null)
        {
            ResetBallAction = null;
        }
        if (ResetDroppingItemAction != null)
        {
            ResetDroppingItemAction = null;
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
                // CreateDroppingItem(brokenBrickPosition, targetItem);
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
        //ResetItemDropBoxes();
        ResetCoroutines();

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
            ResetDroppingItemAction?.Invoke();
            uiManager.ResetStage(myScore, lives);
            ResetPaddleCall();
            ResetBallCall();
            ResetBricks();
        }
    }

    //private void ResetItemDropBoxes()
    //{
    //    for (int i = 0; i < enabledDroppingItems.Count; i++)
    //    {
    //        enabledDroppingItems[i].DisableByReset();
    //    }
    //}

    private void ResetBricks()
    {
        leftBrickCount = bricks.Length;
        foreach (Brick brick in bricks)
        {
            brick.ResetBrick();
        }
    }

    public void ResetBallCall()
    {
        leftBallCount = 1;
        ResetBallAction?.Invoke();
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
                        if(PowerUpCoroutines[i] != null)
                        {
                            StopCoroutine(PowerUpCoroutines[i]);
                            PowerUpCoroutines[i] = null;
                        }
                        // PowerUpCoroutines[i] = StartCoroutine(BrickDamagerUp());
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
                    MultiBallStackUp();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void MultiBallStackUp()
    {
        Ball.multiBallStack++;
        //highestHeight = -16;
        //highestBallIndex = 0;
        //leftBallCount = balls.Length;
        //for (int i = 0; i < balls.Length; i++)
        //{
        //    if (balls[i].gameObject.activeInHierarchy)
        //        if (balls[i].transform.position.y > highestHeight)
        //        {
        //            highestHeight = balls[i].transform.position.y;
        //            highestBallIndex = i;
        //        }
        //}

        //balls[highestBallIndex].ReadyMultiBall();
        //for (int i = 0; i < balls.Length; i++)
        //{
        //    balls[i].MakeMultiBall();
        //}





        //for (int i = 0; i < balls.Length; i++)
        //{
        //    balls[i].MakeMultiBall(balls[highestBallIndex]);
        //}
    }

    public void MakeMultiballCall()
    {
        for (int i = 0; i < multiBallNumber - 1; i++)
        {

            leftBallCount++;
        }
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

    // IEnumerator BrickDamagerUp(int upedPower = 2)
    // {
    //     if (upedPower > brickDamage)
    //     {
    //         brickDamage = upedPower;
    //     }
    //     bricks[0].BrickDamagerSetter = brickDamage;
    //     BallPowerChangeAction?.Invoke(brickDamage);
    //     //for (int i = 0; i < balls.Length; i++)
    //     //{
    //     //    if (balls[i].gameObject.activeInHierarchy)
    //     //    {
    //     //        balls[i].ChangeColor(brickDamage);
    //     //    }
    //     //}
    //
    //
    //     yield return powerUpWaitForSec;
    //
    //     brickDamage = 1;
    //     bricks[0].BrickDamagerSetter = brickDamage;
    //     BallPowerChangeAction?.Invoke(brickDamage);
    //     //for (int i = 0; i < balls.Length; i++)
    //     //{
    //     //    if (balls[i].gameObject.activeInHierarchy)
    //     //    {
    //     //        balls[i].ChangeColor(brickDamage);
    //     //    }
    //     //}
    //
    //     yield return null;
    // }

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

    // public void CreateDroppingItem(Vector3 creationPosition, Item targetItem)
    // {
    //     droppingItemInitializer = objectPool.GetObject<DroppingItem>(PoolObjectType.DroppingItemBox);
    //     droppingItemInitializer.SelfInitialize(creationPosition, targetItem);
    // }

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

    public void InitAndStartGame()
    {
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            instance.titleScene = this.titleScene;
            titleScene.Initialize();
            Destroy(this.uiManager.gameObject);
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
        //
        // ballLayer = LayerMask.NameToLayer("Ball");
        // paddleLayer = LayerMask.NameToLayer("Paddle");
        // deadZoneLayer = LayerMask.NameToLayer("DeadZone");
        // powerUpWaitForSec = new WaitForSeconds(powerUpContinuanceTime);
        isMainMenuOn = false;

        PowerUpCoroutines = new Coroutine[5];
        objectPool.Initialize();
        Initialize();
    }
}

public static class TimeControler
{
    public static void TimeScaler(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}

