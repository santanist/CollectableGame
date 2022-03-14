using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;
    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeck = GiveDeckCard();
    }

    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        return list;
    }
}

public class GameManagerScr : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand,
                     EnemyField, PlayerField;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeTxt;
    public Button EndTurnBtn;

    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
                             PlayerFieldCards = new List<CardInfoScr>(),
                             EnemyHandCards = new List<CardInfoScr>(),
                             EnemyFieldCards = new List<CardInfoScr>();
    public bool IsPlayerTurn
    {
        get
        {
            return Turn % 2 == 0;
        }
    }

    void Start()
    {
        Turn = 0;

        CurrentGame = new Game();

        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);

        StartCoroutine(TurnFunc());
    }

    void GiveHandCards(List<Card> deck, Transform hand)
    {
        int i = 0;
        while (i++ < 4)
            GiveCardToHand(deck, hand);
    }
    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if (deck.Count == 0)
            return;

        Card cardRott = deck[0];

        GameObject cardGO = Instantiate(CardPref, hand, false);

        if (hand == EnemyHand)
        {
            cardGO.GetComponent<CardInfoScr>().HideCardInfo(cardRott);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }

        else
        {
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(cardRott);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScr>());
            cardGO.GetComponent<AttackedCard>().enabled = false;
        }
        
        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();
        

        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            {
                card.SelfCard.ChangeAttackState(true);
            }
                

            while(TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            foreach (var card in EnemyFieldCards)
                card.SelfCard.ChangeAttackState(true);

            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }

            if (EnemyHandCards.Count > 0)
                EnemyTurn(EnemyHandCards);
        }
        ChangeTurn();
    }

    void EnemyTurn(List<CardInfoScr> cards)
    {
        int count = cards.Count == 1 ? 1 :
                    Random.Range(0, cards.Count);
        for (int i = 0; i < count; i++)
        {
            if (EnemyFieldCards.Count > 5)
                return;

            cards[0].ShowCardInfo(cards[0].SelfCard);
            cards[0].transform.SetParent(EnemyField);

            EnemyFieldCards.Add(cards[0]);
            EnemyHandCards.Remove(cards[0]);
        }
    }

    public void ChangeTurn()
    {
        StopAllCoroutines();
        Turn++;
        EndTurnBtn.interactable = IsPlayerTurn;

        if (IsPlayerTurn)
            GiveNewCards();

        StartCoroutine(TurnFunc());
    }

    void GiveNewCards()
    {
        GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }

    public void CardsFight(CardInfoScr playerCard, CardInfoScr enemyCard)
    {
        playerCard.SelfCard.GetDamage(enemyCard.SelfCard.Attack);
        enemyCard.SelfCard.GetDamage(playerCard.SelfCard.Attack);

        if (!playerCard.SelfCard.IsAlive)
            DestroyCard(playerCard);
        else
            playerCard.RefreshData();
        
        if (!enemyCard.SelfCard.IsAlive)
            DestroyCard(enemyCard);
        else
            enemyCard.RefreshData();
    }

    void DestroyCard(CardInfoScr card)
    {
        card.GetComponent<CardMovementScr>().OnEndDrag(null);

        if (EnemyFieldCards.Exists(x => x == card))
            EnemyFieldCards.Remove(card);
        if (PlayerFieldCards.Exists(x => x == card))
            PlayerFieldCards.Remove(card);

        Destroy(card.gameObject);
    }
}
