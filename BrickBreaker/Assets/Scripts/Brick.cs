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
    private bool doesGameManagerExists = true;


    public void Initialize(LayerMask ballLayer, BrickData brickData, StageManager stageManager = null)
    {
        this.brickData = brickData;
        this.ballLayer = ballLayer;
        health = initialHealth;
        
        if(stageManager != null)
        {
            this.stageManager = stageManager;
            doesGameManagerExists = false;
        }
        else
        {
            gameManager = GameManager.Instance;
        }

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
        if (doesGameManagerExists)
        {
            gameManager.ScoreUp(points);
        }
        else
        {
            stageManager.TempScoreUp(points);
        }
        if (health > 1)
        {
            this.health--;
            this.spriteRenderer.sprite = brickData.brickSprites[health];
        }
        else
        {
            this.gameObject.SetActive(false);
            
        }
    }
}
