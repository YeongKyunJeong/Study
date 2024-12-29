using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Paddle paddleThisStage;
    private bool isPaddleSet;

    public Rigidbody2D paddleRigidBody { get; private set; }
    public Transform paddleTransform { get; private set; }
    public Vector2 direction { get; private set; }
    public float speed = 25f;
    private float realSpeed;
    private float deltaTime;

    [SerializeField]
    private int movingMode = 0;

    private Vector2 firstPosition;
    private string horizontalBtn = "Horizontal";
    private float horizontalInput = 0;
    private float inputMin = 0.1f;

    public void Initialize()
    {
        deltaTime = Time.fixedDeltaTime;
        isPaddleSet = false;
    }

    public void ChangePaddle(Paddle newPaddle)
    {
        paddleThisStage = newPaddle;
        paddleRigidBody = paddleThisStage.rigidBody;
        paddleTransform = paddleThisStage.transform;
        firstPosition = newPaddle.firstPosition;
        isPaddleSet = true;

        ResetPaddle();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw(horizontalBtn);
        if (horizontalInput > inputMin)
        {
            direction = Vector2.right;
        }
        else if (horizontalInput < -inputMin)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (movingMode == 0)
        {
            if (direction == Vector2.zero)
            {
                //rigidBody.velocity = Vector2.zero;
                return;
            }
            else
            {
                paddleRigidBody.velocity = direction * speed;
            }
        }
        //else
        //{
        //    if (direction != Vector2.zero)
        //    { 
        //        rigidBody.AddForce(direction * speed);
        //    }
        //}
    }

    public void ResetPaddle()
    {
        paddleTransform.position = firstPosition;
        paddleRigidBody.velocity = Vector2.zero;
    }
}
