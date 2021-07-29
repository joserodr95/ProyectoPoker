using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solitaire : MonoBehaviour {

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

    public static List<string> deck;

    // Start is called before the first frame update
    void Start() {
        PlayCards();
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayCards() {
        deck = GenerateDeck();
        Shuffle();

        // test the cards in the deck:
        foreach(string card in deck) {
            Debug.Log(card);
        }
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

    public static void Shuffle() {
        System.Random rng = new System.Random();
        deck = deck.OrderBy(a => rng.Next()).ToList();
    }
}
