using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rigidBody { get; private set; }

    private static GameManager gameManager;
    private static BrickData brickData;

    private bool isFirstBall;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector2 force = Vector2.zero;
    private Vector3 paddPosition;
    private Vector2 contactPosition;
    private float offset;
    private float angle;
    private float corectedHalfWidth;
    private Coroutine coroutine;
    private Quaternion rotation;
    private Vector2 tempVec;
    private Vector2 firstPosition;

    #region MultiBall Parameter
    public static int multiBallStack = 0;
    private static Vector3 multiBallPos;
    private static float multiBallAngle;
    private static float multiBallspeed;
    private static int leftBallIndex;
    private static float degreeDelta = 5;
    private static bool isUp;

    #endregion

    public float speed;
    private float speedCorrector;

    private static float halfWidth;
    private static float bounceBallSpeed;
    //private WaitForFixedUpdate waitForFixedFrame;
    public static float maxBounceAngle = 60f;
    private static SFXType paddleHitType = SFXType.PaddleHit;
    private static float deflectionStartOffset = 0.75f;
    private static WaitForSeconds waitFor1s;
    private static LayerMask ballLayer;
    private static LayerMask paddleLayer;
    private static LayerMask bricksLayer;
    private bool isFirstTimeUsage = true;
    //private LayerMask bricksLayer;

    public void GlobalInitialize(BrickData givenBrickData)
    {
        if (gameManager == null)
            gameManager = GameManager.Instance;


        if (brickData == null)
            brickData = givenBrickData;
        ballLayer = LayerMask.NameToLayer("Ball");
        paddleLayer = LayerMask.NameToLayer("Paddle");
        bricksLayer = LayerMask.NameToLayer("Bricks");
        speed = 500f;
        halfWidth = 2.5f;
        corectedHalfWidth = halfWidth - deflectionStartOffset;
        multiBallStack = 0;
        waitFor1s = new WaitForSeconds(1f);
    }

    public void Initialize(int ballPower, bool isFirstBall = false)
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        if (spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        this.isFirstBall = isFirstBall;
        gameObject.SetActive(isFirstBall);

        if (isFirstTimeUsage)
        {
            gameManager.ResetBallAction += ResetBall;
            gameManager.BallPowerChangeAction += ChangeColor;
        }
        isFirstTimeUsage = false;

        firstPosition = transform.position;

        coroutine = null;

        if (isFirstBall)
            coroutine = StartCoroutine(CoroutineAtStart());

        spriteRenderer.color = brickData.ballColors[ballPower - 1];
    }

    public void SetRandomDirection()
    {
        force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1;

        rigidBody.AddForce(force.normalized * speed);
        if (gameObject.activeInHierarchy)
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

    public void ResetBall(/*bool reshootBall, bool isFirstBall*/)
    {
        gameObject.SetActive(isFirstBall);
        multiBallStack = 0;
        ChangeColor(1);
        if (isFirstBall)
        {
            rigidBody.velocity = Vector2.zero;
            transform.position = firstPosition;

            ShootBallAtStart();
        }
    }

    public void BeMultiBall()
    {
        gameObject.SetActive(true);
        if (isUp)
        {
            if (multiBallAngle > 55)
            {
                angle = multiBallAngle;
            }
            else if (multiBallAngle > 50)
            {
                angle = multiBallAngle = degreeDelta;
            }
            else if (multiBallAngle < -55)
            {
                angle = multiBallAngle + 4 * degreeDelta;
            }
            else if (multiBallAngle < -45)
            {
                angle = multiBallAngle + 3 * degreeDelta;
            }
            else
            {
                angle = multiBallAngle + 2 * degreeDelta;
            }
        }
        else
        {
            if (multiBallAngle >= 120 || multiBallAngle < 125)
            {
                angle = multiBallAngle + 4 * degreeDelta;
            }
            else if (multiBallAngle >= 125 || multiBallAngle < 130)
            {
                angle = multiBallAngle + 3 * degreeDelta;
            }
            else if (multiBallAngle <= -120 || multiBallAngle > -125)
            {
                angle = multiBallAngle;
            }
            else if (multiBallAngle <= -125 || multiBallAngle > -130)
            {
                angle = multiBallAngle + degreeDelta;
            }
            else
            {
                angle = multiBallAngle + 2 * degreeDelta;
            }

        }

        transform.position = multiBallPos;
        angle -= leftBallIndex * degreeDelta;
        rigidBody.velocity = multiBallspeed * (new Vector2(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad)));
        leftBallIndex++;
    }

    public void ChangeColor(int ballPower)
    {
        spriteRenderer.color = brickData.ballColors[ballPower - 1];
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
    float save = 0;
    float save2 = 0;
    float save3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == paddleLayer)
        {
            if (rigidBody.velocity.y > 0)
                HitPaddle(collision);

        }
        if (collision.gameObject.layer == ballLayer)
        {
            Debug.Log("Ball");
        }
        else if (collision.gameObject.layer == bricksLayer)
        {
            HitBrick(collision);
        }
        //StartCoroutine(SaveStartSpeed());
    }

    private void HitPaddle(Collision2D collision)
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
            //rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //rigidBody.velocity = rotation * Vector2.up;

            rigidBody.velocity = new Vector2(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            //tempVec = new Vector2(-Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle*Mathf.Deg2Rad));
            //Debug.Log("1: " +rigidBody.velocity.x + " , " + rigidBody.velocity.y);
            //Debug.Log("2: "+ tempVec.x + " , " + tempVec.y);
            //Debug.Log("Angle :" + angle + "/ " + Mathf.Cos(angle));
            //Debug.Log(Mathf.Cos(angle));
        }
        if (Mathf.Abs(angle) > 10)
        {
            speedCorrector = Mathf.Abs(angle) / 90f;
            save = speedCorrector;
            save2 = (1 - Mathf.Abs(save * save * save));
            speedCorrector = 1 / (1 - Mathf.Abs(speedCorrector * speedCorrector * speedCorrector));
            save3 = 1 / save2;
        }
        else
        {
            speedCorrector = 1;
        }

        rigidBody.velocity = rigidBody.velocity.normalized * (speedCorrector * bounceBallSpeed);

        if (multiBallStack > 0)
        {
            multiBallStack--;
            ReadyMultiBall();
            BeMultiBall();
            gameManager.MakeMultiballCall();
        }



    }

    private void HitBrick(Collision2D collision)
    {

    }


    public void PlayPaddleHitSFX()
    {
        gameManager.PlaySFX(paddleHitType);
    }

    internal void ReadyMultiBall()
    {
        multiBallPos = transform.position;
        multiBallAngle = Vector2.SignedAngle(Vector2.up, rigidBody.velocity);
        multiBallspeed = rigidBody.velocity.magnitude;
        isUp = rigidBody.velocity.y > 0 ? true : false;
        leftBallIndex = 0;
    }
}
