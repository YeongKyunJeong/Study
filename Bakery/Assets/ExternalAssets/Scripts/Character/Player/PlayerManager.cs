using Bakery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field: SerializeField] private PlayerStateMachine stateMachine;
    public PlayerStateMachine StateMachine { get => stateMachine; }

    [field: SerializeField] private CharacterController controller;
    public CharacterController Controller { get => controller; }
    
    [field: SerializeField] private Mover mover;
    public Mover Mover { get => mover; }

    [field: SerializeField] private Joystick joystick;
    public Joystick Joystick { get => joystick; }

    [field: SerializeField] private Animator animator;
    public Animator Animator { get => animator; }

    public void Initialize()
    {
        if(controller == null) 
        {
            controller = GetComponent<CharacterController>();
        }
        if(mover == null)
        {
            mover = GetComponent<Mover>();
        }
        mover.Initialize(this);
        if(joystick == null)
        {
            joystick = GetComponent<Joystick>();
        }
        stateMachine = new PlayerStateMachine(this);
    }

}
