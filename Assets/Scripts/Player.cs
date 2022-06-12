using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [NonSerialized]
    private bool eliminated;
    [NonSerialized]
    private bool isFolding;
    
    [SerializeField]
    private CardsGroup hand = new CardsGroup();
    private GameObject gameObject;
    private GameObject scoreBoard;
    
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


    public bool IsFolding
    {
        get => isFolding;
        set
        { 
            isFolding = value;
            
            foreach (CardComponent cc in ccHand)
            {
                cc.GetComponent<Selectable>().Folded = isFolding;
            }

            if (seat != 1) return;
            
            if (isFolding)
            {
                BettingManager.Instance.PlayerFolded(0);
                UserInput.Instance.buttons[0].gameObject.SetActive(!isFolding);
            }

            UserInput.Instance.buttons[1].gameObject.SetActive(!isFolding);
        } 
    }

    public bool Eliminated
    {
        get => eliminated;
        set
        {
            if (value)
            {
                this.gameObject.SetActive(false);
                this.scoreBoard.SetActive(false);
            }

            eliminated = value;
        }
    }

    public Player(int seat) {
        this.seat = seat;
        this.name = $"Player0{seat}";
        gameObject = GameObject.Find(name);
        cardsParent = gameObject.transform.GetChild(0);
        scoreBoard = GameObject.Find($"Scoreboard_0{seat}");
    }
}
