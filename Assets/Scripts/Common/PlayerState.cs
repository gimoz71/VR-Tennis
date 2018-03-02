using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState {

    private static PlayerState instance;

    public int totalcounter;
    public int counter;

    private PlayerState() { }

    public static PlayerState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerState();
            }

            return instance;
        }
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}
