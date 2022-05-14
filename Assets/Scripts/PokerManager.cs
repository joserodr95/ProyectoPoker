using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using hfn = HandFullName;
using l10n = Helpers.LocalizationHelper;

public class PokerManager : MonoBehaviour
{
    public static PokerManager Instance { get; private set; }

    [field: SerializeField]
    private GameObject CardPrefab { get; set; } // DO NOT REMOVE OR CHANGE SET

    [SerializeField]
    public CardsGroup deck = new CardsGroup();
    [SerializeField]
    public CardsGroup discartedCards = new CardsGroup();

    [SerializeField]
    public List<Player> players = new List<Player>();
    
    internal int roundNumber = 1;
    internal bool revealCpuCards;

    private bool firstDeal = true;
    
    private UserInput userInput;

    private void Start()
    {
        Instance = this;
        
        for (int i = 1; i <= 4; i++)
        {
            players.Add(new Player(i));
        }

        userInput = GameObject.Find("UiManager").GetComponent<UserInput>();

        deck.FillBasicDeck();
        deck.Shuffle();
        DealCards();
    }

    /// <summary>
    /// This function is called when the script is loaded or a value is changed in the Inspector (Called in the editor only).
    /// You can use this to ensure that when you modify data in an editor, that data stays within a certain range.
    /// </summary>
    // private void OnValidate()
    // {
    //     FlipCards();
    // }

    /// <summary>
    /// It will flip down or up the cards.
    /// </summary>
    public void FlipCards()
    {
        revealCpuCards = !revealCpuCards;
        
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
        
        if (deck.cards.Count <= 0)
        {
            discartedCards.Shuffle();
            deck.cards = new List<Card>(discartedCards.cards);
            discartedCards.cards = new List<Card>();
        }

        cardToDraw = deck.cards[0];
        deck.cards.RemoveAt(0);

        return cardToDraw;
    }

    public void DealCards() {
        Debug.Log($"<b>{l10n.Get("Round", "Other")}: {roundNumber}</b>");
        
        foreach (Player player in players) {
            player.Hand = deck.DrawXCards(5);
            for (int card = 0; card < 5; card++) {
                DealCard(player, card);
            }
        }
        
        firstDeal = false;
    }

    private void DealCard(Player player, int indexAtHand) {
        Transform playerTransform = player.cardsParent.transform;
        GameObject cardGo;

        if (firstDeal)
        {
            cardGo = Instantiate(
                CardPrefab,
                playerTransform.position,
                playerTransform.rotation,
                player.cardsParent);
            cardGo.transform.localPosition = player.cardsPositions[indexAtHand];
        }
        else
        {
            cardGo = player.ccHand[indexAtHand].gameObject;
        }
        
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
    
    public void CheckHandValue()
    {
        Dictionary<int, HandsCalculator.EHandRanks> playerHandRankDict 
            = new Dictionary<int, HandsCalculator.EHandRanks>();

        for (int i = 0; i < 4; i++)
        {
            HandsCalculator.EHandRanks handRank = players[i].Hand.CalculateHandRank();
            playerHandRankDict.Add(i, handRank);
            Debug.Log($"{l10n.Get("Player", "Other")} {i+1}: {hfn.Get(players[i].Hand.cards, handRank)}");
            userInput.playerMessages[i].text = $"{hfn.Get(players[i].Hand.cards, handRank)}";
            userInput.playerMessages[i].enabled = true;
            userInput.SetPlayerMessageStyle(i);
        }
        
        userInput.SetPlayerMessageStyle(DeclareWinner(playerHandRankDict), true);
    }

    private int DeclareWinner(Dictionary<int, HandsCalculator.EHandRanks> playerHandRankDict)
    {
        KeyValuePair<int, HandsCalculator.EHandRanks> bestHand = playerHandRankDict.ElementAt(0);
        foreach (KeyValuePair<int, HandsCalculator.EHandRanks> kvp in playerHandRankDict.Skip(1))
        {
            if (kvp.Value > bestHand.Value)
            {
                bestHand = kvp;
            }
            else if (kvp.Value == bestHand.Value)
            {
                for (int i = players[kvp.Key].Hand.cards.Count-1; i >= 0; i--)
                {
                    Card playerCard = players[kvp.Key].Hand.cards[i];
                    Card bestHandCard = players[bestHand.Key].Hand.cards[i];
 
                    if (playerCard.rank > bestHandCard.rank)
                    {
                        bestHand = kvp;
                        break;
                    }
                    if (playerCard.rank < bestHandCard.rank)
                    {
                        break;
                    }
                }
            }
        }

        l10n.UseTable("Other");
        Debug.Log(
            $"{l10n.Get("Winner")} {l10n.Get("Player")} {bestHand.Key + 1}. {hfn.Get(players[bestHand.Key].Hand.cards, bestHand.Value)}");
        return bestHand.Key;
    }
    
    internal void NewRound()
    {
        roundNumber++;
        FlipCards();
        userInput.ResetPlayerMessages();
        EndOfRoundDiscard();
        DealCards();
    }

    /// <summary>
    /// Add the cards in the hard of each player to the discard pile.
    /// This method is called just before the hands are dealt to the players (after the 1st round).
    /// </summary>
    public void EndOfRoundDiscard()
    {
        foreach (Player player in players)
        {
            foreach (Card card in player.Hand.cards)
            {
                AddToDiscardPile(card);
            }
        }
    }
}