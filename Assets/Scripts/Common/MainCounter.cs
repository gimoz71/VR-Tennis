using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCounter {

    public Dictionary<string, int> MappAree = new Dictionary<string, int>();

    private static MainCounter instance;

    private MainCounter() { }

    public static MainCounter Instance {
        get {
            if (instance == null) {
                instance = new MainCounter();
            }
            instance.Start();
            return instance;
        }
    }

    // Use this for initialization
    public void Start () {

        MappAree.Add("AreaAnterioreDX", 0);
        MappAree.Add("AreaAnterioreSX", 0);
        MappAree.Add("AreaPosterioreDX", 0);
        MappAree.Add("AreaPosterioreSX", 0);
        MappAree.Add("CollisioneRete", 0);
    }
	
	// Update is called once per frame
	public void Update () {
		
	}

    public bool CheckHit(string obj)
    {
        List<string> Keys = new List<string>(MappAree.Keys);
        return Keys.Contains(obj);
    }

    public void ResetTrigger()
    {
        List<string> keys = new List<string>(MappAree.Keys);
        foreach (string key in keys)
        {
            MappAree[key] = 0;
        }
    }

    public void stampaMappa()
    {
        List<string> keys = new List<string>(MappAree.Keys);
        foreach (string key in keys)
        {

            Debug.Log("Chiave: " + key + " | Valore: " + MappAree[key]);
        }
        Debug.Log("------------------------------------------");
    }
}
