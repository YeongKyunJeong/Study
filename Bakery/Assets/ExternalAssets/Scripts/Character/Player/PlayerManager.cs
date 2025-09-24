using Bakery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerStateMachine StateMachine;

    public void Initialize()
    {
        StateMachine = new PlayerStateMachine(this);
    }

}
