using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> completedMiniGames;
    
    public GameData()
    {
        playerPosition = Vector3.zero;

        completedMiniGames = new SerializableDictionary<string, bool>();
    }

    public void MarkMiniGameCompleted(string miniGameName)
    {
        if (!completedMiniGames.ContainsKey(miniGameName))
        {
            completedMiniGames.Add(miniGameName, true);
        }
    }

    public bool IsMiniGameCompleted(string miniGameName)
    {
        return completedMiniGames.ContainsKey(miniGameName) && completedMiniGames[miniGameName];
    }
}
