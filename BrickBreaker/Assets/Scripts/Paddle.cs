using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private PaddleHandler paddleHandler;

    public float speed = 15f;
    public Rigidbody2D rigidBody { get; private set; }

    public Vector2 firstPosition { get; private set; }

    public void Initialize()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();

        }
        firstPosition = transform.position;

        if (paddleHandler == null)
        {
            paddleHandler = GetComponent<PaddleHandler>();
        }
        paddleHandler.Initialize();
        paddleHandler.OnMovementInput += MovePaddle;

        ResetPaddle();
    }

    public void MovePaddle(Vector2 moveDirection)
    {
        if (moveDirection == Vector2.zero)
        {
            //rigidBody.velocity = Vector2.zero;
            return;
        }
        else
        {
            rigidBody.velocity = moveDirection * speed;
        }
    }

    public void ResetPaddle()
    {
        transform.position = firstPosition;
        rigidBody.velocity = Vector2.zero;
    }


}
