using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private LayerMask ballLayer;
    private bool doesGameManagerExist = true;
    private StageManager stageManager;
    private GameManager gameManager;
    [SerializeField] private bool isBelowWall = false;

    public void Initialize(LayerMask ballLayer, StageManager stageManager = null)
    {
        if (stageManager != null)
        {
            doesGameManagerExist = false;
            this.stageManager = stageManager;
        }
        else
        {
            gameManager = GameManager.Instance;
        }
        this.ballLayer = ballLayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBelowWall)
            if (collision.gameObject.layer == ballLayer)
            {
                if (doesGameManagerExist)
                {
                    gameManager.DeadZoneOut();
                }
                else
                {
                    stageManager.TempDeadZoneOut();
                }
            }
    }
}
