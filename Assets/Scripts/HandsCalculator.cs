using System.Collections.Generic;
using System.Linq;

public static class HandsCalculator {

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

    public static EHandRanks CalculateHandRank(this CardsGroup cardsGroup) {

        CardsGroup cardsGroupSorted = cardsGroup.SortCards();
        EHandRanks resultHandRank = EHandRanks.HighCard;

        for (int i = 0; i < 5; i++) {
            cardsGroup.cards[i].UpdateCardValues
                (
                    cardsGroup.cards[i],
                    cardsGroupSorted.cards[i]
                );
        }

        if (CalculateRoyalStraightFlush(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.RoyalStraightFlush;
        } else if (CalculateStraightFlush(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.StraightFlush;
        } else if (CalculateFourOfKind(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.FourOfKind;
        } else if (CalculateFullHouse(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.FullHouse;
        } else if (CalculateFlush(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.Flush;
        } else if (CalculateStraight(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.Straight;
        } else if (CalculateThreeOfKind(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.ThreeOfKind;
        } else if (CalculateTwoPairs(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.TwoPair;
        } else if (CalculateOnePair(cardsGroupSorted.cards)) {
            resultHandRank = EHandRanks.OnePair;
        } else {
            ApplySignificance(ref cardsGroupSorted.cards);    
        }

        cardsGroupSorted = cardsGroupSorted.SortCards(true);
        for (int i = 0; i < 5; i++) {
            cardsGroup.cards[i].UpdateCardValues
            (
                cardsGroup.cards[i],
                cardsGroupSorted.cards[i]
            );
        }

        return resultHandRank;
    }

    private static bool CalculateRoyalStraightFlush(List<Card> cards)
    {
        bool result = cards[0].rank == ERank.TEN && CalculateStraightFlush(cards);
        ApplySignificance(ref cards);
        return result;
    }

    private static bool CalculateStraightFlush(List<Card> cards) {
        ESuit s = cards[0].suit;

        for (int i = 0; i < cards.Count - 1; i++) {
            // If the value of the card's rank that is watching is not equal to
            // the value minus 1 of the next card's rank
            if ((int)cards[i].rank != (int)cards[i + 1].rank - 1) {
                if (!(cards[i].rank == ERank.FIVE && cards[i + 1].rank == ERank.AS)) {
                    return false;
                }
            }

            // If the suit of the next card is the suit that should be
            if (cards[i + 1].suit != s) {
                return false;
            }
        }

        ApplySignificance(ref cards);
        return true;
    }

    private static bool CalculateFourOfKind(List<Card> cards) {
        /*
         * Calculates how many repetitions of the rank of the first card there is
         * if there is 4 of them returns true else tries the same with the second card. 
         * 
         * Since the hand is sorted and the total size of a hand is one more card
         * than in a four of kind, the first card of the group of equal cards
         * that form the four of kind will be either in the first or second position.
		 */

        for (int i = 0; i <= 1; i++) {
            ERank r = cards[i].rank;
            int reps = 0;
            foreach (Card t in cards)
            {
                if (t.rank == r) {
                    reps++;
                }
            }

            if (reps == 4) {
                ApplySignificance(ref cards, 0+i, 1+i, 2+i, 3+i);
                return true;
            }

        }

        return false;
    }

    private static bool CalculateFullHouse(List<Card> cards) {
        int reps = 1;
        ERank r = cards[0].rank;
        ERank? r2 = null;
        int target;

        /*
         * Counts how many times the rank of the first card is repeated between the other cards.
         */
        for (int i = 1; i < cards.Count; i++) {
            if (cards[i].rank == r) {
                reps++;
            } else {
                r2 = cards[i].rank;
            }
        }

        /*
         * If the rank is repeated 2 times it will search for the three of kind,
         * if is repeated 3 times it will search for the pair.
         * If is not repeated neither 2 or 3 times then is not a full house.
         */
        if (reps == 2) {
            target = 3;
        } else if (reps == 3) {
            target = 2;
        } else {
            return false;
        }

        reps = 0;
        for (int i = 1 ; i < cards.Count; i++) {
            if (cards[i].rank == r2) {
                reps++;
            }
        }

        if (reps == target) CalculateThreeOfKind(cards);
        
        return reps == target;
    }

    private static bool CalculateFlush(List<Card> cards) {
        ESuit s = cards[0].suit;

        for (int i = 1; i < cards.Count; i++) {
            if (cards[i].suit != s) {
                return false;
            }
        }

        ApplySignificance(ref cards);
        return true;
    }

    private static bool CalculateStraight(List<Card> cards) {
        for (int i = 0; i < cards.Count-1; i++) {
            /*
             * If the value of the rank of the card that is looking at
             * is not equal to the value minus 1 of the rank of the next card.
             */
            if ((int)cards[i].rank != (int)cards[i+1].rank - 1) {
                if (!(cards[i].rank == ERank.FIVE && cards[i+1].rank == ERank.AS)) {
                    return false;
                }
            }

        }

        if (cards.Any(c => c.rank == ERank.AS) && cards[4].rank == ERank.AS)
        {
            ApplySignificance(ref cards, 0, 1, 2, 3);
        }
        else
        {
            ApplySignificance(ref cards);    
        }

        return true;
    }

    private static bool CalculateThreeOfKind(List<Card> cards)
    {
        for (int i = 0; i < 3; i++) {
            ERank r = cards[i].rank;
            int reps = 1;
            int[] significantIdxs = new int[3];
            significantIdxs[0] = i;
            for (int i2 = i+1; i2 < cards.Count; i2++)
            {
                if (cards[i2].rank != r) continue;
                reps++;
                significantIdxs[reps - 1] = i2;
            }

            if (reps != 3) continue;
            ApplySignificance(ref cards, significantIdxs);
            return true;
        }

        return false;
    }

    private static bool CalculateTwoPairs(List<Card> cards) {
        for (int i = 0; i <= 1; i++) {
            if (cards[i].rank == cards[i+1].rank && cards[i+2].rank == cards[i+3].rank) {
                ApplySignificance(ref cards, i, i+1, i+2, i+3);
                return true;
            }

            if (cards[0].rank != cards[1].rank || cards[3].rank != cards[4].rank) continue;
            ApplySignificance(ref cards, 0, 1, 3, 4);
            return true;
        }

        return false;
    }

    private static bool CalculateOnePair(List<Card> cards) {
        for (int i = 0; i < 4; i++) {
            ERank r = cards[i].rank;
            for (int i2 = i+1; i2 < cards.Count; i2++)
            {
                if (cards[i2].rank != r) continue;
                ApplySignificance(ref cards, i, i2);
                return true;
            }
        }

        return false;
    }

    private static void ApplySignificance(ref List<Card> cards, params int[] significantIdxs)
    {
        foreach (Card t in cards)
        {
            t.hasSignificance = false;
        }

        if (significantIdxs.Length == 0) return;
        foreach (int idx in significantIdxs)
        {
            cards[idx].hasSignificance = true;
        }
    }
}

