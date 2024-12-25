using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ScoreManager : MonoBehaviour
{
    private const int MAX_SCORE = 99999999;
    private const int MAX_SOCRE_LENGTH = 8;
    private const int MAX_LEVEL = 99;
    private const int MAX_LEVEL_LENGTH = 2;

    [SerializeField] private TextMeshProUGUI scoreTMP;
    private int scoreValue;
    public int ScoreSetter
    {
        get { return scoreValue; }
        set
        {
            if (value > MAX_SCORE)
            {
                scoreValue = MAX_SCORE;
            }
            else
            {
                scoreValue = value;
            }
            scoreTMP.text = MakeIntToString(scoreValue, MAX_SOCRE_LENGTH);
        }
    }
    private StringBuilder stringBuilder;
    private string tempString;
    private int tempInt;

    [SerializeField] private TextMeshProUGUI levelTMP;
    private int levelValue;
    public int LevelSetter
    {
        get { return levelValue; }
        set
        {
            if (value > MAX_LEVEL)
            {
                levelValue = MAX_LEVEL;
            }
            else
            {
                levelValue = value;
            }
            levelTMP.text = MakeIntToString(levelValue, MAX_LEVEL_LENGTH);
        }
    }

    public void Initialize()
    {
        stringBuilder = new StringBuilder(10);
    }

    public void ChangeScore(int inputInt, bool isLevelChanged = false)
    {
        if (isLevelChanged)
        {
            LevelSetter = inputInt;
        }
        else
        {
            ScoreSetter = inputInt;
        }
    }

    private string MakeIntToString(int inputScore, int maxLength)
    {
        stringBuilder.Clear();
        tempString = inputScore.ToString();
        tempInt = maxLength - tempString.Length;
        for (int i = 0; i < tempInt; i++)
        {
            stringBuilder.Append("0");
        }
        stringBuilder.Append(inputScore);

        return stringBuilder.ToString();
    }

}
