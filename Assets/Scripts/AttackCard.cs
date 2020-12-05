using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class AttackCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (!GetComponent<CardMovementScr>().GameManager.IsPlayerTurn)
            return; // чтобы карты не могли ходить если не ход игрока

        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();

        if (card &&
            card.SelfCard.CanAttack &&
            transform.parent == GetComponent<CardMovementScr>().GameManager.EnemyField)
        {
            card.SelfCard.ChageAttackState(false);

            if (card.IsPlayer)
                card.DeHighlightedCard();

            GetComponent<CardMovementScr>().GameManager.CardsFight(card, GetComponent<CardInfoScr>());
        }


    }

    
}
