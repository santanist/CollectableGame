using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public Sprite Logo;
    public int Attack, Defense;
    public bool CanAttack;

    public Card(string logoPath, int attack, int defense)
    {
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
        CanAttack = false;
    }

    public void ChangeAttackState(bool can)
    {
        CanAttack = can;
    }
    public void GetDamage(int dmg)
    {
        Defense -= dmg;
    }

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
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
        CardManager.AllCards.Add(new Card("Sprites/cards/aid", 4, 6));
        CardManager.AllCards.Add(new Card("Sprites/cards/afina", 4, 6));
        CardManager.AllCards.Add(new Card("Sprites/cards/artemida", 3, 5));
        CardManager.AllCards.Add(new Card("Sprites/cards/cup_hygeia", 0, 2));
        CardManager.AllCards.Add(new Card("Sprites/cards/cyclops", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/driada", 2, 4));
        CardManager.AllCards.Add(new Card("Sprites/cards/golden_fleece", 0, 2));
        CardManager.AllCards.Add(new Card("Sprites/cards/gorgon_head", 0, 1));
        CardManager.AllCards.Add(new Card("Sprites/cards/graya_sisters", 1, 0));
        CardManager.AllCards.Add(new Card("Sprites/cards/zeus", 6, 7));
        CardManager.AllCards.Add(new Card("Sprites/cards/baldr", 2, 4));
        CardManager.AllCards.Add(new Card("Sprites/cards/odin", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/adigel", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/alatir", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/alkonost", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/jaga", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/svarog", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/freia", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/hugin_munin", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/jormungand", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/loki", 6, 8));
        CardManager.AllCards.Add(new Card("Sprites/cards/myelnir", 6, 8));
    }
}
