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
    /// Fills the deck.
    /// </summary>
    public void FillsDeck(bool omitStars = true) {
        foreach (ESuit s in Enum.GetValues(typeof(ESuit))) {
            if (omitStars && s == ESuit.Stars) continue;
            foreach (ERank r in Enum.GetValues(typeof(ERank)))
            {
                Card carta = new Card(s, r);
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
    /// Remove a number of cards and returns them.
    /// </summary>
    /// <param name="nCardsToDraw">The number of cards to draw.</param>
    /// <returns>The removed cards as a CardsGroup.</returns>
    public CardsGroup DrawXCards(int nCardsToDraw) {
        CardsGroup drawnCards = new CardsGroup();
        for (int i = 0; i < nCardsToDraw; i++)
        {
            drawnCards.cards.Add(PokerManager.Instance.DrawFromTopOfDeck());
        }

        return drawnCards;
    }

    /// <summary>
    /// Sorts the cards.
    /// </summary>
    /// <param name="significantSort">If true it will sort the cards by hasSignificance too.</param>
    /// <returns>The sorted cards as a CardsGroup.</returns>
    public CardsGroup SortCards(bool significantSort = false)
    {
        CardsGroup cardsGroupSorted;
            
        // Sorts the received group of cards
        if (!significantSort)
        {
            cardsGroupSorted  = new CardsGroup()
            {
                cards
                    = (cards.ConvertAll(c => new Card(c.suit, c.rank))
                        .OrderBy(c => c.rank)
                        .ThenBy(c => c.suit)
                        .ToList())
            };
        }
        else
        {
            cardsGroupSorted  = new CardsGroup()
            {
                cards
                    = (cards.ConvertAll(c => new Card(c.suit, c.rank, c.hasSignificance))
                        .OrderBy(c => c.hasSignificance)
                        .ThenBy(c => c.rank)
                        .ThenBy(c => c.suit)
                        .ToList())
            };
        }
        
        UpdateAllCardsValues(cardsGroupSorted);

        return cardsGroupSorted;
    }

    /// <summary>
    /// Updates the values of all the cards.
    /// </summary>
    /// <param name="newValues">The cards with the new values.</param>
    public void UpdateAllCardsValues(CardsGroup newValues)
    {
        for (int i = 0; i < newValues.cards.Count; i++)
        {
            this.cards[i].UpdateCardValues(newValues.cards[i]);
        }
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