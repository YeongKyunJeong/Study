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
    private Collider2D clickedColl;
    private int roomLayerMask;
    private Vector3 mouseClickedPosition;
    private bool isMouseHeld = false;
    private bool isActivating = false;
    private bool isDeactivating = false;
    private JewelRoom nowHeldJewel = null;
    private Vector2Int nowHeldJewelInd = -Vector2Int.one;

    private int tempInd = 0;
    public List<JewelRoom> thisTimeClickedRooms = new List<JewelRoom>();
    public List<JewelRoom> prevClickedRooms = new List<JewelRoom>();

    private Color[] selectableSignColors = new Color[2];

    private string isSelected = "isSelected";
    //private void OnValidate()
    //{
    //    if (DoArrangeJewel)
    //    {
    //        DoArrangeJewel = false;
    //        for (int i = 0; i < 7; i++)
    //        {
    //            for (int j = 0; j < 7; j++)
    //            {
    //                jewels[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
    //                //jewelRooms[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
    //                //jewelRooms[7 * i + j].cord = new Vector2Int(j, i);
    //                //jewelRooms[7 * i + j].cordForCheck = new Vector2(j, i);
    //                jewelRooms[7 * i + j].jewel = jewels[7 * i + j];
    //            }
    //        }
    //    }
    //}

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
                jewelRooms[7 * i + j].jewel = jewels[7 * i + j];

                jewels[7 * i + j].cord = new Vector2Int (j, i );
            }
        }
        selectableSignColors[0] = jewelRooms[0].spriteRenderer.color;
        Color temp = new Color(selectableSignColors[0].r, selectableSignColors[0].g, selectableSignColors[0].b, 0);
        selectableSignColors[1] = temp;

        UpdateSelectable();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isDeactivating) // 이번 클릭으로 보석을 비활성화 하지 않음
            {
                isMouseHeld = true;
                clickedColl = ShotRayAndDetectCollier();

                if (clickedColl != null) // 게임판을 클릭
                {
                    JewelRoom clickedRoom = clickedColl.GetComponent<JewelRoom>();
                    Debug.Log(clickedRoom.jewel.cord.x);
                    Debug.Log(clickedRoom.jewel.cord.y);
                    Debug.Log(clickedRoom.jewel.name);

                    if (nowHeldJewel == clickedRoom) // 클릭이 이전 보석을 벗어나지 않았다면 아무 것도 하지 않음
                        return;
                    else
                    {
                        nowHeldJewel = clickedRoom;
                    }

                    if (clickedRoom.state == 0)
                    {
                        isActivating = true;
                        ActivateJewel(clickedRoom);
                    }
                    else if (clickedRoom.state == 1)
                    {
                        Rollback(clickedRoom);// ToDo: 현재 마우스가 클릭 중인 보석 뒤 연결된, 이번 클릭에서 활성화된 보석을 취소
                    }
                    else
                    {
                        if (isActivating) // 이번 클릭에서 보석을 활성화 한 적이 있음
                        {
                            Rollback(clickedRoom); // ToDo:이번 클릭에서 활성화된 보석을 모두 취소
                        }
                        else
                        {
                            isDeactivating = true;
                            DeactivateJewel(clickedRoom);
                        }
                    }
                }
                else // 게임판 바깥을 클릭
                {
                    return;
                }
            }
            else // 이번 클릭으로 이미 보석을 비활성화 한 경우 마우스 클릭을 떼기 전까지 입력을 받지 않음
            {
                return;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ConfirmActivationJewel();
        }
    }
    Collider2D ShotRayAndDetectCollier()
    {
        mouseClickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseClickedPosition.z = 0;
        return Physics2D.OverlapPoint(mouseClickedPosition, roomLayerMask);
    }

    void ActivateJewel(JewelRoom jewelRoom)
    {
        thisTimeClickedRooms.Add(jewelRoom);
        jewelRoom.state = 1;
        jewelRoom.jewel.anim.SetBool(isSelected, true);
    }

    void ConfirmActivationJewel()
    {
        foreach (JewelRoom jewelRoom in thisTimeClickedRooms)
        {
            jewelRoom.state = 2;
        }

        prevClickedRooms.AddRange(thisTimeClickedRooms);
        thisTimeClickedRooms = new List<JewelRoom> { };

        nowHeldJewel = null;
        isMouseHeld = false;
        isActivating = false;
        isDeactivating = false;
    }

    void Rollback(JewelRoom jewelRoom)
    {
        tempInd = 0;
        if (!thisTimeClickedRooms.Contains(jewelRoom)) // 전부 초기화
        {
            isActivating = false;
            for (int i = tempInd; i < thisTimeClickedRooms.Count; i++)
            {
                DeactivateJewel(thisTimeClickedRooms[i]);
            }
            thisTimeClickedRooms = new List<JewelRoom> { };

        }
        else
        {
            tempInd = thisTimeClickedRooms.IndexOf(jewelRoom); // 현재 마우스가 올라가있는 보석 이후 활성화된 보석만 초기화
            for (int i = tempInd + 1; i < thisTimeClickedRooms.Count; i++)
            {
                DeactivateJewel(thisTimeClickedRooms[i]);
            }
            thisTimeClickedRooms = thisTimeClickedRooms.GetRange(0, tempInd + 1);
        }
    }

    void DeactivateJewel(JewelRoom jewelRoom)
    {
        jewelRoom.state = 0;
        jewelRoom.jewel.anim.SetBool(isSelected, false);
    }

    public void UpdateSelectable()
    {
        if (prevClickedRooms.Count == 0)
        {
            for (int i = 0; i < jewelRooms.Count; i++)
            {
                if (jewelRooms[i].jewel.cord.x == 0)
                {
                    jewelRooms[i].spriteRenderer.color = selectableSignColors[0];
                }
                else if (jewelRooms[i].jewel.cord.x == 6)
                {
                    jewelRooms[i].spriteRenderer.color = selectableSignColors[0];
                }
                else if (jewelRooms[i].jewel.cord.y == 0)
                {
                    jewelRooms[i].spriteRenderer.color = selectableSignColors[0];
                }
                else if (jewelRooms[i].jewel.cord.y == 6)
                {
                    jewelRooms[i].spriteRenderer.color = selectableSignColors[0];
                }
                else
                {
                    jewelRooms[i].spriteRenderer.color = selectableSignColors[1];
                }
            }
        }
        else /*if(nowHeldJewel)*/
        {

        }


    }
}
