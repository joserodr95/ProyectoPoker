using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solitaire : MonoBehaviour {

    [field: SerializeField]
    public List<Sprite> CardFaces { get; set; } = new List<Sprite>(52);
    [field: SerializeField]
    public GameObject CardPrefab { get; set; }
    [field: SerializeField]
    public GameObject DeckButton { get; set; }
    [field: SerializeField]
    public GameObject[] BottomPos { get; set; }
    [field: SerializeField]
    public GameObject[] TopPos { get; set; }

    [field: SerializeField]
    public GameObject InPlayCardsParent { get; set; }

    [field: SerializeField]
    public static string[] Suits { get; set; } = new string[] { "C", "D", "H", "S" };
    [field: SerializeField]
    public static string[] Values { get; set; } = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    [field: SerializeField]
    public List<string>[] Bottoms { get; set; }
    [field: SerializeField]
    public List<string>[] Tops { get; set; }
    [field: SerializeField]
    public List<string> TripsOnDisplay { get; set; } = new List<string>();
    [field: SerializeField]
    public List<List<string>> DeckTrips { get; set; } = new List<List<string>>();

    [field: SerializeField]
    public List<string> Deck { get; set; }
    [field: SerializeField]
    public List<string> DiscardPile { get; set; }

    private int deckLocation;
    private int trips;
    private int tripsRemainder;

    // Actualmente se podrian sustituir por "new List<string>()" cuando se rellena Bottoms
    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();


    // Start is called before the first frame update
    void Start() {
        Bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
        PlayCards();
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayCards() {
        Deck = GenerateDeck();
        Deck = Shuffle(Deck);

        // test the cards in the deck:
        foreach(string card in Deck) {
            Debug.Log(card);
        }

        SolitaireSort();
        StartCoroutine(SolitaireDeal());
        SortDeckIntoTrips();
    }

    public static List<string> GenerateDeck() {
        List<string> newDeck = new List<string>();

        foreach(string s in Suits) {
            foreach(string v in Values) {
                newDeck.Add(s + v);
            }
        }

        return newDeck;
    }

    public static List<string> Shuffle(List<string> deck) {
        System.Random rng = new System.Random();
        return deck.OrderBy(_ => rng.Next()).ToList();
    }

    private IEnumerator SolitaireDeal() {

        for (int i = 0; i < 7; i++) {
            float yOffset = 0;
            float zOffset = .03f;

            Vector3 posInitial = BottomPos[i].transform.position;
            foreach (string card in Bottoms[i]) {
                yield return new WaitForSeconds(0.03f);
                GameObject newCard = Instantiate(CardPrefab,
                                                 new Vector3(posInitial.x, posInitial.y - yOffset, posInitial.z - zOffset),
                                                 Quaternion.identity,
                                                 InPlayCardsParent.transform);
                newCard.name = card;
                if(card == Bottoms[i][Bottoms[i].Count -1]) {
                    newCard.GetComponent<Selectable>().FaceUp = true;
                }

                yOffset += .5f;
                zOffset += .03f;
                DiscardPile.Add(card);
            }
        }

        foreach (string card in DiscardPile) {
            if (Deck.Contains(card)) {
                Deck.Remove(card);
            }
        }
        DiscardPile.Clear();

    }

    private void SolitaireSort() {
        for (int i = 0; i < 7; i++) {
            for (int j = i; j < 7; j++) {
                Bottoms[j].Add(Deck.Last<string>());
                Deck.RemoveAt(Deck.Count - 1);
            }
        }
    }

     private void SortDeckIntoTrips() {
        trips = Deck.Count / 3;
        tripsRemainder = Deck.Count % 3;
        DeckTrips.Clear();

        int modifier = 0;
        for (int i = 0; i < trips; i++) {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 3; j++) {
                myTrips.Add(Deck[j + modifier]);
            }
            DeckTrips.Add(myTrips);
            modifier += 3;
        }
        if (tripsRemainder != 0) {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < tripsRemainder; k++) {
                myRemainders.Add(Deck[Deck.Count - tripsRemainder + modifier]);
                modifier++;
            }
            DeckTrips.Add(myRemainders);
            trips++;
        }
        deckLocation = 0;
     }

    public void DealFromDeck() {

        // Add remaining cards to discard pile
        foreach (Transform child in DeckButton.transform) {
            if (child.CompareTag("Card")) {
                Deck.Remove(child.name);
                DiscardPile.Add(child.name);
                 Destroy(child.gameObject);
            }
        }

        if (deckLocation < trips) {
            // draw 3 new cards
            TripsOnDisplay.Clear();
            float xOffset = 2.5f;
            float zOffset = -.2f;

            foreach (string card in DeckTrips[deckLocation]) {
                GameObject newTopCard = Instantiate(CardPrefab,
                    DeckButton.transform.position + new Vector3(xOffset, 0f, zOffset),
                    //new Vector3(DeckButton.transform.position.x)
                    Quaternion.identity,
                    DeckButton.transform);
                newTopCard.transform.localScale = Vector3.one;
                xOffset += .5f;
                zOffset -= .2f;
                newTopCard.name = card;
                TripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().FaceUp = true;
            }
            deckLocation++;
        }
        else {
            RestackTopDeck();
        }
    }

    private void RestackTopDeck() {
        foreach (string card in DiscardPile) {
            Deck.Add(card);
        }
        DiscardPile.Clear();
        SortDeckIntoTrips();
    }

}
