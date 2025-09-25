using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] private PlayerManager player;
    public PlayerManager Player {  get => player;  }

    [field: SerializeField] private Joystick joystick;
    public Joystick Joystick { get => joystick; }

    private void Awake()
    {
        if(player == null)
        {
            Debug.LogError("Player not Assigned");
        }

        if (joystick == null)
        {
            Debug.LogError("Joystick not Assigned");
        }
    }
}
