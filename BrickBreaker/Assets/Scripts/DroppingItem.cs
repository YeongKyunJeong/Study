using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Item
{
    type1,
    type2
}

public class DroppingItem : MonoBehaviour
{
    private GameManager gameManager;
    private Transform selfTransform;
    private int collidedObjectLayer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Item itemType;
    public bool isEnable;

    private static int itemTypeNumber;
    private static bool isGlobalReady = false;
    private static LayerMask paddleLayer;
    private static LayerMask deadZoneLayer;
    private static BrickData brickData;
    [SerializeField] private float fallingSpeedField = 1;
    private static Vector3 fallingSpeedVector;

    public void GlobalInitialize(BrickData givenBrickData, LayerMask givenPaddleLayer, LayerMask givenDeadZoneLayer)
    {
        if (gameManager == null)
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
            itemTypeNumber = Enum.GetValues(typeof(Item)).Length;
            isGlobalReady = true;
        }
    }

    public void SelfInitialize(Vector3 startPosition)
    {
        if (isGlobalReady)
        {
            isEnable = true;
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            selfTransform = transform;
            selfTransform.position = startPosition;
            itemType = (Item)Random.Range(0, itemTypeNumber);

        }
        else
        {
            Debug.Log("Droping item : GlobalInitialize not Done");
        }
    }

    private void FixedUpdate()
    {
        selfTransform.localPosition += fallingSpeedVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidedObjectLayer = collision.gameObject.layer;
        isEnable = false;
        gameObject.SetActive(false);
        if (collidedObjectLayer == paddleLayer)
        {
            gameManager.ItemGettodaze(itemType);
        }
        else if (collidedObjectLayer == deadZoneLayer)
        {
        }
    }
}