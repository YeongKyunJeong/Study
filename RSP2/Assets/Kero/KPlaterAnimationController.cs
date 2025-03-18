using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class KPlaterAnimationController : MonoBehaviour
    {
        KPlayerInput kPlayerInput;
        Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            kPlayerInput = GetComponent<KPlayerInput>();
            kPlayerInput.MoveEvent += OnMove;
            
            animator = GetComponentInChildren<Animator>();
        }

        private void OnMove(Vector2 obj)
        {
            // animator.SetFloat("Horizontal");
        }
    }
}
