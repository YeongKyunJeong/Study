using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
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
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    // 온갖 곳에서 FloationgTextManager를 참조하게 하지 말고 Show()를 사용 가능하게 하기 위해서
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r++];
            //r++;

            if (r == xpTable.Count)
                break;
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r++];
        }

        return xp;
    }
    public void GrantXp(int xp) //***** Bug fixed
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        int endLevel = GetCurrentLevel();
        for (int i = 0; i < endLevel-currLevel; i++)
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("Level Up");
        player.OnLevelUp();
    }
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
        s += weapon.weaponLevel.ToString();

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
        if (GetCurrentLevel() != 1)
        {
            player.maxHitPoint = 10;
            player.SetLevel(GetCurrentLevel());
        }
        // Change weapon state
        weapon.SetWeaponLevel(int.Parse(data[3]));


        Debug.Log("LoadState");

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
