using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
    
    [SerializeField]
    private CardsGroup hand = new CardsGroup();

    [SerializeField]
    internal List<bool> toDiscard = new List<bool>(new bool[5]);
    
    internal List<CardComponent> ccHand = new List<CardComponent>();

    internal Transform cardsParent;

    internal Vector3[] cardsPositions = 
        {
            new Vector3(-2.5f, 0f, 0f),
            new Vector3(-1.25f, 0f, 0f), 
            new Vector3(0.00f, 0f, 0f),
            new Vector3(+1.25f, 0f, 0f), 
            new Vector3(+2.5f, 0f, 0f),
        };

    internal string name;
    internal int seat;
    
    internal CardsGroup Hand { get => hand; set => hand = value; }
    
    public Player(int seat) {
        this.seat = seat;
        this.name = $"Player0{seat}";
        cardsParent = GameObject.Find(name).transform.GetChild(0);
    }
}
