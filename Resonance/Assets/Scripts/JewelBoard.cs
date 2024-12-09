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
    private int enabledRoomLayer;
    private int enabledRoomLayerMask;
    private int disabledRoomLayer;
    private Vector3 mouseClickedPosition;
    private bool isMouseHeld = false;

    private bool updateJewel = false;
    private bool isActivating = false;
    private bool isDeactivating = false;
    public JewelRoom nowHeldJewel = null;
    private Vector2Int nowHeldJewelInd = -Vector2Int.one;

    private int tempInd = 0; // int값 계산용
    private int xDiff = 0;
    private int yDiff = 0;

    [SerializeField]
    private int chainDir = 5; // 5: 클릭 불가, 1: ↙, 2: ↓. 3: ↘, 4: ←, 6: →, 7: ↖, 8: ↑, 9: ↗
    [SerializeField]
    private List<int> prevChainDirHistory = new List<int>() { };
    [SerializeField]
    private List<int> thisTimeChainDirHistory = new List<int>() { };

    public List<JewelRoom> thisTimeClickedRooms = new List<JewelRoom>();
    public List<JewelRoom> prevClickedRooms = new List<JewelRoom>();
    //public JewelRoom beforeHeldJewel = null;

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
        enabledRoomLayerMask = LayerMask.GetMask("EnabledRoom");
        enabledRoomLayer = LayerMask.NameToLayer("EnabledRoom");
        disabledRoomLayer = LayerMask.NameToLayer("DisabledRoom");
        chainDir = 5;
        prevChainDirHistory = new List<int>() { };
    }

    private void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                //jewels[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
                //jewelRooms[7 * i + j].transform.position = new Vector2(-3.6f + gap * j, 3.6f - gap * i);
                //jewelRooms[7 * i + j].cordForCheck = new Vector2(j, i);
                jewelRooms[7 * i + j].jewel = jewels[7 * i + j];
                jewelRooms[7 * i + j].cord = new Vector2Int(j, i);

                jewels[7 * i + j].cord = new Vector2Int(j, i);
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
                    updateJewel = true;
                    JewelRoom clickedRoom = clickedColl.GetComponent<JewelRoom>();

                    if (nowHeldJewel == clickedRoom) // 클릭이 이전 보석을 벗어나지 않았다면 아무 것도 하지 않음
                        return;
                    else
                    {
                        //beforeHeldJewel = nowHeldJewel;
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

        if (updateJewel)
            if (Input.GetMouseButtonUp(0))
            {
                ConfirmActivationJewel();
            }
    }
    Collider2D ShotRayAndDetectCollier()
    {
        mouseClickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseClickedPosition.z = 0;
        return Physics2D.OverlapPoint(mouseClickedPosition, enabledRoomLayerMask);
    }

    void ActivateJewel(JewelRoom jewelRoom)
    {
        thisTimeClickedRooms.Add(jewelRoom);
        jewelRoom.state = 1;
        jewelRoom.jewel.anim.SetBool(isSelected, true);

        chainDir = jewelRoom.nextChainDir;
        thisTimeChainDirHistory.Add(chainDir);

        UpdateSelectable();
    }

    void ConfirmActivationJewel()
    {
        foreach (JewelRoom jewelRoom in thisTimeClickedRooms)
        {
            jewelRoom.state = 2;
        }

        UpdateSelectable();
        prevClickedRooms.AddRange(thisTimeClickedRooms);
        thisTimeClickedRooms = new List<JewelRoom> { };
        prevChainDirHistory.AddRange(thisTimeChainDirHistory);
        thisTimeChainDirHistory = new List<int> { };

        //beforeHeldJewel = nowHeldJewel;
        updateJewel = false;
        nowHeldJewel = null;
        isMouseHeld = false;
        isActivating = false;
        isDeactivating = false;
    }
    void DeactivateJewel(JewelRoom jewelRoom) // 이전 클릭에 활성화된 보석을 비활성화 시키는 경우
    {
        // 이어진 체인을 모두 찾아서 비활성화
        tempInd = prevClickedRooms.IndexOf(jewelRoom);

        for (int i = tempInd; i < prevClickedRooms.Count; i++)
        {
            CancelActivationofJewel(prevClickedRooms[i]);
        }

        prevClickedRooms = prevClickedRooms.GetRange(0, tempInd);
        prevChainDirHistory = prevChainDirHistory.GetRange(0, tempInd);

        if (tempInd == 0)
            nowHeldJewel = null;

        else
            nowHeldJewel = prevClickedRooms[tempInd - 1];

        UpdateSelectable();
    }

    void Rollback(JewelRoom jewelRoom)
    {
        tempInd = 0;
        if (!thisTimeClickedRooms.Contains(jewelRoom)) // 전부 초기화
        {
            isActivating = false;
            chainDir = prevChainDirHistory[prevChainDirHistory.Count - 1];
            for (int i = 0; i < thisTimeClickedRooms.Count; i++)
            {
                CancelActivationofJewel(thisTimeClickedRooms[i]);
            }
            thisTimeClickedRooms = new List<JewelRoom> { };
            thisTimeChainDirHistory = new List<int> { };
            nowHeldJewel = prevClickedRooms[prevClickedRooms.Count - 1];

        }
        else
        {
            tempInd = thisTimeClickedRooms.IndexOf(jewelRoom); // 현재 마우스가 올라가있는 보석 이후 활성화된 보석만 초기화
            chainDir = thisTimeChainDirHistory[tempInd];
            nowHeldJewel = jewelRooms[tempInd];
            for (int i = tempInd + 1; i < thisTimeClickedRooms.Count; i++)
            {
                CancelActivationofJewel(thisTimeClickedRooms[i]);
            }
            thisTimeClickedRooms = thisTimeClickedRooms.GetRange(0, tempInd + 1);
            thisTimeChainDirHistory = thisTimeChainDirHistory.GetRange(0, tempInd + 1);


            nowHeldJewel = jewelRoom;
        }

        UpdateSelectable();
    }

    void CancelActivationofJewel(JewelRoom jewelRoom) // 이번 클릭에 활성화된 보석을 되돌리는 경우
    {
        jewelRoom.state = 0;
        jewelRoom.jewel.anim.SetBool(isSelected, false);
    }



    public void UpdateSelectable()
    {
        if (prevClickedRooms.Count == 0 && thisTimeClickedRooms.Count == 0)
        {
            for (int i = 0; i < jewelRooms.Count; i++)
            {
                JewelRoom targetRoom = jewelRooms[i];
                if (targetRoom.jewel.cord.x == 0)
                {

                    MakeSelectable(targetRoom, true);

                    if (targetRoom.jewel.cord.y == 0)
                    {
                        targetRoom.nextChainDir = 3;
                    }
                    else if (targetRoom.jewel.cord.y == 6)
                    {
                        targetRoom.nextChainDir = 9;
                    }
                    else
                    {
                        targetRoom.nextChainDir = 6;
                    }
                }
                else if (targetRoom.jewel.cord.x == 6)
                {
                    MakeSelectable(targetRoom, true);

                    if (targetRoom.jewel.cord.y == 0)
                    {
                        targetRoom.nextChainDir = 1;
                    }
                    else if (targetRoom.jewel.cord.y == 6)
                    {
                        targetRoom.nextChainDir = 7;
                    }
                    else
                    {
                        targetRoom.nextChainDir = 4;
                    }
                }
                else if (targetRoom.jewel.cord.y == 0)
                {
                    MakeSelectable(targetRoom, true);
                    targetRoom.nextChainDir = 2;
                }
                else if (targetRoom.jewel.cord.y == 6)
                {
                    MakeSelectable(targetRoom, true);
                    targetRoom.nextChainDir = 8;
                }
                else
                {
                    MakeSelectable(targetRoom, false);
                    targetRoom.nextChainDir = 5;
                }
            }
        }
        else
        {
            for (int i = 0; i < jewelRooms.Count; i++)
            {
                JewelRoom targetRoom = jewelRooms[i];

                if (prevClickedRooms.Contains(targetRoom))
                {
                    targetRoom.nextChainDir = prevChainDirHistory[prevClickedRooms.IndexOf(targetRoom)];
                }
                else if (thisTimeClickedRooms.Contains(targetRoom))
                {
                    targetRoom.nextChainDir = thisTimeChainDirHistory[thisTimeClickedRooms.IndexOf(targetRoom)];
                }
                else
                {
                    xDiff = targetRoom.cord.x - nowHeldJewel.cord.x;
                    yDiff = targetRoom.cord.y - nowHeldJewel.cord.y;
                    switch (chainDir)
                    {
                        case 6:
                            {
                                if (xDiff != 1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else if (Mathf.Abs(yDiff) > 1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, true);
                                    targetRoom.nextChainDir = 6;
                                }
                                break;
                            }
                        case 4:
                            {
                                if (xDiff != -1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else if (Mathf.Abs(yDiff) > 1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, true);
                                    targetRoom.nextChainDir = 4;
                                }
                                break;
                            }
                        case 2:
                            {
                                if (yDiff != 1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else if (Mathf.Abs(xDiff) > 1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, true);
                                    targetRoom.nextChainDir = 2;
                                }
                                break;
                            }
                        case 8:
                            {
                                if (yDiff != -1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else if (Mathf.Abs(xDiff) > 1)
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, true);
                                    targetRoom.nextChainDir = 8;
                                }
                                break;
                            }
                        case 3:
                            {
                                if (xDiff == 1)
                                {
                                    if (yDiff == 1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 3;
                                    }
                                    else if (yDiff == 0 || yDiff == -1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 6;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else if (yDiff == 1)
                                {
                                    if (xDiff == 0 || xDiff == -1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 2;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }

                                break;
                            }
                        case 1:
                            {
                                if (xDiff == -1)
                                {
                                    if (yDiff == -1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 1;
                                    }
                                    else if (yDiff == 0 || yDiff == 1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 4;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else if (yDiff == 1)
                                {
                                    if (xDiff == 0 || xDiff == 1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 2;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }

                                break;
                            }
                        case 9:
                            {
                                if (xDiff == 1)
                                {
                                    if (yDiff == -1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 9;
                                    }
                                    else if (yDiff == 0 || yDiff == 1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 6;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else if (yDiff == -1)
                                {
                                    if (xDiff == 0 || xDiff == -1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 8;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }

                                break;
                            }
                        case 7:
                            {
                                if (xDiff == -1)
                                {
                                    if (yDiff == -1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 7;
                                    }
                                    else if (yDiff == 0 || yDiff == 1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 4;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else if (yDiff == -1)
                                {
                                    if (xDiff == 0 || xDiff == 1)
                                    {
                                        MakeSelectable(targetRoom, true);
                                        targetRoom.nextChainDir = 8;
                                    }
                                    else
                                    {
                                        MakeSelectable(targetRoom, false);
                                        //targetRoom.nextChainDir = 5;
                                    }
                                }
                                else
                                {
                                    MakeSelectable(targetRoom, false);
                                    //targetRoom.nextChainDir = 5;
                                }

                                break;
                            }
                        default:
                            break;
                    }
                }

            }
        }
    }

    private void MakeSelectable(JewelRoom targetRoom, bool toSelectable = true, bool isChained = false)
    {

        if (toSelectable)
        {
            targetRoom.spriteRenderer.color = selectableSignColors[0];
            targetRoom.gameObject.layer = enabledRoomLayer;
        }
        else
        {
            targetRoom.spriteRenderer.color = selectableSignColors[1];
            targetRoom.gameObject.layer = disabledRoomLayer;
        }
    }
}
