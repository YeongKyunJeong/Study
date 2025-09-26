using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class Bread : PooledObject
    {
        bool isFirstInitiailze = false;

        [Header("For Bread On Tray")]
        private Vector3 defaultPos;
        private int myIndex;
        Coroutine movingCoroutine;

        public void SetBreadToBasket(Vector3 pos)
        {
            transform.position = pos;
            // To Do :: Add Effects
            return;

            //////////////
        }

        public void InitializeOnTray(Vector3 posOnTray, int i) 
        {
            defaultPos = posOnTray;
            myIndex = i;
        }

        public void SetBreadOnTray()
        {
            
        }

        public void PlayMoveToTrayAnimation(Vector3 startPos, float duration = 0.1f)
        {
            if(movingCoroutine != null) StopCoroutine(movingCoroutine);
            movingCoroutine = StartCoroutine(MoveAnimation(startPos, duration));
        }

        private IEnumerator MoveAnimation(Vector3 startPos, float duration)
        {
            float t0 = Time.time;
            while (true)
            {
                float u = (Time.time - t0) / duration;
                if (u >= 1f) break;
                transform.position = Vector3.LerpUnclamped(startPos, defaultPos, u);
                yield return null; // 할당 없음
            }
            transform.position = defaultPos;
        }
    }
}
