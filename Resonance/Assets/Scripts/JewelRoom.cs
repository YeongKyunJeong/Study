using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelRoom : MonoBehaviour
{
    public BoxCollider2D coll;
    public Vector2Int cord { get; set; }
    public Vector2 cordForCheck;
    public Jewel jewel { get; set; }
    public int state = 0; // -1: 이전 클릭에서 활성화 가능  0: 비활성화 / 1: 이번 클릭에서 활성화 / 2: 활성화 완료
    public int nextChainDir = 5; // 5: 클릭 불가, 1: ↙, 2: ↓. 3: ↘, 4: ←, 6: →, 7: ↖, 8: ↑, 9: ↗
    public SpriteRenderer spriteRenderer;
    //public Jewel jewel;
    //private void OnValidate()
    //{
    //    if (coll == null)
    //        coll = GetComponent<BoxCollider2D>();

    //}
    public void Initialize(Jewel jewel, Vector2Int cord)
    {
        if (coll == null)
            coll = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        this.jewel = jewel;
        this.cord = cord;
    }
}
