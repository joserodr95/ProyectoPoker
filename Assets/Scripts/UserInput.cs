using System;
using UnityEngine;
using static HandsCalculator;

public class UserInput : MonoBehaviour {

    public PokerManager pokerManager;
    
    private Selectable seleccionable;
    private InGameCardInfo cardInfo;

    private Camera mCam;

    private void Start()
    {
        mCam = Camera.main;
    }

    private void Update() {
        GetMouseClick();
    }

    private void GetMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Collider2D coll = Physics2D.Raycast(
            mCam.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero)
            .collider;
        if (ReferenceEquals(coll, null)) return;
        switch (coll.tag) {
            case "Card":
                CardClickActions(coll.gameObject);
                break;
        }
    }

    private void CardClickActions(GameObject go) {
        // Card click actions
        //Debug.LogFormat("Card {0} clicked", go.name);

        seleccionable = go.GetComponent<Selectable>();
        cardInfo = go.GetComponent<InGameCardInfo>();

        if (!seleccionable.FaceUp) return;
        if (cardInfo.playerOwner == 1)
        {
            seleccionable.Selected = !seleccionable.Selected;
            pokerManager.players[0].toDiscard[cardInfo.indexAtHand] = seleccionable.Selected;
        }
    }

    /// <summary>
    /// Discards your cards marked to discard or the non-significant cards for the CPU players.
    /// </summary>
    public void Discard() {

        for (int pNum = 0; pNum < pokerManager.players.Count; pNum++)
        {
            Player player = pokerManager.players[pNum];
            CardsGroup pHand = player.Hand;

            if (pNum == 0) RealPlayerDiscard(player, ref pHand);
            else CpuPlayerDiscard(ref pHand);
        }
    }


    private void RealPlayerDiscard(Player player, ref CardsGroup pHand)
    {
        for (int i = 0; i < pHand.cards.Count; i++)
        {
            if (!player.toDiscard[i]) continue;
            
            pokerManager.AddToDiscardPile(pHand.cards[i]);
            player.Hand.cards[i].UpdateCardValues(pokerManager.DrawFromTopOfDeck());
            player.toDiscard[i] = false;
            player.ccHand[i].GetComponent<Selectable>().Selected = false;
        }

        pHand.CalculateHandRank();
    }
    
    private void CpuPlayerDiscard(ref CardsGroup pHand)
    {
        int numberOfLeftSideDiscards = NumberOfLeftCardsToDiscard(pHand.CalculateHandRank());
        
        for (int i = 0; i < pHand.cards.Count; i++)
        {
            if (i >= numberOfLeftSideDiscards) continue;
            
            pokerManager.AddToDiscardPile(pHand.cards[i]);
            pHand.cards[i].UpdateCardValues(
                pokerManager.DrawFromTopOfDeck()
            );
        }
        
        pHand.CalculateHandRank();
    }

    private static int NumberOfLeftCardsToDiscard(EHandRanks calculateHandRank)
    {
        switch (calculateHandRank)
        {
            case EHandRanks.HighCard:
                return 4;
            case EHandRanks.OnePair:
                return 3;
            case EHandRanks.TwoPair:
                return 1;
            case EHandRanks.ThreeOfKind:
                return 2;
            case EHandRanks.Straight:
            case EHandRanks.Flush:
            case EHandRanks.FullHouse:
            case EHandRanks.FourOfKind:
            case EHandRanks.StraightFlush:
            case EHandRanks.RoyalStraightFlush:
                return 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(calculateHandRank), calculateHandRank, null);
        }
    }

    public void CheckHandValue() {
        Debug.Log($"Player 1: {pokerManager.players[0].Hand.CalculateHandRank()}");
        #if UNITY_EDITOR
        for (int i = 1; i < 4; i++)
        {
            Debug.Log($"Player {i+1}: {pokerManager.players[i].Hand.CalculateHandRank()}");
        }
        #endif
    }

}
