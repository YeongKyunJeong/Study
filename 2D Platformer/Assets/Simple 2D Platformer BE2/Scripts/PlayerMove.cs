using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    int platformLayerMask;
    [SerializeField]
    int playerDamagedLayerNum;
    int playerBaseLayerNum;
    float immuneTime;
    Color playerDamagedColor = new Color(1, 1, 1, 0.4f);
    //bool isInAir = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        if (maxSpeed < 1)
            maxSpeed = 4.5f;
        if (jumpPower < 1)
            jumpPower = 20;
        platformLayerMask = LayerMask.GetMask("Platform");
        if (immuneTime < 1)
        {
            immuneTime = 3.0f;
        }
        playerDamagedLayerNum = LayerMask.NameToLayer("PlayerDamaged");
        playerBaseLayerNum = gameObject.layer;
    }

    private void Update() // 단발적인 키 입력
    {
        // Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            anim.SetBool("isUp", true);
        }

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

        // Landing Platform
        //Debug.DrawRay(rigid.position, 0.6f * Vector3.down, Color.green);


        if (rigid.velocity.y <= 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.6f, platformLayerMask);
            if (rayHit.collider != null)
            {
                anim.SetBool("isJumping", false);
            }
            else if (!anim.GetBool("isJumping"))
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isUp", false);
            }
            else if (anim.GetBool("isUp"))
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isUp", false);
            }
        }
        else
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (rigid.velocity.y < 0 && (transform.position.y > collision.transform.position.y))
            {
                OnAttack(collision.transform);
                GameManager.instance.stagePoint += 100;
            }
            else
                StartCoroutine(DamagedCoroutine(collision.transform.position));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (collision.gameObject.name.Contains("Bronze Coin"))
                GameManager.instance.stagePoint += 50;
            else if (collision.gameObject.name.Contains("Silver Coin"))
                GameManager.instance.stagePoint += 100;
            else if (collision.gameObject.name.Contains("Gold Coin"))
                GameManager.instance.stagePoint += 200;


            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Finish")
        {

        }
    }

    void OnAttack(Transform enemy)
    {
        // Point

        // Reaction Force;
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
        // Enemy Die
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
    }


    IEnumerator DamagedCoroutine(Vector2 enemyPos)
    {
        gameObject.layer = playerDamagedLayerNum;
        int dirc = transform.position.x - enemyPos.x > 0 ? 1 : -1;
        rigid.velocity = new Vector2(rigid.velocity.x, 5);
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        anim.SetBool("isImmuned", true);
        anim.SetTrigger("isDamaged");

        yield return new WaitForSeconds(immuneTime);
        gameObject.layer = playerBaseLayerNum;
        anim.SetBool("isImmuned", false);

        yield return null;

    }

    void Damaged()
    {

    }
}
