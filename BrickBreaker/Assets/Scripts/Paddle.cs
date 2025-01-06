using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private PaddleHandler paddleHandler;

    public float speed = 100f;
    private bool doDamping = false;
    // public Rigidbody2D rigidBody { get; private set; }

    public Vector2 firstPosition { get; private set; }

    public Vector3 velocity;

    public float dampingTime = 0.1f;

    public float stackTime = 0f;

    public void Initialize()
    {
        // if (rigidBody == null)
        // {
        //     rigidBody = GetComponent<Rigidbody2D>();
        //
        // }
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
            // stackTime = 0f;
            doDamping = true;
            return;
        }
        else
        {
            doDamping = false;
            velocity = moveDirection;
        }


    }

    private void Update()
    {
        if (doDamping)
        {
            if (velocity.x != 0)
                velocity = Vector3.SmoothDamp(velocity, Vector2.zero, ref velocity, dampingTime);
        }
        transform.position += velocity * (speed * Time.deltaTime);

        // todo: ÁÂ¿ì ÀÌÅ» Á¦ÇÑ

        //if (doDamping)
        //{
        //}

        // if(stackTime < dampingTime)
        // {
        //     stackTime += Time.deltaTime;
        //     velocity = Vector3.Lerp(velocity, Vector2.zero, stackTime / dampingTime);
        // }

    }

    public void ResetPaddle()
    {
        transform.position = firstPosition;
        //rigidBody.velocity = Vector2.zero;
    }


}
