using UnityEngine;

/// <summary>
/// Class that holds the main info about a card.
/// </summary>
[System.Serializable]
public class Card {
    
    public string name;
    public ESuit suit;
    public ERank rank;
    public bool hasSignificance;

    public Card() {}

    public Card(Card card) {
        this.name = card.name;
        this.suit = card.suit;
        this.rank = card.rank;
        this.hasSignificance = card.hasSignificance;
    }
    
    public Card(ESuit suit, ERank rank, bool hasSignificance = false) {
        this.suit = suit;
        this.rank = rank;
        this.hasSignificance = hasSignificance;
        this.name = this.ToString();
    }

    /// <summary>
    /// Updates the values of card.
    /// </summary>
    /// <param name="cardWithNewValues">The card that has the new values that will be passed.</param>
    public void UpdateCardValues(Card cardWithNewValues)
    {
        this.name = cardWithNewValues.name;
        this.suit = cardWithNewValues.suit;
        this.rank = cardWithNewValues.rank;
        this.hasSignificance = cardWithNewValues.hasSignificance;
    }

    public static bool operator<(Card left, Card right)
    {
        if (left.rank == right.rank) return left.suit < right.suit;

        return left.rank < right.rank;
    }

    public static bool operator>(Card left, Card right)
    {
        if (left.rank == right.rank) return left.suit > right.suit;

        return left.rank > right.rank;
    }
    
    public sealed override string ToString() {
        name = "";

        switch (rank) {
            case ERank.Two:
                name += 2;
                break;
            case ERank.Three:
                name += 3;
                break;
            case ERank.Four:
                name += 4;
                break;
            case ERank.Five:
                name += 5;
                break;
            case ERank.Six:
                name += 6;
                break;
            case ERank.Seven:
                name += 7;
                break;
            case ERank.Eight:
                name += 8;
                break;
            case ERank.Nine:
                name += 9;
                break;
            case ERank.Ten:
                name += 10;
                break;
            case ERank.Jack:
                name += 11;
                break;
            case ERank.Queen:
                name += 12;
                break;
            case ERank.King:
                name += 13;
                break;
            case ERank.Ace:
                name += 14;
                break;
            default:
                Debug.LogError("Rank not implemented.");
                break;
        }

        switch (suit) {
            case ESuit.Clubs:
                name += "CL";
                break;
            case ESuit.Diamonds:
                name += "DI";
                break;
            case ESuit.Hearts:
                name += "HE";
                break;
            case ESuit.Spades:
                name += "SP";
                break;
            case ESuit.Stars:
                name += "ST";
                break;
            default:
                Debug.LogError("Suit not implemented.");
                break;
        }

        return name;
    }
}

public enum ESuit {
    Clubs       = 1,
    Diamonds    = 2,
    Hearts      = 3,
    Spades      = 4,
    Stars       = 5,
}

public enum ERank {
    Two    = 2,
    Three  = 3,
    Four   = 4,
    Five   = 5,
    Six    = 6,
    Seven  = 7,
    Eight  = 8,
    Nine   = 9,
    Ten    = 10,
    Jack   = 11,
    Queen  = 12,
    King   = 13,
    Ace    = 14
}
