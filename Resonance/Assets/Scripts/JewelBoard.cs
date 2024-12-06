using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelBoard : MonoBehaviour
{
    [SerializeField] private List<Jewel> jewels;
    [SerializeField] private List<Transform> jewelTransforms;
    [SerializeField] private List<JewelRoom> jewelRooms;
    public bool DoArrangeJewel = false;
    public float gap = 1.2f;
    public Camera mainCamera;
    private RaycastHit2D rayHit;
    private Collider2D clickedColl;
    private int roomLayerMask;
    private Vector3 mouseClickedPosition;

    private string isSelected = "isSelected";
    private void OnValidate()
    {
        if (DoArrangeJewel)
        {
            DoArrangeJewel = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    //jewels[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
                    //jewelRooms[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
                    //jewelRooms[7 * i + j].cord = new Vector2Int(j, i);
                    //jewelRooms[7 * i + j].cordForCheck = new Vector2(j, i);
                    jewelRooms[7 * i + j].jewel = jewels[0];
                }
            }
        }
    }

    private void Awake()
    {
        roomLayerMask = LayerMask.GetMask("Room");
    }

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                //jewels[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
                //jewelRooms[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
                //jewelRooms[7 * i + j].cord = new Vector2Int(j, i);
                //jewelRooms[7 * i + j].cordForCheck = new Vector2(j, i);
                jewelRooms[7 * i + j].jewel = jewels[0];
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseClickedPosition.z = 0;
            clickedColl = Physics2D.OverlapPoint(mouseClickedPosition, roomLayerMask);
            if (clickedColl != null)
            {
                JewelRoom clickedRoom = clickedColl.GetComponent<JewelRoom>();
                clickedRoom.jewel.anim.SetBool(isSelected, !clickedRoom.jewel.anim.GetBool(isSelected));
            }
        }
    }
}
