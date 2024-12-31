using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }

    private GameManager gameManager;
    private StageManager stageManager;

    private float deflectionStartOffset = 0.75f;
    private Vector2 force = Vector2.zero;
    private Vector3 paddPosition;
    private Vector2 contactPosition;
    private float offset;
    private float angle;
    private float halfWidth;
    private float corectedHalfWidth;
    private Coroutine coroutine;
    private Quaternion rotation;
    private Vector2 firstPosition;
    private bool doesGameManagerExist = true;
    private SFXType paddleHitType = SFXType.PaddleHit;

    public float speed;
    private float speedCorrector;
    private float bounceBallSpeed;
    public float maxBounceAngle = 60f;
    private WaitForSeconds waitFor1s;
    //private WaitForFixedUpdate waitForFixedFrame;
    private LayerMask ballLayer;
    private LayerMask paddleLayer;
    //private LayerMask bricksLayer;

    public void Initialize(StageManager stageManager = null)
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        ballLayer = LayerMask.NameToLayer("Ball");
        paddleLayer = LayerMask.NameToLayer("Paddle");
        //bricksLayer = LayerMask.NameToLayer("Bricks");
        speed = 500f;
        halfWidth = 2.5f;

        if (stageManager == null)
        {
            doesGameManagerExist = true;
            gameManager = GameManager.Instance;
        }
        else
        {
            doesGameManagerExist = false;
            this.stageManager = stageManager;
        }

        firstPosition = transform.position;

        corectedHalfWidth = halfWidth - deflectionStartOffset;
        waitFor1s = new WaitForSeconds(1f);
        //waitForFixedFrame = new WaitForFixedUpdate();
        coroutine = null;
        coroutine = StartCoroutine(CoroutineAtStart());

    }

    public void SetRandomDirection()
    {
        force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1;

        // temp
        force.x = 0;

        rigidBody.AddForce(force.normalized * speed);
        StartCoroutine(SaveStartSpeed());

    }

    public void ShootBallAtStart()
    {
        if (coroutine != null)
        {
            coroutine = null;
        }
        coroutine = StartCoroutine(CoroutineAtStart());
    }

    public void ResetBall(bool reshootBall)
    {
        rigidBody.velocity = Vector2.zero;
        transform.position = firstPosition;
        if (reshootBall)
        {
            ShootBallAtStart();
        }
    }

    IEnumerator SaveStartSpeed()
    {
        yield return new WaitForFixedUpdate();
        bounceBallSpeed = rigidBody.velocity.magnitude;
        //Debug.Log(bounceBallSpeed);
        yield return null;
    }

    IEnumerator CoroutineAtStart()
    {
        // Temp
        Debug.Log("Count : 3");
        yield return waitFor1s;

        Debug.Log("Count : 2");
        yield return waitFor1s;

        Debug.Log("Count : 1");
        yield return waitFor1s;

        Debug.Log("Start!");
        SetRandomDirection();
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == paddleLayer)
        {
            if (rigidBody.velocity.y > 0)
                DeflectBall(collision);

        }
        if (collision.gameObject.layer == ballLayer)
        {
            Debug.Log("Ball");
        }
        //else if (collision.gameObject.layer == bricksLayer)
        //{
        //    collision.
        //}
        else
        {

        }
        //StartCoroutine(SaveStartSpeed());
    }

    private void DeflectBall(Collision2D collision)
    {
        PlayPaddleHitSFX();
        paddPosition = collision.transform.position;
        contactPosition = collision.GetContact(0).point;
        offset = contactPosition.x - paddPosition.x;

        if ((offset < deflectionStartOffset) && (offset > -deflectionStartOffset))
        {
        }
        else
        {
            if (offset >= deflectionStartOffset)
            {
                offset = offset > halfWidth ? halfWidth - deflectionStartOffset : offset - deflectionStartOffset;
            }
            else/* if (offset <= -deflectionStartOffset)*/
            {
                offset = offset < -halfWidth ? -halfWidth + deflectionStartOffset : offset + deflectionStartOffset;
            }
            angle = Vector2.SignedAngle(Vector2.up, rigidBody.velocity);    // incident angle
            angle = Mathf.Clamp(angle - (offset / halfWidth) * maxBounceAngle, -maxBounceAngle, maxBounceAngle);
            rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            rigidBody.velocity = rotation * Vector2.up;
            //Debug.Log("Angle :" + angle + "/ " + Mathf.Cos(angle));
            //Debug.Log(Mathf.Cos(angle));
            speedCorrector = Mathf.Abs(angle) / 90f;
        }
        if (Mathf.Abs(angle) > 10)
            speedCorrector = 1 / (1 - speedCorrector * speedCorrector * speedCorrector);
        else
        {
            speedCorrector = 1;
        }
        Debug.Log(speedCorrector);
        rigidBody.velocity = rigidBody.velocity.normalized * (speedCorrector * bounceBallSpeed);

        return;
    }

    public void PlayPaddleHitSFX()
    {
        if (doesGameManagerExist)
        {
            gameManager.PlaySFX(paddleHitType);
        }
        else
        {
            stageManager.PlaySFX(paddleHitType);
        }
    }
}
