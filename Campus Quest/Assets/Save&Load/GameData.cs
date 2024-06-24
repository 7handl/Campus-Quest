using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> miniGameCompleted;


    public GameData()
    {
        playerPosition = new Vector3(0, 0, 0);

        miniGameCompleted = new SerializableDictionary<string, bool>();
    }

}

