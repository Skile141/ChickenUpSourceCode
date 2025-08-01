using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardTable : MonoBehaviour
{
    public TextMeshProUGUI[] playerList = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] playerScoreList = new TextMeshProUGUI[3];

    private string noText = "N/A";
    private string generalFormat = "{0}";
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateLeaderboardDisplay();
    }

    private void UpdateLeaderboardDisplay()
    {
        if (DataManager.instance == null)
        {
            Debug.Log("Data not found");
            return;
        }

        List<PlayerData> topPlayers = DataManager.instance.GetTopPlayers();

        for (int i = 0; i < 3; i++)
        {
            if (i < topPlayers.Count)
            {
          
                if (playerList[i] != null)
                {
                    string nameText = string.Format(generalFormat, topPlayers[i].playerName);
                    playerList[i].text = nameText;
                }

                if (playerScoreList[i] != null)
                {
                    string scoreText = string.Format(generalFormat, topPlayers[i].playerScore);
                    playerScoreList[i].text = scoreText;
                }
            }
            else 
            { 
                if (playerList[i] != null)
                {
                    string nameText = string.Format(generalFormat, noText);
                    playerList[i].text = nameText;
                }

                if (playerScoreList[i] != null)
                {
                    string scoreText = string.Format(generalFormat, noText);
                    playerScoreList[i].text = scoreText;
                }
            }
        }
    }

    public void RefreshLeaderboard()
    {
        UpdateLeaderboardDisplay();
    }
}
