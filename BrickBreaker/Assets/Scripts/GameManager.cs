using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int level = 1;
    private string levelCallingStringWith0 = "Level0";
    private string levelCallingString = "Level";
    private string tempString = null;
    public int myScore = 0;
    public int lives = 3;

    //private 

    [SerializeField] private bool isNewGame = true;

    [SerializeField] private Paddle paddle;

    [SerializeField] private Ball ball;

    [SerializeField] private BrickData brickData;

    [SerializeField] private StageManager stageManager;

    private LayerMask ballLayer;


    public static GameManager Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {

        if (instance == null)
        {
            Instance = this;
        }

        ballLayer = LayerMask.NameToLayer("Ball");
        //Addressables.LoadAssetAsync<BrickData>("Assets/Scripts/BrickData.asset").Completed += OnBrickDataLoad;
        brickData = Resources.Load<BrickData>("Data/BrickData");
        brickData.Initialize();


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
            //paddle = stageManager.paddle;
            //paddle.Initialize();
            //ball = stageManager.ball;
            //ball.Initialize();
            //for (int i = 0; i < stageManager.bricks.Length; i++)
            //{
            //    stageManager.bricks[i].Initialize(brickData);
            //}
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
        this.level = level;
        if (level > 9)
        {
            tempString = $"{levelCallingString}{level}";
        }
        else
        {
            tempString = $"{levelCallingStringWith0}{level}";
        }


        Debug.Log(tempString);
        SceneManager.LoadScene(tempString);
        //SceneManager.LoadScene(level);
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
        Debug.Log($"StageManager : {score}");
        if (isBroken)
        {
            Debug.Log("Broken");
        }
    }

    public void DeadZoneOut()
    {

    }
}
