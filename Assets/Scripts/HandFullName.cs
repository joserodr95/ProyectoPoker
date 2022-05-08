using System;
using System.Collections.Generic;
using System.Linq;

using l10n = Helpers.LocalizationHelper;

public static class HandFullName
{
    /// <summary>
    /// Gets the full name of a hand.
    /// </summary>
    /// <param name="handCards">The hand cards.</param>
    /// <param name="handRank">The rank of the hand.</param>
    /// <returns>The full name of the hand.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string Get(List<Card> handCards, HandsCalculator.EHandRanks handRank)
    {
        string rightSideMsg = "";
        
        l10n.UseTable("CardsNames");

        switch (handRank)
        {
            case HandsCalculator.EHandRanks.HighCard:
            case HandsCalculator.EHandRanks.Straight:
            case HandsCalculator.EHandRanks.Flush:
            case HandsCalculator.EHandRanks.StraightFlush:
                rightSideMsg = 
                    $"{l10n.Get(handCards.Last().rank.ToString())}";
                break;
            case HandsCalculator.EHandRanks.OnePair:
                rightSideMsg = 
                    $"{l10n.Get(handCards.Last().rank.ToString(), n: 2)}";
                break;
            case HandsCalculator.EHandRanks.TwoPair:
                rightSideMsg =
                    $"{l10n.Get(handCards.Last().rank.ToString(), n: 2)} {l10n.Get("And", "Other").ToLower()} {l10n.Get(handCards[2].rank.ToString(), n: 2)}";
                break;
            case HandsCalculator.EHandRanks.ThreeOfKind:
                rightSideMsg = 
                    $"{l10n.Get(handCards.Last().rank.ToString(), n: 3)}";
                break;
            case HandsCalculator.EHandRanks.FullHouse:
                rightSideMsg =
                    $"{l10n.Get(handCards.Last().rank.ToString(), n: 3)} {l10n.Get("And", "Other").ToLower()} {l10n.Get(handCards[1].rank.ToString(), n: 2)}";
                break;
            case HandsCalculator.EHandRanks.FourOfKind:
                rightSideMsg = 
                    $"{l10n.Get(handCards.Last().rank.ToString(), n: 4)}";
                break;
            case HandsCalculator.EHandRanks.RoyalStraightFlush:
                rightSideMsg = l10n.Get("TheBestHandInTheGame", "Other");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(handRank), handRank, null);
        }

        return $"{l10n.Get(handRank.ToString(), "HandRanksNames")} - {rightSideMsg}";
    }
}