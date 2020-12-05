using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class CardMovementScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offSet;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public GameManagerScr GameManager;
    public bool IsDraggable;


    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
        GameManager = FindObjectOfType<GameManagerScr>();
    }

    // выполняется единожды когда мы берем карту
    public void OnBeginDrag(PointerEventData eventData)
    {
        offSet = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);

        DefaultParent = DefaultTempCardParent = transform.parent;

        IsDraggable = GameManager.IsPlayerTurn &&
            (
            (DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND &&
            GameManager.PlayerMana >= GetComponent<CardInfoScr>().SelfCard.Manacost) ||
            (DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_FIELD &&
            GetComponent<CardInfoScr>().SelfCard.CanAttack)
            );
            /*(DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND ||
                      DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_FIELD) &&
                      GameManager.IsPlayerTurn; */

        if (!IsDraggable)
            return;

        if (GetComponent<CardInfoScr>().SelfCard.CanAttack)

        GameManager.HighlightTargets(true);

        TempCardGO.transform.SetParent(DefaultParent);
        TempCardGO.transform.SetSiblingIndex(transform.GetSiblingIndex());

        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    // выполняется каждый кадр пока мы тянем карту
    public void OnDrag(PointerEventData eventData)
    {

        if (!IsDraggable)
            return;


        Vector3 newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        
        transform.position = newPos + offSet;

        if (TempCardGO.transform.parent != DefaultTempCardParent)
            TempCardGO.transform.SetParent(DefaultTempCardParent);

        if (DefaultParent.GetComponent<DropPlaceScr>().Type != FieldType.SELF_FIELD)
            CheckPosition();
    }

    // выполняется единожды когда отпускаем карту
    public void OnEndDrag(PointerEventData eventData)
    {

        if (!IsDraggable)
            return;

        GameManager.HighlightTargets(false);


        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

         
        transform.SetSiblingIndex(TempCardGO.transform.GetSiblingIndex()); // чтобы карта устанавливалась не только с краю, необходимо ей установить индек что и у временной карты

        TempCardGO.transform.SetParent(GameObject.Find("Canvas").transform);
        TempCardGO.transform.localPosition -= new Vector3(2833, 0);
    }
    // перемещение временной карты в зависимости от положения поднятой карты на экране
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

        TempCardGO.transform.SetSiblingIndex(newIndex); //новый индекс временной карте

    }

    public void MoveToField(Transform field)
    {
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.DOMove(field.position, .5f);
    }

    public void MoveToTarget(Transform target)
    {
        StartCoroutine(MoveToTargetCor(target));
    }

    IEnumerator MoveToTargetCor(Transform target)
    {
        Vector3 pos = transform.position;
        Transform parent = transform.parent;
        int index = transform.GetSiblingIndex();

        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;

        transform.SetParent(GameObject.Find("Canvas").transform);

        transform.DOMove(target.position, .25f);

        yield return new WaitForSeconds(.25f);

        transform.DOMove(pos, .25f);

        yield return new WaitForSeconds(.25f);

        transform.SetParent(parent);
        transform.SetSiblingIndex(index);

        transform.parent.GetComponent<HorizontalLayoutGroup>().enabled = false;
    }
}