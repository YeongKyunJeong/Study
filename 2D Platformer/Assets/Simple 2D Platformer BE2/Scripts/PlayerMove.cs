using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (maxSpeed < 1)
            maxSpeed = 4.5f;
    }

    private void Update() // 단발적인 키 입력
    {
        // Stop Speed
        if (Input.GetButtonUp("Horizontal"))    //*** 정지 조작감 수정 필요, a와 d가 동시에 눌렸을 때 상황 고려 필요
        {
            //rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            rigid.velocity = new Vector2(0.1f * rigid.velocity.x, rigid.velocity.y);
        }

        // Direction Sprtie
        if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            anim.SetBool("isWalking", true);    // 입력 기준 애니메이션 변경
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            anim.SetBool("isWalking", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //if(Mathf.Abs(rigid.velocity.x) <= 0.3f) // 속도 기준 애니메이션 변경
        //{
        //    anim.SetBool("isWalking", false);
        //}
        //else
        //{
        //    anim.SetBool("isWalking", true);
        //}

    }

    private void FixedUpdate() // 지속적인 키 입력
    {
        // Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // Max Speed
        if (rigid.velocity.x > maxSpeed) // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed) // Right Max Speed
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
    }
}
