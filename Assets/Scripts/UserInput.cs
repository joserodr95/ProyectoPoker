using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

    //public GameObject slot1;
    //private Solitaire solitaire;

    // Start is called before the first frame update
    void Start() {
        //solitaire = FindObjectOfType<Solitaire>();
        //slot1 = this.gameObject;
    }

    // Update is called once per frame
    void Update() {
        GetMouseClick();
    }

    private void GetMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                // what has been hit? Deck/Card/EmptySlot...
                if (hit.collider.CompareTag("Deck")) {
                    // Clicked deck
                    DeckClickActions();
                } else if (hit.collider.CompareTag("Card")) {
                    // Clicked card
                    //CardClickActions(hit.collider.gameObject);
                } else if (hit.collider.CompareTag("Top")) {
                    // Clicked top
                    TopClickActions();
                } else if (hit.collider.CompareTag("Bottom")) {
                    // Clicked bottom
                    BottomClickActions();
                } else {
                    // Not implemented
                    Debug.LogFormat("Clicked on the non-implemented tag: {0}", hit.collider.tag);
                }
            }
        }
    }

    private void DeckClickActions() {
        // Deck click actions
        Debug.Log("Deck clicked");
        //solitaire.DealFromDeck();
    }

    /*
    private void CardClickActions(GameObject go) {
        // Card click actions
        Debug.LogFormat("Card {0} clicked", go.name);

        Solitaire solitaire = FindObjectOfType<Solitaire>();
        Selectable goSel = go.GetComponent<Selectable>();

        // If the card clicked on is facedown
        // If the card clicked on is not blocked
        // Flip it over

        // If the card clicked on is in the deck pile with the trips
        // If it is not blocked
        // Select it

        // If the card is face up
        // If there is no card currently selected
        // Select the card

        if(goSel.FaceUp) {
            goSel.Selected ^= true;
            if (goSel.Selected) {
                solitaire.SelectedCards.Add(goSel.gameObject);
                if (solitaire.SelectedCards.Count == 5) {
                    foreach (GameObject gameObject in solitaire.SelectedCards) {
                        CartaComponente dcParaEliminar = gameObject.GetComponent<CartaComponente>();
                        solitaire.Hileras[dcParaEliminar.hileraNum].cartas.RemoveAt(dcParaEliminar.posEnHilera);
                        Destroy(gameObject);
                    }
                    solitaire.SelectedCards.Clear();
                    solitaire.RevelaCartasEnTopDeHileras();
                }
            } else {
                solitaire.SelectedCards.Remove(goSel.gameObject);
            }
        }

            // If there is already a card selected AND it is not the same card
                // If the new card is eligable to stack on the old card
                    // Stack it
                // Else
                    // Select the new card

            // Else If there is already a card selected an it is the same card
                // If the time is short enough then it is a double click
                    // If the card is eligible to fly up top then do it
    }
    */

    private void TopClickActions() {
        // Top click actions
        Debug.Log("Top clicked");
    }

    private void BottomClickActions() {
        // Bottom click actions
        Debug.Log("Bottom clicked");
    }

}
