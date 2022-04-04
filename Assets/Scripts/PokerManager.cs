using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PokerManager : MonoBehaviour
{
    [field: SerializeField]
    private GameObject CardPrefab { get; set; } // DO NOT REMOVE SET

    [SerializeField]
    public CardsGroup deck = new CardsGroup();
    [SerializeField]
    public CardsGroup discartedCards = new CardsGroup();

    [SerializeField]
    public List<Player> players = new List<Player>();
    
    [SerializeField]
    private bool infiniteDeck;

    [SerializeField] 
    private bool revealCpuCards;

    private void Start() {
        for (int i = 1; i <= 4; i++)
        {
            players.Add(new Player(i));
        }

        deck.FillBasicDeck();
        deck.Shuffle();
        DealCards();
    }

    private void OnValidate()
    {
        FlipCards();
    }

    private void FlipCards()
    {
        foreach (Player player in players)
        {
            foreach (CardComponent cc in player.ccHand)
            {
                if (player.seat == 1) continue;
                cc.GetComponent<Selectable>().FaceUp = revealCpuCards;
            }
        }
    }

    public Card DrawFromTopOfDeck()
    {
        Card cardToDraw;

        if (infiniteDeck)
        {
            if (deck.cards.Count <= 0)
            {
                discartedCards.Shuffle();
                deck.cards = new List<Card>(discartedCards.cards);
                discartedCards.cards = new List<Card>();
            }
        }
        
        cardToDraw = deck.cards[0];
        deck.cards.RemoveAt(0);

        return cardToDraw;
    }

    private void DealCards() {

        foreach (Player player in players) {
            player.Hand = deck.DrawXCards(deck, 5);
            for (int card = 0; card < 5; card++) {
                DealCard(player, card);
            }
        }
    }

    private void DealCard(Player player, int indexAtHand) {
        Transform playerTransform = player.cardsParent.transform;
        
        GameObject cardGo = Instantiate(
            CardPrefab,
            playerTransform.position,
            playerTransform.rotation,
            player.cardsParent);
        cardGo.transform.localPosition = player.cardsPositions[indexAtHand];
        
        CardComponent cardComponent = cardGo.GetComponent<CardComponent>();
        player.ccHand.Add(cardComponent);
        cardComponent.card = player.Hand.cards[indexAtHand];
        cardComponent.name = player.Hand.cards[indexAtHand].ToString();
        cardComponent.gameObject.name = player.Hand.cards[indexAtHand].ToString();
        
        // Muestra boca arriba las cartas del jugador real y boca abajo el resto,
        // excepto en el editor de unity que están todas boca arriba si se ha marcado así
        cardGo.GetComponent<Selectable>().FaceUp = false;
        #if UNITY_EDITOR
                cardGo.GetComponent<Selectable>().FaceUp = revealCpuCards;  
        #endif
        if (player.seat == 1) cardGo.GetComponent<Selectable>().FaceUp = true;

        InGameCardInfo inGameInfo = cardGo.AddComponent<InGameCardInfo>();
        inGameInfo.indexAtHand = indexAtHand;
        inGameInfo.playerOwner = player.seat;
    }

    /// <summary>
    /// Adds a card to the discardPile.
    /// </summary>
    /// <param name="cardValuesToDiscard">The values of the new card that will be added to the discards.</param>
    public void AddToDiscardPile(Card cardValuesToDiscard)
    {
        Card cardToAddToDiscard = new Card(cardValuesToDiscard.suit, cardValuesToDiscard.rank);
        discartedCards.cards.Add(cardToAddToDiscard);
    }
}