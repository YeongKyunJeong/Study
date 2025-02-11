using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class Initializer : MonoBehaviour
{
    [SerializeField] GameManager gameManagerPrefab;
    void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager gameManager = Instantiate(gameManagerPrefab);
            gameManager.InitAndStartGame();
        }

    }

}
