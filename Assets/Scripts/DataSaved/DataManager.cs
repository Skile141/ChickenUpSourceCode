using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private PlayerDataList playerDataList = new PlayerDataList();
    public static DataManager instance { get; private set; }
    private string savePath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("More than 1 data manager in the scene");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        savePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        LoadPlayerRecords();
        Debug.Log("DataManager initialized successfully.");
    }

    public void AddToTheLeaderboard(string name, int score)
    {
        playerDataList.playerData.Add(new PlayerData(name, score));
        playerDataList.playerData.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));
        if (playerDataList.playerData.Count > 3)
        {
            playerDataList.playerData.RemoveAt(3);
        }

        SavePlayerRecords();
        UpdateLeaderboardUI();
    }

    public void SavePlayerRecords()
    {
        string json = JsonUtility.ToJson(playerDataList);
        File.WriteAllText(savePath, json);
    }

    public void LoadPlayerRecords()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerDataList = JsonUtility.FromJson<PlayerDataList>(json);
        }
    }

    public List<PlayerData> GetTopPlayers()
    {
        return playerDataList.playerData;
    }

    public void UpdateLeaderboardUI()
    {
        LeaderboardTable leaderboardTable = FindAnyObjectByType<LeaderboardTable>();
        if (leaderboardTable != null)
        {
            leaderboardTable.RefreshLeaderboard();
        }
    }
}