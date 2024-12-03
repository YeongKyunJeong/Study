using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    //GameManager.instance.player; // 메모리에 바로 올렸기 때문에 클래스에서 직접 부를 수 있음
    //Trigger가 check된 collider에서 나갔을때 호출

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        Vector3 playerPosition = GameManager.instance.playerTransform.position;
        Vector3 myPosition = transform.position;

        switch (transform.tag)
        {
            case "Field":
                {

                    float distX = playerPosition.x - myPosition.x;
                    float distY = playerPosition.y - myPosition.y;
                    float dirX = distX < 0 ? -1 : 1;
                    float dirY = distY < 0 ? -1 : 1;
                    distX = Mathf.Abs(distX);
                    distY = Mathf.Abs(distY);

                    //if(MathF.Abs(distX - distY) <= 0.1f)
                    //{
                    //    transform.Translate(Vector3.right * dirX * 60);
                    //    transform.Translate(Vector3.up * dirY * 60);

                    //}
                    //else 
                    if (distX > distY)
                    {
                        transform.Translate(Vector3.right * dirX * 60);
                    }
                    else if (distX < distY)
                    {
                        transform.Translate(Vector3.up * dirY * 60);
                    }
                    else if(distX == distY)
                    {
                        transform.Translate((Vector3.right * dirX + Vector3.up * dirY) * 60);
                    }
                    break;
                }
            case "Enemy":
                {
                    break;
                }
        }

    }




}
