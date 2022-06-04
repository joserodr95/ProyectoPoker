using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BettingManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] playersScoreBoards;
    [SerializeField]
    private TextMeshProUGUI jackpot;
    [SerializeField]
    private Button[] betButtons;
    
    private int[] playersMoney = {50, 50, 50, 50};
    private int[] playersBets = {0, 0, 0, 0};
    private int totalBetted = 0;

    public void SubstractMoney(int moneyQty)
    {
        // -
        playersMoney[0] -= moneyQty;
        playersScoreBoards[0].text = playersMoney[0].ToString();
        // +
        playersBets[0] += moneyQty;
        totalBetted += moneyQty;
        jackpot.text = playersBets.Sum().ToString();

        ToggleBetButtons();
    }

    private void ToggleBetButtons()
    {
        betButtons[0].interactable = playersMoney[0] >= 1;
        betButtons[1].interactable = playersMoney[0] >= 5;
        betButtons[2].interactable = playersMoney[0] >= 10;
    }
}