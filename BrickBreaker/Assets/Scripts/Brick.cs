using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set; }
    [SerializeField] int initialHealth = 3;

    public int points = 100;

    public SpriteRenderer spriteRenderer { get; private set; }

    [SerializeField] private bool isBreakable = true;
    private GameManager gameManager;

    private Transform selfTransform;
    private LayerMask ballLayer;
    private BrickData brickData;
    private bool isBroken = false;

    private SFXType brickHitType = SFXType.BrickHit;
    private SFXType brickBreakType = SFXType.BrickBreak;

    public void Initialize(LayerMask ballLayer, BrickData brickData)
    {
        this.brickData = brickData;
        this.ballLayer = ballLayer;
        isBroken = false;
        selfTransform = transform;

        gameManager = GameManager.Instance;

        ResetBrick();

        health = initialHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = brickData.brickSprites[health];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
            if (collision.gameObject.layer == ballLayer)
            {
                Hit();
            }
    }

    private void Hit()
    {
        if (health > 1)
        {
            this.health--;
            PlayBrickSFX(brickHitType);
            this.spriteRenderer.sprite = brickData.brickSprites[health];
        }
        else
        {
            this.gameObject.SetActive(false);
            PlayBrickSFX(brickBreakType);
            isBroken = true;
        }
        gameManager.ScoreUpAndCreateItem(points, selfTransform.position,isBroken);

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
