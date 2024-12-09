using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelRoom : MonoBehaviour
{
    public BoxCollider2D coll;
    public Vector2Int cord { get; set; }
    public Vector2 cordForCheck;
    public Jewel jewel { get; set; }
    public int state = 5; // 0: Deactivate / 1: Activated this click / 2: Activated
    public int nextChainDir = -1; // 5: 클릭 불가, 1: ↙, 2: ↓. 3: ↘, 4: ←, 6: →, 7: ↖, 8: ↑, 9: ↗
    public SpriteRenderer spriteRenderer;
    //public Jewel jewel;
    //private void OnValidate()
    //{
    //    if (coll == null)
    //        coll = GetComponent<BoxCollider2D>();

    //}
    private void Awake()
    {
        if (coll == null)
            coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
