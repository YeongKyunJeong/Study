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
    [SerializeField] private float nowCooledTime;
    //private bool isCooling;


    void Awake()
    {
        target = transform.parent.GetComponentInChildren<Player>().transform.GetComponent<Rigidbody2D>();
        isMoving = false;
        fireCall = false;
        isFiring = false;
        isMeleeing = false;
        //isCooling = false;
        nowCooledTime = fireCoolTime;

        rigidb = GetComponent<Rigidbody2D>();
        //spriter = GetComponent<Sprite>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dirVec = target.position - rigidb.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        // 이동
        if (!fireCall && !isFiring) // 비 사격상태
        {
            if (dirVec.magnitude >= 2)
            {
                if (!isMoving)
                {
                    isMoving = true;
                    animator.SetBool("isWalking", true);
                }
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    animator.SetBool("isWalking", false);
                }
            }
        }

        if(fireCoolTime <= nowCooledTime)
        {
            if(dirVec.magnitude <= fireRange)
            {
                isMoving = false;
                fireCall = true;
                animator.SetBool("isWalking", false);
            }
        }

        if (fireCall && !isFiring)
        {
            //Debug.Log("Done");
            animator.SetTrigger("fire");
            fireCall = false;
            nowCooledTime = 0;
        }
        else if(fireCoolTime > nowCooledTime)
        {
            nowCooledTime += Time.deltaTime;
        }



        // 사격
        //if (fireCoolTime <= nowCooledTime) // 사격 가능 상태
        //{
        //    if (!isFiring && dirVec.magnitude <= fireRange) // 사거리 내에 들어옴
        //    {
        //        isFiring = true;

        //        if (isMoving)   // 이동 중이면 사격 신호를 보내고 이동 중지
        //        {
        //            isMoving = false;
        //            animator.SetBool("isWalking", false);
        //        }
        //    }
        //    else if (isFiring && !isMoving) // 이동 중이 아니면 사격 후 쿨타임 적용
        //    {
        //        animator.SetTrigger("fire");
        //        nowCooledTime = 0;
        //    }
        //}
        //else if (fireCoolTime > nowCooledTime) // 사격 쿨타임 상태
        //{
        //    //if (!isCooling)
        //    //{
        //    //    isCooling = true;
        //    //}
        //    nowCooledTime += Time.fixedDeltaTime;
        //}


        if (nextVec.x > 0)
        {
            if (nextVec.y == 0)
            {
                moveDirZ = 0f;
            }
            else
            {
                moveDirZ = Mathf.Atan2(nextVec.y, nextVec.x) * 180 / Mathf.PI;
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
                moveDirZ = -Mathf.Atan2(nextVec.y, -nextVec.x) * 180 / Mathf.PI + 180f;
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

        if (isMoving)
        {
            rigidb.MovePosition(rigidb.position + nextVec);
        }
        rigidb.velocity = Vector2.zero;
    }
}
