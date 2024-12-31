using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHandler : MonoBehaviour
{
    private float inputMin = 0.1f;

    public Action<Vector2> OnMovementInput;
    public Vector2 direction { get; private set; }

    public void Initialize()
    {
        InputManager.OnMovementInput += SendMovementInput;
    }

    private void SendMovementInput(float horizontalInput)
    {
        OnMovementInput?.Invoke(horizontalInput * Vector2.right);
    }

}
