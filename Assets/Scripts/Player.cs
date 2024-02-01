using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody2D rigid;
    Transform boostEffectParent;
    Transform boostEffect;
    public float speed;
    public float boostEffectSize;
    public float moveDirZ;
    public float boostDist;

    [Space]
    public DialogueManager dialogueManager;



    SpriteRenderer spriteR;

    Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        boostEffectParent = GetComponentsInChildren<Transform>()[1];
        boostEffect = boostEffectParent.GetComponentsInChildren<Transform>()[1];
        Debug.Log("Boost Name : " + boostEffect.name);
    }

    void Start()
    {

    }

    // Update is called once per frame
    // 1. 직접 스크립팅
    //void Update()
    //{
        //inputVec.x = Input.GetAxis("Horizontal"); // 부드럽게 움직임
        //inputVec.y = Input.GetAxis("Vertical");

        //inputVec.x = Input.GetAxisRaw("Horizontal"); // 1, 0, -1로 끊어져서 움직임;
        //inputVec.y = Input.GetAxisRaw("Vertical");

        //inputVec.Normalize();


    //}


    private void FixedUpdate()
    {
        // 1. 힘을 줌
        // rigid.AddForce(inputVec);

        // 2. 속도 제어
        // rigid.velocity = inputVec;

        // 3. 위치 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        if (nextVec.x > 0)
        {
            if (nextVec.y == 0)
            {
                moveDirZ = 0f;
            }
            else
            {
                moveDirZ = Mathf.Atan2(nextVec.y, nextVec.x) * 180 / Mathf.PI ;
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
                moveDirZ =-+90f;
            }
        }
        boostEffectSize = Mathf.Sqrt(nextVec.magnitude) * 3f;
        //boostEffectConstant = nextVec.magnitude*10f;
        boostDist = boostEffectSize;
        rigid.MovePosition(rigid.position + nextVec);
        boostEffect.localScale = new Vector3(boostEffectSize, 0.5f + 0.2f * boostEffectSize, 1);
        boostEffect.localPosition = new Vector3(boostDist / 2 + 0.5f, 0, 0);

        boostEffectParent.rotation = Quaternion.Euler(0f, 0f, moveDirZ+180f);
        rigid.rotation = moveDirZ;
    }

     //2. InputSytem 사용
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>(); // nomalize는 Player Input에서 설정
    }

    // 좌우 대칭만 사용할 경우
    //private void LateUpdate()
    //{
    //    if (inputVec.x != 0)
    //    {
    //        //spriteR.flipX = inputVec.x < 0;
    //    }
    //}
}
