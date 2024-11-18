using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Player player;
    public Rigidbody2D target;

    public bool isLive;

    public bool isMoving;
    public bool fireCall;
    public bool isFiring;
    public bool isMeleeing;

    public Animator animator;

    Rigidbody2D rigidb;
    float moveDirZ;
    //SpriteRenderer spriter;

    public float fireRange;
    public float fireCoolTime;
    [SerializeField] public bool isMovingShotUnit = false;
    [SerializeField] private float nowCooledTime;
    [SerializeField] private bool isDebug = false;
    [SerializeField] private bool isToofar = false;

    private float nextVecDelta;
    private float radToDegree = 180 / Mathf.PI;
    private Coroutine animationCoroutine;
    //private bool isCooling;


    void Awake()
    {
        target = transform.parent.GetComponentInChildren<Player>().transform.GetComponent<Rigidbody2D>();
        isMoving = false;
        fireCall = false;
        isFiring = false;   // 애니메이터에서 관리
        isMeleeing = false;
        //isCooling = false;
        nowCooledTime = fireCoolTime;

        rigidb = GetComponent<Rigidbody2D>();
        nextVecDelta = speed * Time.fixedDeltaTime;
        //spriter = GetComponent<Sprite>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLive)
        {
            return;
        }

        Vector2 dirVec = target.position - rigidb.position;
        Vector2 nextVec = dirVec.normalized * nextVecDelta; /*speed * Time.fixedDeltaTime*/;

        if (!isFiring) // 사격 애니메이션이 출력 중이 아님
        {
            if (fireCall) // 사격 명령 수신 상태
            {
                if (isMoving) // 이동 중인 경우 정지하고 다음 프레임으로
                {
                    isMoving = false;
                    animator.SetBool("isWalking", false);
                    //doPassThisFrame = true;
                }
                else // 정지 상태인 경우 사격
                {
                    Fire();
                    animator.SetTrigger("fire");

                }
            }
            else // 사격 명령 수신 전
            {
                if (fireCoolTime <= nowCooledTime) // 사격 쿨타임 X
                {
                    if (dirVec.magnitude <= fireRange) // 사거리 내로 진입 => 정지 && 사격 요청
                    {
                        isMoving = false;
                        fireCall = true;
                        animator.SetBool("isWalking", false);
                    }
                    else
                    {
                        Move(nextVec);
                    }
                }
                else
                {
                    Move(nextVec);
                }
            }

            if (fireCoolTime > nowCooledTime)
            {
                nowCooledTime += Time.fixedDeltaTime;
            }

        }
        else
        {

        }
;
    }


    public void Move(Vector2 nextVec)
    {
        if (nextVec.x > 0)
        {
            if (nextVec.y == 0)
            {
                moveDirZ = 0f;
            }
            else
            {
                moveDirZ = Mathf.Atan2(nextVec.y, nextVec.x) * radToDegree/*180 / Mathf.PI*/;
            }
        }
        else if (nextVec.x < 0)
        {
            if (nextVec.y == 0)
            {
                moveDirZ = 180f;
            }
            else
            {
                moveDirZ = -Mathf.Atan2(nextVec.y, -nextVec.x) * radToDegree/*180 / Mathf.PI*/ + 180f;
            }
        }
        else if (nextVec.x == 0)
        {
            if (nextVec.y > 0)
            {
                moveDirZ = 90f;
            }
            else if (nextVec.y < 0)
            {
                moveDirZ = -+90f;
            }
        }

        rigidb.SetRotation(moveDirZ);

        rigidb.MovePosition(rigidb.position + nextVec);

        rigidb.velocity = Vector2.zero;
        if (!isMoving)
        {
            animator.SetBool("isWalking", true);
        }
        return;
    }

    public void Fire(int weaponNumber = 0, int weaponType = 0, float projectileDelay = 0)
    {
        string weaponAnimationName = "";
        switch (weaponType)
        {
            case 0:
                {
                    weaponAnimationName = "fire";
                    animationCoroutine = StartCoroutine(DirectFiringProjactile(projectileDelay));
                    break;
                }
            default:
                {
                    weaponAnimationName = "fire";
                    break;
                }
        }
        animator.SetTrigger(weaponAnimationName);
    }

    public void OnFireAnimationStart()
    {
        if (isDebug)
            Debug.Log("Fire Start");
        isFiring = true;

    }

    public void OnFireAnimationEnd()
    {
        if (isDebug)
            Debug.Log("Fire End");
        isFiring = false;
        fireCall = false;
        nowCooledTime = -Time.fixedDeltaTime;
        //nowCooledTime = 0;
    }

    IEnumerator DirectFiringProjactile(float DelayTime)
    {
        yield return new WaitForSecondsRealtime(DelayTime);

        yield return null;
    }

}
