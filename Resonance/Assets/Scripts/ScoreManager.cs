using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI goalScore;
    public int goalScoreSetter
    {
        get { return int.Parse(goalScore.text); }
        set
        {
            int raw = value <= 99999999 ? value : 99999999;
            goalScore.text = ScoreTextSet(raw);
        }
    }

    public TextMeshProUGUI myScore;
    public int myScoreSetter
    {
        get { return int.Parse(myScore.text); }
        set
        {
            int raw = value <= 99999999 ? value : 99999999;
            myScore.text = ScoreTextSet(raw);
        }
    }


    public TextMeshProUGUI chance;
    public int chanceSetter
    {
        get { return int.Parse(chance.text); }
        set
        {
            chance.text = ChanceTextSet(value);
        }
    }

    private string stringForWork = "";
    private int intForWork = 0;

    public void Initialize(int initGoalScore, int initChance = 10, int initMyScore = 0)
    {
        goalScoreSetter = initGoalScore;
        myScoreSetter = initMyScore;
        chanceSetter = initChance;
    }

    private string ChanceTextSet(int rawInt, int totalChance = 10)
    {
        stringForWork = "";
        stringForWork = rawInt.ToString() + " / " + totalChance.ToString();
        return stringForWork;
    }

    private string ScoreTextSet(int rawInt, int maxLength = 8)
    {
        stringForWork = rawInt.ToString();
        intForWork = stringForWork.Length;
        for (int i = 0; i < maxLength - intForWork; i++)
        {
            stringForWork = "0" + stringForWork;
        }
        return stringForWork;
    }
}
