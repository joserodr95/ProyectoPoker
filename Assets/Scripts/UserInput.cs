using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        GetMouseClick();
    }

    public void MensajeDescartar () {
        Debug.Log("Descartar");
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
                    CardClickActions(hit.collider.gameObject);
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

    private void CardClickActions(GameObject go) {
        // Card click actions
        Debug.LogFormat("Card {0} clicked", go.name);

        Selectable goSel = go.GetComponent<Selectable>();

        if(goSel.FaceUp) {
            // También podría ser: goSel.Selected = !goSel.Selected;
            goSel.Selected ^= true;
            //go.GetComponent<CartaEnJuegoInfo>().indexEnMano;
        }
    }

    private void TopClickActions() {
        // Top click actions
        Debug.Log("Top clicked");
    }

    private void BottomClickActions() {
        // Bottom click actions
        Debug.Log("Bottom clicked");
    }

}
