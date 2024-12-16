using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { instance = value; } }

    [SerializeField]
    private string sceneName;

    [SerializeField]
    private int screenWidth;
    [SerializeField]
    private int screenHeight;

    [SerializeField]
    private AspectRatioEnforcer aspectRatioEnforcer;

    [SerializeField]
    private JewelBoard jewelBoard;
    [SerializeField]
    private JewelData jewelData;
    [SerializeField]
    private UIManager uIManager;

    public int cycleCount = 10;

    public int myScore = 0;

    private void Awake()
    {
        GameManager.Instance = this;
        uIManager.Initialize();
        aspectRatioEnforcer.Initialize(uIManager.GetComponent<RectTransform>(), screenWidth, screenHeight);
        jewelData.Initialize();
        jewelBoard.Initialize(jewelData);
    }

    public void ScoreChangeCall(int resultScore)
    {
        myScore += resultScore;
        uIManager.ScoreChangeCall(myScore);
    }

    public void Restart()
    {
        if (sceneName == null)
        {
            Debug.Log("Scene Name Error");
        }
        else
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneName);
        }

    }

    public void GameOver()
    {
        uIManager.ButtonManager.continueButton.enabled = false;
        uIManager.ButtonManager.MenuBtnClick();
        Debug.Log("Game Over");
    }

}
