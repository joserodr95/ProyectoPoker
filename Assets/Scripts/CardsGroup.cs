using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Clase que recoge una lista de DataCarta y incluye métodos a usar sobre esta.
/// </summary>
[System.Serializable]
public class CardsGroup : IEnumerable, IComparer<Card> {

    public List<Card> cards = new List<Card>();
    
    private Random rng = new Random();

    /// <summary>
    /// Rellena un ConjuntoCartas con el mazo básico de 52 naipes.
    /// </summary>
    public void FillBasicDeck() {
        foreach (ESuit p in Enum.GetValues(typeof(ESuit))) {
            foreach (ERank r in Enum.GetValues(typeof(ERank)))
            {
                Card carta = new Card(p, r);
                this.cards.Add(carta);
            }
        }
    }

    /// <summary>
    /// Mezcla las cartas.
    /// </summary>
    public void Shuffle() {
        this.cards = this.cards.OrderBy(_ => rng.Next()).ToList();
    }

    /// <summary>
    /// Elimina un número de cartas y lo devuelve
    /// </summary>
    /// <returns>Las cartas eliminadas como ConjuntoCartas</returns>
    public CardsGroup DrawXCards(CardsGroup fuenteDeRobo, int numCartasARobar) {
        CardsGroup cartasRobadas = new CardsGroup {
            cards = fuenteDeRobo.cards.GetRange(0, numCartasARobar)
        };
        fuenteDeRobo.cards.RemoveRange(0, numCartasARobar);

        return cartasRobadas;
    }

    public CardsGroup SortCards(bool significantSort = false)
    {
        // Sorts the received group of cards
        if (!significantSort)
        {
            CardsGroup cardsGroupSorted = new CardsGroup
            {
                cards
                    = (cards.ConvertAll(c => new Card(c.suit, c.rank))
                        .OrderBy(c => c.rank)
                        .ThenBy(c => c.suit)
                        .ToList())
            };
            return cardsGroupSorted;
        }
        else
        {
            CardsGroup cardsGroupSorted = new CardsGroup
            {
                cards
                    = (cards.ConvertAll(c => new Card(c.suit, c.rank, c.hasSignificance))
                        .OrderBy(c => c.hasSignificance)
                        .ThenBy(c => c.rank)
                        .ThenBy(c => c.suit)
                        .ToList())
            };
            return cardsGroupSorted;
        }
    }
    
    public void Sort() {
        this.cards.Sort();
    }

    public IEnumerator GetEnumerator() {
        return ((IEnumerable)cards).GetEnumerator();
    }

    public int Compare(Card card1, Card card2)
    {
        if (card1.rank > card2.rank) {
            return 1;
        }

        if (card1.rank < card2.rank) {
            return -1;
        }

        if (card1.suit > card2.suit) {
            return 1;
        }

        if (card1.suit < card2.suit) {
            return -1;
        }

        return 0;
    }
}