using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Paddle paddleThisStage;
    private bool isPaddleSet;
    private bool doesGameManagerExist;

    private GameManager gameManager;
    private StageManager stageManager;

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
    private string escButton = "Cancel";
    private float horizontalInput = 0;
    private float inputMin = 0.1f;

    public void Initialize()
    {
        deltaTime = Time.fixedDeltaTime;
        isPaddleSet = false;
    }

    public void ChangeStage(Paddle newPaddle, StageManager stageManager = null)
    {
        paddleThisStage = newPaddle;
        paddleRigidBody = paddleThisStage.rigidBody;
        paddleTransform = paddleThisStage.transform;
        firstPosition = newPaddle.firstPosition;
        if (stageManager == null)
        {
            gameManager = GameManager.Instance;
            doesGameManagerExist = true;
        }
        else
        {
            this.stageManager = stageManager;
            doesGameManagerExist = false;
        }
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

        if (Input.GetButtonDown("Cancel"))
        {
            if (doesGameManagerExist)
            {
                gameManager.ESCCall();
            }
            else
            {
                stageManager.ESCCall();
            }
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
