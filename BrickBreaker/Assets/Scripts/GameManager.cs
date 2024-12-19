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

    [SerializeField] private bool isNewGame = true;

    public static GameManager Instance { get { return instance; } private set { instance = value; } }
    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);


        Initialize();
    }

    private void Initialize()
    {
        if (isNewGame)
            StartNewGame();
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
        //SceneManager.LoadScene(level);
    }
}
