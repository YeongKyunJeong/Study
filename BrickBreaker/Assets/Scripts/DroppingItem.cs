using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Item
{
    None,
    PowerUp,
    LifeUp,
    MultiBall
}
//////////// #### To Do : Add more Item Logics
public class DroppingItem : MonoBehaviour, IObjectPool
{
    private static GameManager gameManager;
    [SerializeField] private Transform selfTransform;
    private int collidedObjectLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Item itemType;
    [SerializeField] private Rigidbody2D rigid;

    private static int itemTypeNumber;
    private static bool isGlobalReady = false;
    private static LayerMask paddleLayer;
    private static LayerMask deadZoneLayer;
    private static BrickData brickData;
    [SerializeField] private float fallingSpeedField = 1;
    private static Vector3 fallingSpeedVector;

    private bool isFirstTimeUsage = true;

    public void GlobalInitialize(BrickData givenBrickData, LayerMask givenPaddleLayer, LayerMask givenDeadZoneLayer)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("Droping item : GameManager not detected");
        }
        else
        {
            gameManager = GameManager.Instance;
            brickData = givenBrickData;
            paddleLayer = givenPaddleLayer;
            deadZoneLayer = givenDeadZoneLayer;
            fallingSpeedVector = fallingSpeedField * Time.fixedDeltaTime * Vector3.down;
            itemTypeNumber = brickData.itemTypeNumber + 1;
            isGlobalReady = true;
        }
    }

    public void SelfInitialize(Vector3 startPosition, Item targetItem)
    {
        if (isGlobalReady)
        {
            gameObject.SetActive(true);
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody2D>();
            }
            if (selfTransform == null)
            {
                selfTransform = transform;
            }

            if (isFirstTimeUsage)
                gameManager.ResetDroppingItemAction += DisableByReset;
            isFirstTimeUsage = false;

            selfTransform.position = startPosition;
            itemType = targetItem/*(Item)Random.Range(1, itemTypeNumber)*/;
            spriteRenderer.color = brickData.itemColors[(int)itemType];

        }
        else
        {
            Debug.Log("Droping item : GlobalInitialize not Done");
        }
    }

    /// <summary>
    /// //////////////////////////////// To Do: Add Item drop probability logic;
    /// </summary>

    private void FixedUpdate()
    {
        selfTransform.localPosition += fallingSpeedVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedObjectLayer = collision.gameObject.layer;
        if (collidedObjectLayer == paddleLayer)
        {
            Debug.Log(itemType.ToString());
            gameObject.SetActive(false);
            gameManager.ItemGettodaze(itemType);
        }
        else if (collidedObjectLayer == deadZoneLayer)
        {
            gameObject.SetActive(false);
        }
    }

    public void DisableByReset()
    {
        gameObject.SetActive(false);
    }

    public void GetInitialize()
    {
        
    }

    public void Release()
    {
        
    }
}