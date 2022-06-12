using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static HandsCalculator;
using Random = UnityEngine.Random;

public class UserInput : MonoBehaviour
{
    public enum EPhase
    {
        Discarting,
        Betting
    }

    [SerializeField] public List<Button> buttons = new List<Button>();
    [SerializeField] public List<TextMeshProUGUI> playerMessages = new List<TextMeshProUGUI>();

    public PokerManager pokerManager;

    public bool selectableCards = true;

    [SerializeField] private BettingManager _bettingManager;

    private EPhase _currentPhase = EPhase.Discarting;
    private InGameCardInfo cardInfo;
    private bool controlable = true;
    private Camera mCam;

    private Selectable seleccionable;
    public static UserInput Instance { get; private set; }

    private EPhase CurrentPhase
    {
        get => _currentPhase;
        set
        {
            _currentPhase = value;
            buttons[1].interactable = controlable = true;
            switch (_currentPhase)
            {
                case EPhase.Discarting:
                {
                    PhaseButtonsToggler(false);

                    break;
                }
                case EPhase.Betting:
                {
                    PhaseButtonsToggler(true);
                    _bettingManager.ReevaluateBetButtons();

                    for (int i = 0; i < _bettingManager.playersMoneyBeforeRound.Length; i++)
                        _bettingManager.playersMoneyBeforeRound[i] = _bettingManager.playersMoney[i];

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(_currentPhase), _currentPhase, null);
            }
        }
    }

    private void Start()
    {
        Instance = this;
        mCam = Camera.main;
    }

    private void Update()
    {
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
        switch (coll.tag)
        {
            case "Card":
                CardClickActions(coll.gameObject);
                break;
        }
    }

    private void CardClickActions(GameObject go)
    {
        // Card click actions
        //Debug.LogFormat("Card {0} clicked", go.name);

        if (!controlable) return;

        seleccionable = go.GetComponent<Selectable>();
        cardInfo = go.GetComponent<InGameCardInfo>();

        if (!seleccionable.FaceUp) return;
        if (cardInfo.playerOwner == 1 && selectableCards)
        {
            seleccionable.Selected = !seleccionable.Selected;
            pokerManager.players[0].toDiscard[cardInfo.indexAtHand] = seleccionable.Selected;
        }
    }

    private void PhaseButtonsToggler(bool flag)
    {
        selectableCards = !flag;

        foreach (Button betButton in _bettingManager.betButtons) betButton.gameObject.SetActive(flag);
        buttons[0].gameObject.SetActive(flag);
        buttons[1].interactable = !flag;
    }

    public void StartCurrentPhase()
    {
        switch (CurrentPhase)
        {
            case EPhase.Discarting:
                StartDiscard();
                break;
            case EPhase.Betting:
                StartBetting();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    ///     Discards your cards marked to discard or the non-significant cards for the CPU players.
    /// </summary>
    private void StartDiscard()
    {
        if (controlable) StartCoroutine(Discard());
    }

    /// <summary>
    ///     Confirms your betting.
    /// </summary>
    private void StartBetting()
    {
        if (controlable) StartCoroutine(Betting());
    }

    public void SetPlayerMessageStyle(int playerNum, bool winner = false)
    {
        if (winner)
        {
            TextMeshProUGUI winnerMsg = playerMessages[playerNum];

            winnerMsg.color = CustomColor.winnerVertexColor;
            winnerMsg.enableVertexGradient = true;
            winnerMsg.colorGradient =
                new VertexGradient(CustomColor.winnerGold, CustomColor.winnerGold, Color.white, Color.white);
        }
        else
        {
            TextMeshProUGUI winnerMsg = playerMessages[playerNum];

            winnerMsg.enableVertexGradient = false;
            winnerMsg.color = Color.white;
        }
    }

    /// <summary>
    ///     Disables all the player messages and set them the default white style.
    /// </summary>
    internal void ResetPlayerMessages()
    {
        for (int i = 0; i < playerMessages.Count; i++)
        {
            playerMessages[i].enabled = false;
            SetPlayerMessageStyle(i);
        }
    }

    private IEnumerator Discard()
    {
        buttons[1].interactable = controlable = false;

        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        for (int pNum = 0; pNum < pokerManager.players.Count; pNum++)
        {
            Player player = pokerManager.players[pNum];
            CardsGroup pHand = player.Hand;

            // Debug.Log("Finished WaitForSeconds at timestamp : " + Time.time);

            if (pNum == 0) RealPlayerDiscard(player, ref pHand);
            else StartCoroutine(CpuPlayerDiscard(player, pHand));

            yield return new WaitForSeconds(.5f);
        }

        CurrentPhase = EPhase.Betting;
    }

    private void RealPlayerDiscard(Player player, ref CardsGroup pHand)
    {
        for (int i = 0; i < pHand.cards.Count; i++)
        {
            if (!player.toDiscard[i]) continue;

            pokerManager.AddToDiscardPile(pHand.cards[i]);
            player.Hand.cards[i].UpdateCardValues(pokerManager.DrawFromTopOfDeck());
            player.toDiscard[i] = false;
            player.ccHand[i].GetComponent<Selectable>().Selected = false;
        }

        pHand.CalculateHandRank();
    }

    private IEnumerator CpuPlayerDiscard(Player player, CardsGroup pHand)
    {
        if (player.Eliminated) yield break;

        int numberOfLeftSideDiscards = NumberOfLeftCardsToDiscard(pHand.CalculateHandRank());

        for (int i = 0; i < pHand.cards.Count; i++)
        {
            if (i >= numberOfLeftSideDiscards) continue;

            pokerManager.AddToDiscardPile(pHand.cards[i]);
            pHand.cards[i].UpdateCardValues(
                pokerManager.DrawFromTopOfDeck()
            );
            player.ccHand[i].GetComponent<Selectable>().Selected = true;
        }

        yield return WaitForSecondsHelper.GetWait(.66f);
        foreach (CardComponent cc in player.ccHand) cc.GetComponent<Selectable>().Selected = false;

        pHand.CalculateHandRank();
    }

    private IEnumerator Betting()
    {
        buttons[0].interactable = false;
        buttons[1].interactable = false;
        _bettingManager.ToggleBetButtons(false);

        _bettingManager.playersBetsThisBettingRound = new[] {0, 0, 0, 0};

        // CPU betting
        yield return CpuBetting();

        while (!_bettingManager.ShouldBettingBeDone() && pokerManager.players[0].IsFolding) yield return CpuBetting();

        if (_bettingManager.ShouldBettingBeDone())
        {
            pokerManager.CheckHandValue();
            pokerManager.FlipCards();

            yield return new WaitForSeconds(5f);

            _bettingManager.FinishRound(pokerManager.lastWinner);
            CurrentPhase = EPhase.Discarting;
            pokerManager.NewRound();
        }

        buttons[0].interactable = true;
        _bettingManager.ToggleBetButtons(true);
        _bettingManager.ReevaluateBetButtons();
        yield return null;
    }

    private IEnumerator CpuBetting()
    {
        for (int i = 1; i < 4; i++)
        {
            if (pokerManager.players[i].IsFolding || pokerManager.players[i].Eliminated) continue;

            int extraMin =
                _bettingManager.higherBetThisRound == 0 ? Random.Range(1, 5) : 0; // Avoids 0 as a valid bet
            int playerBet = _bettingManager.CalculateCpuPlayerBetDecision(i) + extraMin;
            // Debug.Log($"if ({playerBet} + {_bettingManager.playersBets[i]} < {_bettingManager.higherBetThisRound}) Fold({i});");
            if (playerBet + _bettingManager.playersBets[i] < _bettingManager.higherBetThisRound)
            {
                pokerManager.players[i].IsFolding = true;
            }
            else
            {
                if (Random.Range(1, 4) == 3)
                {
                    _bettingManager.SubstractMoneyToPlayer(playerBet, i);
                }
                else
                {
                    _bettingManager.SubstractMoneyToPlayer(
                        _bettingManager.playersBets.Max() - _bettingManager.playersBets[i] + extraMin, i);
                }
            }

            yield return new WaitForSeconds(1.5f);
        }
    }

    private static int NumberOfLeftCardsToDiscard(EHandRanks calculateHandRank)
    {
        switch (calculateHandRank)
        {
            case EHandRanks.HighCard:
                return 4;
            case EHandRanks.OnePair:
                return 3;
            case EHandRanks.TwoPair:
                return 1;
            case EHandRanks.ThreeOfKind:
                return 2;
            case EHandRanks.Straight:
            case EHandRanks.Flush:
            case EHandRanks.FullHouse:
            case EHandRanks.FourOfKind:
            case EHandRanks.StraightFlush:
            case EHandRanks.RoyalStraightFlush:
                return 0;
            default:
                throw new ArgumentOutOfRangeException(nameof(calculateHandRank), calculateHandRank, null);
        }
    }

    public void Fold()
    {
        pokerManager.players[0].IsFolding = true;
    }
}