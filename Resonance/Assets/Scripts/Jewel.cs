using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    public Vector2Int cord { get; set; }
    public Animator anim { get; set; }

    private void Awake()
    {
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }
}
