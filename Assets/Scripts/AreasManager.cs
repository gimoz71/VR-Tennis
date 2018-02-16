using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

public class AreasManager
{

    public Dictionary<string, int> MappAree = new Dictionary<string, int>();

    private static AreasManager instance;

    private AreasManager() { }

    public static AreasManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AreasManager();
                instance.Start();
            }
            
            return instance;
        }
    }

    public int counter;
    public int totalcounter;

    // Use this for initialization
    public void Start()
    {

        MappAree.Add("AreaAnterioreDX", 0);
        MappAree.Add("AreaAnterioreSX", 0);
        MappAree.Add("AreaPosterioreDX", 0);
        MappAree.Add("AreaPosterioreSX", 0);
        //MappAree.Add("CollisioneRete", 0);
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
