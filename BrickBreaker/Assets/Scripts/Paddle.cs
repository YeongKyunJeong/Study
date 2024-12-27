using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }
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
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();

        }
        firstPosition = transform.position;

        deltaTime = Time.fixedDeltaTime;
        //Debug.Log("Paddle Ready");
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

    public void ResetPaddle()
    {
        transform.position = firstPosition;
        rigidBody.velocity = Vector2.zero;
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
                rigidBody.velocity = direction * speed;
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

}
