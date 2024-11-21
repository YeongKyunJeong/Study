using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // static : 정적, 메모리에 얹어버림, 유니티에서 나타나지 않아서 스크립트로 초기화해야됨
    public Player player;
    public Transform playerTransform;
    public PoolManager poolManager;
    public Transform area;

    private void Awake()
    {
        instance = this;
        if(playerTransform == null)
        {
            playerTransform = player.transform;
        }
    }

    public void Update()
    {
        area.position = playerTransform.position;
    }

}
