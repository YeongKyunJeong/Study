using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set; }
    [SerializeField] int initialHealth = 3;

    public int points = 100;

    public SpriteRenderer spriteRenderer { get; private set; }

    private static GameManager gameManager;

    private static LayerMask BallLayer = 0;
    private static int brickDamage = 0;
    private static Vector3 zeroVector = Vector3.zero;
    private static BrickData brickData;

    public int BrickDamagerSetter { get { return brickDamage; } set { brickDamage = value; } }
    [SerializeField] private Item fixedDropItem = Item.None;
    [SerializeField] private Item resultDropItem;

    [SerializeField] private bool isBreakable = true;
    private Transform selfTransform;
    private bool isBroken = false;

    private SFXType brickHitType = SFXType.BrickHit;
    private SFXType brickBreakType = SFXType.BrickBreak;

    public void Initialize(LayerMask givenBallLayer, int givenBrickDamage, BrickData givenBrickData, Item givenItem = Item.None)
    {
        if (gameManager == null)
        {
            gameManager = GameManager.Instance;
        }
        if (brickData == null)
        {
            brickData = givenBrickData;
        }
        if (BallLayer == 0)
        {
            BallLayer = givenBallLayer;
        }
        if (brickDamage == 0)
        {
            brickDamage = givenBrickDamage;
        }

        isBroken = false;
        selfTransform = transform;
        if (givenItem == Item.None)
            resultDropItem = fixedDropItem;
        else
            resultDropItem = givenItem;

        ResetBrick();

        health = initialHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = givenBrickData.brickSprites[health];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
            if (collision.gameObject.layer == BallLayer)
            {
                Hit();
            }
    }

    private void Hit()
    {
        health -= brickDamage;
        if (health > 0)
        {
            PlayBrickSFX(brickHitType);
            this.spriteRenderer.sprite = brickData.brickSprites[health];
            gameManager.HitBrick(points, zeroVector);

        }
        else
        {
            health = 0;
            this.gameObject.SetActive(false);
            PlayBrickSFX(brickBreakType);
            isBroken = true;
            gameManager.HitBrick(points, selfTransform.position, isBroken, resultDropItem);
        }
    }

    public void PlayBrickSFX(SFXType inputSFX)
    {
        gameManager.PlaySFX(inputSFX);
    }

    public void ResetBrick()
    {
        this.gameObject.SetActive(true);
        this.health = initialHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = brickData.brickSprites[health];
    }
}
