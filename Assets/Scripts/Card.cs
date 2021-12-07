using System.Collections;
using UnityEngine;

[System.Serializable]
public class Card {

    public string name;
    public ESuit suit;
    public ERank rank;

    public Card() { }

    public Card(Card card) {
        this.name = card.name;
        this.suit = card.suit;
        this.rank = card.rank;
    }

    public void UpdateCardValues(Card cardToUpdate, Card cardWithNewValues) {
        cardToUpdate.name = cardWithNewValues.name;
        cardToUpdate.suit = cardWithNewValues.suit;
        cardToUpdate.rank = cardWithNewValues.rank;
    }

    public Card(ESuit suit, ERank rank) {
        this.suit = suit;
        this.rank = rank;
        this.name = this.ToString();
    }

    public override string ToString() {
        name = "";

        switch (rank) {
            case ERank.TWO:
                name += 2;
                break;
            case ERank.THREE:
                name += 3;
                break;
            case ERank.FOUR:
                name += 4;
                break;
            case ERank.FIVE:
                name += 5;
                break;
            case ERank.SIX:
                name += 6;
                break;
            case ERank.SEVEN:
                name += 7;
                break;
            case ERank.EIGHT:
                name += 8;
                break;
            case ERank.NINE:
                name += 9;
                break;
            case ERank.TEN:
                name += 10;
                break;
            case ERank.JACK:
                name += 11;
                break;
            case ERank.QUEEN:
                name += 12;
                break;
            case ERank.KING:
                name += 13;
                break;
            case ERank.AS:
                name += 14;
                break;
            default:
                Debug.LogError("Rank not implemented.");
                break;
        }

        switch (suit) {
            case ESuit.CLUBS:
                name += "CL";
                break;
            case ESuit.DIAMONDS:
                name += "DI";
                break;
            case ESuit.HEARTS:
                name += "HE";
                break;
            case ESuit.SPADES:
                name += "SP";
                break;
            default:
                Debug.LogError("Suit not implemented.");
                break;
        }

        return name;
    }
}

public enum ESuit {
    CLUBS       = 1,
    DIAMONDS    = 2,
    HEARTS      = 3,
    SPADES      = 4,
}
public enum ERank {
    TWO = 2,
    THREE = 3,
    FOUR = 4,
    FIVE = 5,
    SIX = 6,
    SEVEN = 7,
    EIGHT = 8,
    NINE = 9,
    TEN = 10,
    JACK = 11,
    QUEEN = 12,
    KING = 13,
    AS = 14
}
