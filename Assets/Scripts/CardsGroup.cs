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

    /// <summary>
    /// Rellena un ConjuntoCartas con el mazo básico de 52 naipes.
    /// </summary>
    public void FillBasicDeck() {

        Card carta;
        foreach (ESuit p in Enum.GetValues(typeof(ESuit))) {
            foreach (ERank r in Enum.GetValues(typeof(ERank))) {
                carta = new Card(p, r);
                this.cards.Add(carta);
            }
        }
    }

    /// <summary>
    /// Mezcla las cartas.
    /// </summary>
    public void Shuffle() {
        Random rng = new Random();
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

    public void Sort() {
        this.cards.Sort();
    }

    public IEnumerator GetEnumerator() {
        return ((IEnumerable)cards).GetEnumerator();
    }

    public int Compare(Card card1, Card card2) {
        if (card1.rank > card2.rank) {
            return 1;
        } else if (card1.rank < card2.rank) {
            return -1;
        } else {
            if (card1.suit > card2.suit) {
                return 1;
            } else if (card1.suit < card2.suit) {
                return -1;
            } else {
                return 0;
            }
        }
    }
}