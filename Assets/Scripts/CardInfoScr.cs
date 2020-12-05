using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfoScr : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public TextMeshProUGUI Name, Attack, Defense, Manacost;
    public GameObject HideObj, HighlitedObj; // объявили объект рубашки и HighlitedObj подсветку карт 
    public bool IsPlayer;
    public Color NormalCol, TargetCol;

    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        HideObj.SetActive(true);
        IsPlayer = false;
        Manacost.text = ""; // чтобы мы не видели сколько маны стоит карта противника
        
    }

    public void ShowCardInfo(Card card, bool isPlayer)
    {
        IsPlayer = isPlayer;
        HideObj.SetActive(false);
        SelfCard = card;


        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;

        RefreshData();
    }

    public void RefreshData()
    {
        Attack.text = SelfCard.Attack.ToString();
        Defense.text = SelfCard.Defense.ToString();
        Manacost.text = SelfCard.Manacost.ToString();
    }

    // card's highlight 
    public void HighlightCard()
    {
        HighlitedObj.SetActive(true);
    }

    public void DeHighlightedCard()
    {
        HighlitedObj.SetActive(false);
    }

    public void CheckForAvaliability(int currentMana)
    {
        GetComponent<CanvasGroup>().alpha = currentMana >= SelfCard.Manacost ?
                                            1 :
                                            .5f;
    }

    // change card's highlight that we can attacked
    public void HighlightAsTarget(bool highlight)
    {
        GetComponent<Image>().color = highlight ?
                                      TargetCol :
                                      NormalCol;

    }
}