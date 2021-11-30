using System;
using System.Collections.Generic;
using UnityEngine;

public class PokerManager : MonoBehaviour
{
    [field: SerializeField]
    public GameObject CardPrefab { get; set; }

    [SerializeField]
    public CardsGroup deck = new CardsGroup();
    [SerializeField]
    public CardsGroup discartedCards = new CardsGroup();

    [SerializeField]
    public List<Player> players = new List<Player>();

    private void Start() {
        players.Add(new Player(1));

        deck.FillBasicDeck();
        deck.Shuffle();
        DealCards();
    }

    public Card DrawFromTopOfDeck() {
        Card cardToDraw = deck.cards[0];
        deck.cards.RemoveAt(0);

        return cardToDraw;
    }

    private void DealCards() {

        foreach (Player player in players) {
            player.hand = deck.DrawXCards(deck, 5);
            for (int card = 0; card < 5; card++) {
                DealCard(player, card);
            }
        }
    }

    private void DealCard(Player player, int indexAtHand) {
        GameObject cardGO = Instantiate(CardPrefab,
                                                player.cardsPositions[indexAtHand],
                                                Quaternion.identity,
                                                player.cardsParent);
        CardComponent cardComponent = cardGO.GetComponent<CardComponent>();
        player.ccHand.Add(cardComponent);
        cardComponent.Init(player.hand.cards[indexAtHand]);
        cardGO.GetComponent<Selectable>().FaceUp = true;
        InGameCardInfo inGameInfo = cardGO.AddComponent<InGameCardInfo>();
        inGameInfo.indexAtHand = indexAtHand;
        inGameInfo.playerOwner = player.seat;
    }
    
}
