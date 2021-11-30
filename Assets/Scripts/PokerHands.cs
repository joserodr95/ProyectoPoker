using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokerHands {
    public enum EHandRanks {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfKind,
        Straight,
        Flush,
        FullHouse,
        FourOfKind,
        StraightFlush,
        RoyalStraightFlush
    }

    public EHandRanks CalculateHandRank(CardsGroup cardsGroup) {
        CardsGroup cardsGroupSorted = cardsGroup;
        cardsGroupSorted.cards = cardsGroup.cards.OrderBy(c => c.rank).ThenBy(c => c.suit).ToList<Card>();

        return EHandRanks.HighCard;
    }
}

