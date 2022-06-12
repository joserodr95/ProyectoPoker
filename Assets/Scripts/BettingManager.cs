using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BettingManager : MonoBehaviour
{
    public static BettingManager Instance { get; private set; }

    [SerializeField] internal Button[] betButtons;

    [SerializeField] private TextMeshProUGUI[] playersScoreBoards;
    [SerializeField] private TextMeshProUGUI jackpot;

    public int higherBetThisRound = 1;

    public int[] playersBets = {0, 0, 0, 0};
    public int[] playersBetsThisBettingRound = {0, 0, 0, 0};
    public int[] playersMoney = {50, 50, 50, 50}; // Change only in Editor
    public int[] playersMoneyBeforeRound = {0, 0, 0, 0};
    public int totalBetted = 0;

    private void Start()
    {
        Instance = this;

        List<Player> players = PokerManager.Instance.players;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].Eliminated) continue;

            playersScoreBoards[i * 2].text = playersMoney[i].ToString();
        }
    }

    public void ToggleBetButtons(bool newState)
    {
        foreach (Button betButton in betButtons)
        {
            betButton.interactable = newState;
        }
    }

    public void ReevaluateBetButtons()
    {
        int viableBet = playersMoney.Max() + playersBets.Max();

        foreach (Player player in PokerManager.Instance.players)
        {
            if (player.Eliminated || player.IsFolding) continue;
            viableBet = Math.Min(viableBet, playersMoney[player.seat - 1] + playersBets[player.seat - 1] - playersBets[0]);
        }

        betButtons[0].interactable = viableBet >= 1;
        betButtons[1].interactable = viableBet >= 5;
        betButtons[2].interactable = viableBet >= 10;
    }

    public void SubstractMoney(int moneyQty)
    {
        SubstractMoneyToPlayer(moneyQty, 0);
        UserInput.Instance.buttons[1].interactable = playersBets[0] >= higherBetThisRound;
        ReevaluateBetButtons();
    }

    public void SubstractMoneyToPlayer(int moneyQty, int playerNum)
    {
        // -
        playersMoney[playerNum] -= moneyQty;
        playersScoreBoards[playerNum * 2].text = playersMoney[playerNum].ToString();
        // +
        playersBets[playerNum] += moneyQty;
        playersBetsThisBettingRound[playerNum] += moneyQty;
        totalBetted += moneyQty;
        jackpot.text = totalBetted.ToString();

        playersScoreBoards[playerNum * 2 + 1].gameObject.transform.parent.gameObject
            .SetActive(playersBets[playerNum] > 0);
        playersScoreBoards[playerNum * 2 + 1].text = playersBets[playerNum].ToString();

        higherBetThisRound = playersBets.Max();
    }

    public void ResetTotalBetted()
    {
        totalBetted = 0;
        jackpot.text = totalBetted.ToString();

        for (int i = 0; i < playersBets.Length; i++) playersBets[i] = 0;
    }

    private void ResetScoreboards()
    {
        for (int i = 0; i < PokerManager.Instance.players.Count; i++)
        {
            playersScoreBoards[i * 2].text = playersMoney[i].ToString();
            playersScoreBoards[i * 2 + 1].text = 0.ToString();
            playersScoreBoards[i * 2 + 1].transform.parent.gameObject.SetActive(false);
        }
    }

    public void PlayerFolded(int numPlayer)
    {
        SubstractMoney(-playersBetsThisBettingRound[numPlayer]);

        foreach (Button t in betButtons) t.interactable = false;
    }

    public int CalculateCpuPlayerBetDecision(int numPlayer)
    {
        int result;

        List<Player> players = PokerManager.Instance.players;
        HandsCalculator.EHandRanks pRank = players[numPlayer].Hand.CalculateHandRank();

        if (players[numPlayer].Eliminated) return 0;

        switch (pRank)
        {
            case HandsCalculator.EHandRanks.HighCard:
            case HandsCalculator.EHandRanks.OnePair:
                result = Math.Min(higherBetThisRound, Math.Max(5, playersMoney[numPlayer] / 10));
                // Debug.Log($"{numPlayer} has a bad hand. ({result})");
                break;
            case HandsCalculator.EHandRanks.TwoPair:
            case HandsCalculator.EHandRanks.ThreeOfKind:
                result = (higherBetThisRound + Math.Max(10, playersMoney[numPlayer] / 5)) / 2;
                // Debug.Log($"{numPlayer} has a mid hand. ({result})");
                break;
            case HandsCalculator.EHandRanks.Straight:
            case HandsCalculator.EHandRanks.Flush:
            case HandsCalculator.EHandRanks.FullHouse:
            case HandsCalculator.EHandRanks.FourOfKind:
            case HandsCalculator.EHandRanks.StraightFlush:
            case HandsCalculator.EHandRanks.RoyalStraightFlush:
                result = Math.Max(higherBetThisRound, Random.Range(playersMoney[numPlayer] / 2, playersMoney[numPlayer] + 1));
                // Debug.Log($"{numPlayer} has a good hand. ({result})");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        result = result > playersMoney[numPlayer]
            ? playersMoney[numPlayer]
            : result;
        result = result > playersMoneyBeforeRound.Where(money => money > 0).Min() - playersBets[numPlayer]
            ? playersMoneyBeforeRound.Where(money => money > 0).Min() - playersBets[numPlayer]
            : result;
        
        return result;
    }

    public bool ShouldBettingBeDone()
    {
        List<Player> playingPlayers = PokerManager.Instance.players.Where(p => !(p.Eliminated || p.IsFolding)).ToList();
        HashSet<int> differentBets = new HashSet<int>();
        foreach (Player player in playingPlayers)
        {
            differentBets.Add(playersBets[player.seat - 1]);
        }
        
        return differentBets.Count == 1;
    }

    public void FinishRound(int numWinner)
    {
        playersMoney[numWinner] += totalBetted;

        playersBets = new[] {0, 0, 0, 0};
        ResetScoreboards();
        ReevaluateBetButtons();
        for (int i = 0; i < playersMoney.Length; i++)
        {
            if (playersMoney[i] == 0) PokerManager.Instance.players[i].Eliminated = true;
        }
    }
}