using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public GameManagerScr GameManager;
    public bool IsDraggable;

    AudioSource AudioDrag;
    
    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
        GameManager = FindObjectOfType<GameManagerScr>();
        AudioDrag = GetComponent<AudioSource>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.localScale = new Vector2(transform.localScale.x * 2.5f, transform.localScale.y * 2.5f);
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        
        DefaultParent = DefaultTempCardParent = transform.parent;

        IsDraggable = DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND ||
                      DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_FIELD &&
                      GameManager.IsPlayerTurn;

        if (!IsDraggable)
            return;

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        AudioDrag.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable)
            return;

        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;
        

        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);
        if (DefaultParent.GetComponent<DropPlaceScr>().Type !=FieldType.SELF_FIELD)
            CheckPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = new Vector2(transform.localScale.x / 2.5f, transform.localScale.y / 2.5f);
        if (!IsDraggable)
            return;

        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex());

        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition = new Vector3(2059, 0);
    }

    void CheckPosition()
    {
        int newIndex = DefaultTempCardParent.childCount;

        for (int i = 0; i < DefaultTempCardParent.childCount; i++)
        {
            if (transform.position.x < DefaultTempCardParent.GetChild(i).position.x)
            {

                newIndex = i;

                if (TempCardGO.transform.GetSiblingIndex() < newIndex)
                    newIndex--;
                break;

            }
        }

        TempCardGO.transform.SetSiblingIndex(newIndex);

    }
}
