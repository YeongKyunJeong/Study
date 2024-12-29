using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health { get; private set; }
    [SerializeField] int initialHealth = 3;

    public int points = 100;

    public SpriteRenderer spriteRenderer { get; private set; }

    [SerializeField] private bool isBreakable = true;
    GameManager gameManager;

    private LayerMask ballLayer;
    private BrickData brickData;
    private StageManager stageManager;
    private bool isBroken = false;
    private bool doesGameManagerExists = true;

    private SFXType brickHitType = SFXType.BrickHit;
    private SFXType brickBreakType = SFXType.BrickBreak;

    public void Initialize(LayerMask ballLayer, BrickData brickData, StageManager stageManager = null)
    {
        this.brickData = brickData;
        this.ballLayer = ballLayer;
        isBroken = false;

        if (stageManager != null)
        {
            this.stageManager = stageManager;
            doesGameManagerExists = false;
        }
        else
        {
            gameManager = GameManager.Instance;
        }

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

        if (doesGameManagerExists)
        {
            gameManager.ScoreUp(points, isBroken);
        }
        else
        {
            stageManager.TempScoreUp(points, isBroken);
        }
    }

    public void PlayBrickSFX(SFXType inputSFX)
    {
        if (doesGameManagerExists)
        {
            gameManager.PlaySFX(inputSFX);
        }
        else
        {
            stageManager.PlaySFX(inputSFX);
        }
    }
    
    public void ResetBrick()
    {
        this.gameObject.SetActive(true);
        this.health = initialHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = brickData.brickSprites[health];
    }
}
