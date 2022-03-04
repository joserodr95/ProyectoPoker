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

    public static EHandRanks CalculateHandRank(CardsGroup cardsGroup) {

        // Sorts in a new group of cards
        //CardsGroup cardsGroupSorted = new CardsGroup();
        //cardsGroupSorted.cards = cardsGroup.cards.ConvertAll(c => new Card(c.suit, c.rank));
        //cardsGroupSorted.cards = cardsGroupSorted.cards.OrderBy(c => c.rank).ThenBy(c => c.suit).ToList<Card>();

        // Sorts the received group of cards
        // CardsGroup cardsGroupSorted = new CardsGroup();
        // cardsGroupSorted.cards = cardsGroup.cards.ConvertAll(c => new Card(c.suit, c.rank));
        // cardsGroupSorted.cards = cardsGroupSorted.cards.OrderBy(c => c.rank).ThenBy(c => c.suit).ToList<Card>();
        CardsGroup cardsGroupSorted = new CardsGroup
        {
            cards
                = (cardsGroup.cards.ConvertAll(c => new Card(c.suit, c.rank))
                    .OrderBy(c => c.rank).ThenBy(c => c.suit).ToList<Card>())
        };

        for (int i = 0; i < 5; i++) {
            cardsGroup.cards[i].UpdateCardValues(cardsGroup.cards[i], cardsGroupSorted.cards[i]);
        }

        if (CalculateRoyalStraightFlush(cardsGroupSorted.cards)) {
            return EHandRanks.RoyalStraightFlush;
        } else if (CalculateStraightFlush(cardsGroupSorted.cards)) {
            return EHandRanks.StraightFlush;
        } else if (CalculateFourOfKind(cardsGroupSorted.cards)) {
            return EHandRanks.FourOfKind;
        } else if (CalculateFullHouse(cardsGroupSorted.cards)) {
            return EHandRanks.FullHouse;
        } else if (CalculateFlush(cardsGroupSorted.cards)) {
            return EHandRanks.Flush;
        } else if (CalculateStraight(cardsGroupSorted.cards)) {
            return EHandRanks.Straight;
        } else if (CalculateThreeOfKind(cardsGroupSorted.cards)) {
            return EHandRanks.ThreeOfKind;
        } else if (CalculateTwoPairs(cardsGroupSorted.cards)) {
            return EHandRanks.TwoPair;
        } else if (CalculateOnePair(cardsGroupSorted.cards)) {
            return EHandRanks.OnePair;
        }

        return EHandRanks.HighCard;
    }

    private static bool CalculateRoyalStraightFlush(List<Card> cards) {
        return cards[0].rank == ERank.TEN && CalculateStraightFlush(cards);
    }

    private static bool CalculateStraightFlush(List<Card> cards) {
        ESuit s = cards[0].suit;

        for (int i = 0; i < cards.Count - 1; i++) {
            // Si el valor del rango de la carta que esta mirando no es igual al valor menos 1 del rango de la siguiente carta
            if ((int)cards[i].rank != (int)cards[i + 1].rank - 1) {
                if (!(cards[i].rank == ERank.FIVE && cards[i + 1].rank == ERank.AS)) {
                    return false;
                }
            }

            // Si el palo de la siguiente carta es el palo que debería ser
            if (cards[i + 1].suit != s) {
                return false;
            }
        }

        return true;
    }

    private static bool CalculateFourOfKind(List<Card> cards) {
        /*
         * Calcula cuantas repeticiones del rango de la primera carta hay
         * si hay 4 es poker si no intenta lo mismo con la segunda carta
         * 
         * Como la mano está ordenada y el tamaño de la mano es de una carta más
		 * que el poker, la primera carta del conjunto de cuatro cartas de 
		 * rango igual que formaría el poker estará sí o sí en el primer o 
		 * el segundo puesto.
		 */

        for (int i = 0; i <= 1; i++) {
            ERank r = cards[i].rank;
            int reps = 0;
            for (int i2 = 0; i2 < cards.Count; i2++) {
                if (cards[i2].rank == r) {
                    reps++;
                }
            }

            if (reps == 4) {
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
         * Cuenta cuantas veces se repite el rango de la primera carta,
         * entre las otras cartas.
         */
        for (int i = 1; i < cards.Count; i++) {
            if (cards[i].rank == r) {
                reps++;
            } else {
                r2 = cards[i].rank;
            }
        }

        /*
         * Si el rango se repite 2 veces buscará el trio, si se repite 3 buscará la pareja. 
         * Si no se repite ni 2 ni 3 veces es que no es full
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

        return reps == target;

    }

    private static bool CalculateFlush(List<Card> cards) {
        ESuit s = cards[0].suit;

        for (int i = 1; i < cards.Count; i++) {
            if (cards[i].suit != s) {
                return false;
            }
        }

        return true;
    }

    private static bool CalculateStraight(List<Card> cards) {
        for (int i = 0; i < cards.Count-1; i++) {
            // Si el valor del rango de la carta que esta mirando no es igual al valor menos 1 del rango de la siguiente carta
            if ((int)cards[i].rank != (int)cards[i+1].rank - 1) {
                if (! (cards[i].rank == ERank.FIVE && cards[i+1].rank == ERank.AS)) {
                    return false;
                }
            }

        }

        return true;
    }

    private static bool CalculateThreeOfKind(List<Card> cards) {
        for (int i = 0; i < 3; i++) {
            ERank r = cards[i].rank;
            int reps = 1;
            for (int i2 = i+1; i2 < cards.Count; i2++) {
                if (cards[i2].rank == r) {
                    reps++;
                }
            }
            if (reps == 3) {
                return true;
            }
        }

        return false;
    }

    private static bool CalculateTwoPairs(List<Card> cards) {
        for (int i = 0; i <= 1; i++) {
            if (cards[i].rank == cards[i+1].rank && cards[i+2].rank == cards[i + 3].rank) {
                return true;
            }
            if (cards[0].rank == cards[1].rank && cards[3].rank == cards[4].rank) {
                return true;
            }
        }

        return false;
    }

    private static bool CalculateOnePair(List<Card> cards) {
        for (int i = 0; i < 4; i++) {
            ERank r = cards[i].rank;
            for (int i2 = i+1; i2 < cards.Count; i2++) {
                if (cards[i2].rank == r) {
                    return true;
                }
            }
        }

        return false;
    }
}

