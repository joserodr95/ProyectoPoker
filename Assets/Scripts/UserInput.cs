using System.Linq;
using UnityEngine;

public class UserInput : MonoBehaviour {

    public PokerManager pokerManager;
    Selectable seleccionable;
    InGameCardInfo cardInfo;

    // Update is called once per frame
    void Update() {
        GetMouseClick();
    }

    private void GetMouseClick() {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            switch (hit.collider?.tag) {
                case "Card":
                    CardClickActions(hit.collider.gameObject);
                    break;
                default:
                    break;
            }
        }
    }

    private void CardClickActions(GameObject go) {
        // Card click actions
        Debug.LogFormat("Card {0} clicked", go.name);

        seleccionable = go.GetComponent<Selectable>();
        cardInfo = go.GetComponent<InGameCardInfo>();

        if (seleccionable.FaceUp) {
            seleccionable.Selected = cardInfo.toDiscard = !seleccionable.Selected;
            pokerManager.players[0].toDiscard[cardInfo.indexAtHand] = seleccionable.Selected;
        }
    }

    public void Discard() {
        for (int i = 0; i < 5; i++) {
            if (pokerManager.players[0].toDiscard[i]) {
                pokerManager.discartedCards.cards.Add(pokerManager.players[0].hand.cards[i]);
                pokerManager.players[0].hand.cards[i] = pokerManager.DrawFromTopOfDeck();
                pokerManager.players[0].ccHand[i].Init(pokerManager.players[0].hand.cards[i]);
            }
        }
    }

    public void CheckHandValue() {
        pokerManager.players[0].hand.cards = pokerManager.players[0].hand.cards.OrderBy(c => c.rank).ThenBy(c => c.suit).ToList<Card>();
    }

}
