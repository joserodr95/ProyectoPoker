using UnityEngine;
using static  HandsCalculator;

public class UserInput : MonoBehaviour {

    public PokerManager pokerManager;
    
    private Selectable seleccionable;
    private InGameCardInfo cardInfo;

    private Camera mCam;

    private void Start()
    {
        mCam = Camera.main;
    }

    private void Update() {
        GetMouseClick();
    }

    private void GetMouseClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        
        Collider2D coll = Physics2D.Raycast(
            mCam.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero)
            .collider;
        if (ReferenceEquals(coll, null)) return;
        switch (coll.tag) {
            case "Card":
                CardClickActions(coll.gameObject);
                break;
        }
    }

    private void CardClickActions(GameObject go) {
        // Card click actions
        //Debug.LogFormat("Card {0} clicked", go.name);

        seleccionable = go.GetComponent<Selectable>();
        cardInfo = go.GetComponent<InGameCardInfo>();

        if (!seleccionable.FaceUp) return;
        seleccionable.Selected = !seleccionable.Selected;
        pokerManager.players[0].toDiscard[cardInfo.indexAtHand] = seleccionable.Selected;
    }

    public void Discard() {
        for (int i = 0; i < 5; i++)
            if (pokerManager.players[0].toDiscard[i])
            {
                pokerManager.discartedCards.cards.Add(pokerManager.players[0].Hand.cards[i]);
                pokerManager.players[0].Hand.cards[i].UpdateCardValues(pokerManager.players[0].Hand.cards[i],
                    pokerManager.DrawFromTopOfDeck());
                pokerManager.players[0].toDiscard[i] = false;
                pokerManager.players[0].ccHand[i].GetComponent<Selectable>().Selected = false;
            }
    }

    public void CheckHandValue() {
        Debug.Log(CalculateHandRank(pokerManager.players[0].Hand));
    }

}
