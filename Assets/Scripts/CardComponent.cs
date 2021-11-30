using System.Collections;
using UnityEngine;

public class CardComponent : MonoBehaviour {

    public new string name;
    public ESuit suit;
    public ERank rank;
    public GameObject goOwner;

    public void Init(Card card) {
        this.suit = card.suit;
        this.rank = card.rank;
        this.name = card.name;
        this.gameObject.name = this.name;
    }

    private void Start() {
        goOwner = this.gameObject;
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
                Debug.LogError("Rank of card not implemented.");
                break;
        }

        switch (suit) {
            case ESuit.HEARTS:
                name += "CO";
                break;
            case ESuit.DIAMONDS:
                name += "DI";
                break;
            case ESuit.SPADES:
                name += "PI";
                break;
            case ESuit.CLUBS:
                name += "TR";
                break;
            default:
                Debug.LogError("Suit of card not implemented.");
                break;
        }

        return name;
    }
}
