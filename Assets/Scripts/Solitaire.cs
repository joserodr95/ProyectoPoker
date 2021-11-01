using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Solitaire : MonoBehaviour {

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
    public ConjuntoCartas[] Hileras { get; set; }
    [field: SerializeField]
    public List<string>[] Tops { get; set; }
    [field: SerializeField]
    public List<string> TripsOnDisplay { get; set; } = new List<string>();
    [field: SerializeField]
    public List<List<string>> DeckTrips { get; set; } = new List<List<string>>();

    private ConjuntoCartas MazoBase { get; set; } = new ConjuntoCartas();
    [field: SerializeField]
    public ConjuntoCartas MazoEnJuego { get; set; } = new ConjuntoCartas();
    [field: SerializeField]
    public ConjuntoCartas DiscardPile { get; set; } = new ConjuntoCartas();

    [field: SerializeField]
    public List<GameObject> SelectedCards { get; set; } = new List<GameObject>();

    private int deckLocation;
    private int trips;
    private int tripsRemainder;

    // Actualmente se podrian sustituir por "new List<string>()" cuando se rellena Bottoms
    private ConjuntoCartas hilera0 = new ConjuntoCartas();
    private ConjuntoCartas hilera1 = new ConjuntoCartas();
    private ConjuntoCartas hilera2 = new ConjuntoCartas();
    private ConjuntoCartas hilera3 = new ConjuntoCartas();
    private ConjuntoCartas hilera4 = new ConjuntoCartas();
    private ConjuntoCartas hilera5 = new ConjuntoCartas();
    private ConjuntoCartas hilera6 = new ConjuntoCartas();


    // Start is called before the first frame update
    void Start() {
        Hileras = new ConjuntoCartas[] { hilera0, hilera1, hilera2, hilera3, hilera4, hilera5, hilera6};
        MazoBase.RellenaMazoBasico();
        MazoEnJuego.RellenaMazoBasico();
        MazoEnJuego.Barajea();
        PlayCards();
    }

    public void PlayCards() {

        // test the cards in the deck:
        foreach (DataCarta dc in MazoEnJuego.cartas) {
            string color = (dc.palo.Equals(EPalo.DIAMANTES) || dc.palo.Equals(EPalo.CORAZONES) ? "#FF7F7F" : "black");
            Debug.LogFormat("<color=\"{0}\"><b>{1}</b></color>", color, dc);
        }

        RellenaHileras();
        StartCoroutine(SolitaireDeal());
        //SortDeckIntoTrips();
    }

    public static Dictionary<string, DataCarta> Barajea(Dictionary<string, DataCarta> mazo) {
        System.Random rng = new System.Random();
        return mazo.OrderBy(_ => rng.Next()).ToDictionary(cKVP => cKVP.Key, cKVP => cKVP.Value );
    }

    private IEnumerator SolitaireDeal() {

        for (int i = 0; i < 7; i++) {
            float yOffset = 0;
            float zOffset = .03f;

            Vector3 posInitial = BottomPos[i].transform.position;
            foreach (DataCarta carta in Hileras[i].cartas) {
                yield return new WaitForSeconds(0.03f);
                GameObject nuevaCarta = Instantiate(CardPrefab,
                                                 new Vector3(posInitial.x, posInitial.y - yOffset, posInitial.z - zOffset),
                                                 Quaternion.identity,
                                                 InPlayCardsParent.transform);
                nuevaCarta.name = carta.ToString();
                RellenaDataCarta(nuevaCarta, carta.palo, carta.rango);

                if(carta == Hileras[i].cartas[Hileras[i].cartas.Count-1]) {
                    nuevaCarta.GetComponent<Selectable>().FaceUp = true;
                }

                yOffset += .5f;
                zOffset += .03f;
                DiscardPile.cartas.Add(carta);
                MazoBase.cartas.Remove(carta);
            }
        }

        // ???
        //foreach (string card in DiscardPile) {
        //    if (MazoBase.cartas.ForEach(c => c.ToString().Equals(card)) /*MazoBase.ContainsKey(card))*/ {
        //        MazoBase.Remove(card);
        //    }
        //}
        DiscardPile.cartas.Clear();

    }

    private void RellenaDataCarta(GameObject carta, EPalo palo, ERango rango) {
        DataCarta dc = carta.GetComponent<DataCarta>();
        dc.name = carta.name;
        dc.palo = palo;
        dc.rango = rango;
    }

    private void RellenaHileras() {
        for (int i = 0; i < 7; i++) {
            for (int j = i; j < 7; j++) {
                Hileras[j].cartas.Add(MazoEnJuego.cartas[MazoEnJuego.cartas.Count - 1]);
                MazoEnJuego.cartas.RemoveAt(MazoEnJuego.cartas.Count - 1);
            }
        }
    }

     //private void SortDeckIntoTrips() {
     //   trips = MazoBase.Count / 3;
     //   tripsRemainder = MazoBase.Count % 3;
     //   DeckTrips.Clear();

     //   int modifier = 0;
     //   for (int i = 0; i < trips; i++) {
     //       List<string> myTrips = new List<string>();
     //       for (int j = 0; j < 3; j++) {
     //           myTrips.Add(MazoBase.ElementAt(j + modifier).Key);
     //       }
     //       DeckTrips.Add(myTrips);
     //       modifier += 3;
     //   }
     //   if (tripsRemainder != 0) {
     //       List<string> myRemainders = new List<string>();
     //       modifier = 0;
     //       for (int k = 0; k < tripsRemainder; k++) {
     //           myRemainders.Add(MazoBase.ElementAt(MazoBase.Count - tripsRemainder + modifier).Key);
     //           modifier++;
     //       }
     //       DeckTrips.Add(myRemainders);
     //       trips++;
     //   }
     //   deckLocation = 0;
     //}

    public void DealFromDeck() {

        //// Add remaining cards to discard pile
        //foreach (Transform child in DeckButton.transform) {
        //    if (child.CompareTag("Card")) {
        //        MazoBase.cartas.Remove(child.name);
        //        DiscardPile.Add(child.name);
        //         Destroy(child.gameObject);
        //    }
        //}

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
            //RestackTopDeck();
        }
    }

    //private void RestackTopDeck() {
    //    foreach (string card in DiscardPile) {
    //        Mazo.Add(card);
    //    }
    //    DiscardPile.Clear();
    //    SortDeckIntoTrips();
    //}

}
