using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] private PlayerManager player;
    public PlayerManager Player { get => player; }

    [field: SerializeField] private Joystick joystick;
    public Joystick Joystick { get => joystick; }
    private static GameManager instance;
    public static GameManager Instance { get => instance; }
    public Action GameStartEvent;

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError("Player not Assigned");
        }

        if (joystick == null)
        {
            Debug.LogError("Joystick not Assigned");
        }
    }

    private void Start()
    {
        player.Initialize();
    }
}
