using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AttackHero : MonoBehaviour, IDropHandler
{
    
    public enum  HeroType
    {
        ENEMY,
        PLAYER
    }

    public HeroType Type;
    public GameManagerScr GameManager;
    public Color NormalCol, TargetCol;




    public void OnDrop(PointerEventData eventData)
    {
        if (!GameManager.IsPlayerTurn)
            return;

        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();

        if (card &&
            card.SelfCard.CanAttack &&
            Type == HeroType.ENEMY)

        {
            card.SelfCard.CanAttack = false;
            GameManager.DamageHero(card, true);
        }


    }
    // change card's highlight that we can attacked
    public void HighlightAsTarget(bool highlight)
    {
        GetComponent<Image>().color = highlight ?
                                      TargetCol :
                                      NormalCol;

    }

}
