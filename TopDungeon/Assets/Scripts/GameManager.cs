using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }


        PlayerPrefs.DeleteAll();


        instance = this;
        //SceneManager.sceneLoaded += SaveState;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprties;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    // public weapon weapon....

    // Logic
    public int pesos;
    public int experience;

    // Save State
    /*
     * INT preferedSkin
     * INT pesos
     * Int experience
     * Int weapenLevel
     */
    public void SaveState(/*Scene s, LoadSceneMode mode*/)
    {
        //SceneManager.sceneLoaded -= SaveState;
        
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        //SceneManager.sceneLoaded -= LaveState;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player skin
        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change weapon state

        Debug.Log("LoadState");
    }
}
