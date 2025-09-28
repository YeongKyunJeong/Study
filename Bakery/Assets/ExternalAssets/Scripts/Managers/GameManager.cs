using Bakery;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [field: SerializeField] private PlayerManager player;
    public PlayerManager Player { get => player; }

    [field: SerializeField] private Joystick joystick;
    public Joystick Joystick { get => joystick; }

    [field: SerializeField] private BreadManager breadManager;
    public BreadManager BreadManage { get => breadManager; }
    [field: SerializeField] private InteractionManager interactionManager;
    public InteractionManager InteractionManager { get => interactionManager; }
    [field: SerializeField] private CustomerManager customerManager;
    public CustomerManager CustomerManager { get => customerManager; }

    public Action GameStartEvent;

    private void Awake()
    {
        instance = this;


        if (joystick == null)
        {
            Debug.LogError("Joystick not Assigned");
        }

        if (breadManager == null)
        {
            Debug.LogError("BreadManager not Assigned");
        }

        if (interactionManager == null)
        {
            Debug.LogError("InteractionManager not Assigned");
        }

        if (customerManager == null)
        {
            Debug.LogError("customerManager not Assigned");
        }

        if (player == null)
        {
            Debug.LogError("Player not Assigned");
        }

    }

    private void Start()
    {
        player.Initialize();
        interactionManager.Initialize();
        breadManager.Initialize();
        customerManager.Initialize();


        GameStartEvent?.Invoke();
    }

    private void Update()
    {
        player.CallUpdate();
        customerManager.CallUpdate();
        interactionManager.CallUpdate();
    }
}
