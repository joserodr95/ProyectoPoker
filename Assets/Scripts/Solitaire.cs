using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solitaire : MonoBehaviour {

    public List<Sprite> cardFaces = new List<Sprite>(52);
    public GameObject cardPrefab;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    public GameObject parent;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
    public List<string>[] bottoms;
    public List<string>[] tops;

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();

    public List<string> deck;

    // Start is called before the first frame update
    void Start() {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
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

        SolitaireSort();
        StartCoroutine(SolitaireDeal());
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

    private IEnumerator SolitaireDeal() {

        for (int i = 0; i < 7; i++) {
            float yOffset = 0;
            float zOffset = .03f;

            Vector3 posInitial = bottomPos[i].transform.position;
            foreach (string card in bottoms[i]) {
                yield return new WaitForSeconds(0.05f);
                GameObject newCard = Instantiate(cardPrefab,
                                                 new Vector3(posInitial.x, posInitial.y - yOffset, posInitial.z - zOffset),
                                                 Quaternion.identity,
                                                 parent.transform);
                newCard.name = card;
                if(card == bottoms[i][bottoms[i].Count -1]) {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

                yOffset += .5f;
                zOffset += .03f;
            }
        }
    }

    private void SolitaireSort() {
        for (int i = 0; i < 7; i++) {
            for (int j = i; j < 7; j++) {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

}
