using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlayerData 
{
    public string playerName;
    public int playerScore;

    // Parameterized constructor
    public PlayerData(string name, int score)
    {
        this.playerName = name;
        this.playerScore = score;
    }
}

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> playerData = new List<PlayerData>();
}
