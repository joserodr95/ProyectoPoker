using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using hfn = HandFullName;
using l10n = Helpers.LocalizationHelper;

public class PokerManager : MonoBehaviour
{
    public static PokerManager Instance { get; private set; }

    [field: SerializeField] private GameObject CardPrefab { get; set; } // DO NOT REMOVE OR CHANGE SET

    [SerializeField] public CardsGroup deck = new CardsGroup();
    [SerializeField] public CardsGroup discartedCards = new CardsGroup();

    [SerializeField] public List<Player> players = new List<Player>();

    private int roundNumber = 1;
    internal int lastWinner = -1;
    internal bool revealCpuCards;

    private bool firstDeal = true;

    [SerializeField] private UserInput userInput;
    [SerializeField] private FinalState finalState;

    private void Start()
    {
        Instance = this;

        for (int i = 1; i <= 4; i++) players.Add(new Player(i));

        Debug.Log($"SceneManager.GetActiveScene().name = {SceneManager.GetActiveScene().name}");
        if (SceneManager.GetActiveScene().name == "PokerLevel1")
        {
            deck.FillsDeck(true);
        }
        else if (SceneManager.GetActiveScene().name == "PokerLevel2")
        {
            deck.FillsDeck(false);
        }
        deck.Shuffle();
        DealCards();
    }

    /// <summary>
    /// It will flip down or up the cards.
    /// </summary>
    public void FlipCards()
    {
        revealCpuCards = !revealCpuCards;

        foreach (Player player in players)
        {
            if (player.Eliminated) continue;
            foreach (CardComponent cc in player.ccHand)
            {
                if (player.seat == 1) continue;
                cc.GetComponent<Selectable>().FaceUp = revealCpuCards;
            }
        }
    }

    public Card DrawFromTopOfDeck()
    {
        if (deck.cards.Count <= 0)
        {
            discartedCards.Shuffle();
            deck.cards = new List<Card>(discartedCards.cards);
            discartedCards.cards = new List<Card>();
        }

        Card cardToDraw = deck.cards[0];
        deck.cards.RemoveAt(0);

        return cardToDraw;
    }

    private void DealCards()
    {
        Debug.Log($"<b>{l10n.Get("Round", "Other")}: {roundNumber}</b>");

        foreach (Player player in players)
        {
            player.Hand = deck.DrawXCards(5);
            for (int card = 0; card < 5; card++)
            {
                if (!player.Eliminated) DealCard(player, card);
            }
        }

        firstDeal = false;
    }

    private void DealCard(Player player, int indexAtHand)
    {
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
        if (firstDeal) player.ccHand.Add(cardComponent);
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

        if (cardGo.TryGetComponent(typeof(InGameCardInfo), out Component _)) return;

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
        Dictionary<int, HandsCalculator.RankCardsTuple> playerHandRankDict
            = new Dictionary<int, HandsCalculator.RankCardsTuple>();

        for (int i = 0; i < 4; i++)
        {
            if (players[i].IsFolding || players[i].Eliminated) continue;
            HandsCalculator.EHandRanks handRank = players[i].Hand.CalculateHandRank();
            playerHandRankDict.Add(i, new HandsCalculator.RankCardsTuple(handRank, players[i].Hand.cards));
            Debug.Log($"{l10n.Get("Player", "Other")} {i + 1}: {hfn.Get(players[i].Hand.cards, handRank)}");
            userInput.playerMessages[i].text = $"{hfn.Get(players[i].Hand.cards, handRank)}";
            userInput.playerMessages[i].enabled = true;
            userInput.SetPlayerMessageStyle(i);
        }

        lastWinner = HandsCalculator.DeclareWinner(playerHandRankDict);
        userInput.SetPlayerMessageStyle(lastWinner, true);
    }

    internal void NewRound()
    {
        roundNumber++;
        FlipCards();
        userInput.ResetPlayerMessages();
        BettingManager.Instance.ResetTotalBetted();
        EndOfRoundDiscard();
        foreach (Player player in players) player.IsFolding = false;
        if (players[0].Eliminated) finalState.EndGame(FinalState.EState.Lose);
        else if (players[1].Eliminated && players[2].Eliminated && players[3].Eliminated) finalState.EndGame(FinalState.EState.Win);
        DealCards();
    }

    /// <summary>
    /// Add the cards in the hard of each player to the discard pile.
    /// This method is called just before the hands are dealt to the players (after the 1st round).
    /// </summary>
    private void EndOfRoundDiscard()
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