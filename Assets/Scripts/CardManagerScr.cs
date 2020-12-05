using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;


public struct Card
{
    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public bool CanAttack;
    public bool IsPlaced;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }

    public Card(string name, string logoPath, int attack, int defense, int manacost)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
        Manacost = manacost;
        CanAttack = false;
        IsPlaced = false;

    }

    public void ChageAttackState(bool can)
    {
        CanAttack = can;
    }

    public void GetDamage(int dmg)
    {
        Defense -= dmg;
    }

    
}
public static class CardManager
    {
        public static List<Card> AllCards = new List<Card>();
    }

public class CardManagerScr : MonoBehaviour
{
        public void Awake()
        {
            CardManager.AllCards.Add(new Card("zevs", "Sprites/Cards/zevs", 5, 5, 6));
            CardManager.AllCards.Add(new Card("poseidon", "Sprites/Cards/poseidon", 4, 3, 5));
            CardManager.AllCards.Add(new Card("gefest", "Sprites/Cards/gefest", 3, 3, 4));
            CardManager.AllCards.Add(new Card("afina", "Sprites/Cards/afina", 2, 1, 3));
            CardManager.AllCards.Add(new Card("germes", "Sprites/Cards/germes", 8, 1, 5));
            CardManager.AllCards.Add(new Card("artemida", "Sprites/Cards/artemida", 1, 1, 2));
        }
        
}
