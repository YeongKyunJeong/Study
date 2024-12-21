using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int level = 1;
    private string levelCallingStringWith0 = "Level0";
    private string levelCallingString = "Level";
    private string tempString = null;
    public int myScore = 0;
    public int lives = 3;

    [SerializeField] private bool isTemporary = true;   // GM used when stage scene run independently

    [SerializeField] private bool isNewGame = true;

    [SerializeField] private Paddle paddle;

    [SerializeField] private Ball ball;
    public static GameManager Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {

        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            if (isTemporary)   // Turn it off if there is true GameManager
            {
                this.gameObject.SetActive(false);
            }

        }

        if (!isTemporary)   // Only make true GameManager DontdestroyOnLoad 
        {
            DontDestroyOnLoad(this.gameObject);
        }

        Initialize(isTemporary);
    }

    private void Initialize(bool isTemporaryGameManager = true)
    {
        if (isTemporaryGameManager)
        {
            StartStageIndependently();
        }
        else
        {
            if (isNewGame)
                StartNewGame();

        }

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
    }

    private void StartNewGame()
    {
        myScore = 0;
        lives = 3;

        LoadLevel(1);
    }
    private void StartStageIndependently()
    {
        myScore = 0;
        lives = 3;
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
}
