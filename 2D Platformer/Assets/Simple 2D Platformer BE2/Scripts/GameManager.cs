using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int stagePoint = 0;

    private void Awake()
    {
        instance = this;
        stagePoint = 0;
    }
}
