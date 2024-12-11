using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    private JewelData jewelData;
    public Vector2Int cord /*{ get; set; }*/;
    public Animator anim /*{ get; set; }*/;
    public SpriteRenderer spriteRenderer /*{ get; set; }*/;
    private Coroutine animationCoroutine = null;
    private string pop = "pop";
    public WaitForSeconds popAnimationTime;

    public void Initialize(JewelData jewelData)
    {
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        this.jewelData = jewelData;
        popAnimationTime = jewelData.popAnimationWaitforSecond;
    }

    public void ChangeJewelSprite(bool isPop = true, int targetID = -1)
    {
        if(animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        
        animationCoroutine = StartCoroutine(SpriteChangeCoroutine(isPop, targetID));
    }

    IEnumerator SpriteChangeCoroutine(bool isPop, int targetID)
    {
        if (isPop) // 보석이 터지는 이펙트
        {
            anim.SetTrigger(pop);
            
            yield return popAnimationTime;
        }
        


        yield return null;
    }

    
    
}
