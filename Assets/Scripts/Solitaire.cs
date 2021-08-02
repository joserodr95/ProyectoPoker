using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solitaire : MonoBehaviour {

    public List<Sprite> cardFaces = new List<Sprite>(52);
    public GameObject cardPrefab;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

    public List<string> deck;

    // Start is called before the first frame update
    void Start() {
        PlayCards();
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayCards() {
        deck = GenerateDeck();
        deck = Shuffle(deck);

        // test the cards in the deck:
        foreach(string card in deck) {
            Debug.Log(card);
        }

        SolitaireDeal();
    }

    public static List<string> GenerateDeck() {
        List<string> newDeck = new List<string>();

        foreach(string s in suits) {
            foreach(string v in values) {
                newDeck.Add(s + v);
            }
        }

        return newDeck;
    }

    public static List<string> Shuffle(List<string> deck) {
        System.Random rng = new System.Random();
        deck = deck.OrderBy(_ => rng.Next()).ToList();

        return deck;
    }

    private void SolitaireDeal() {
        float yOffset = 0;
        float zOffset = .03f;

        foreach (string card in deck) {
            GameObject newCard = Instantiate(cardPrefab,
                                             new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z - zOffset),
                                             Quaternion.identity);
            newCard.name = card;
            newCard.GetComponent<Selectable>().faceUp = true;

            yOffset += .8f;
            zOffset += .03f;
        }
    }
}
