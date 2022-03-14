using System.Collections.Generic;
using UnityEngine;

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

    private void Start() {
        for (int i = 1; i <= 4; i++)
        {
            players.Add(new Player(i));
        }

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
            player.Hand = deck.DrawXCards(deck, 5);
            for (int card = 0; card < 5; card++) {
                DealCard(player, card);
            }
        }
    }

    private void DealCard(Player player, int indexAtHand) {
        Transform playerTransform = player.cardsParent.transform;
        
        GameObject cardGO = Instantiate(
            CardPrefab,
            playerTransform.position,
            playerTransform.rotation,
            player.cardsParent);
        cardGO.transform.localPosition = player.cardsPositions[indexAtHand];
        
        CardComponent cardComponent = cardGO.GetComponent<CardComponent>();
        player.ccHand.Add(cardComponent);
        cardComponent.card = player.Hand.cards[indexAtHand];
        cardComponent.name = player.Hand.cards[indexAtHand].ToString();
        cardComponent.gameObject.name = player.Hand.cards[indexAtHand].ToString();
        
        // Muestra boca arriba las cartas del jugador real y boca abajo el resto,
        // excepto en el editor de unity que están todas boca arriba
        cardGO.GetComponent<Selectable>().FaceUp = false;
        if (player.seat == 1) cardGO.GetComponent<Selectable>().FaceUp = true;
#if UNITY_EDITOR
        cardGO.GetComponent<Selectable>().FaceUp = true;  
#endif
        
        InGameCardInfo inGameInfo = cardGO.AddComponent<InGameCardInfo>();
        inGameInfo.indexAtHand = indexAtHand;
        inGameInfo.playerOwner = player.seat;
    }
    
}