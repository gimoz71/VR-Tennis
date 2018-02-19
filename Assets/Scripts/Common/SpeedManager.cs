﻿using System.Collections.Generic;
using UnityEngine;


// SINGLETON DEI PUNTEGGI (centralizzato)
public class SpeedManager {

   

    // genero l'hashtable delle aree errate
    public Dictionary<string, int> MappVelocita = new Dictionary<string, int>();

    private static SpeedManager instance;

    private SpeedManager() { }

    public static SpeedManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SpeedManager();
                instance.Start();
            }
            
            return instance;
        }
    }

    public int counter;
    public int totalcounter;

    // aggiungo le voci nella hashtable allo start
    public void Start()
    {


        // Aree Errate
        MappVelocita.Add("Lento", 0);
        MappVelocita.Add("Medio", 0);
        MappVelocita.Add("Veloce", 0);
    }

    
    // Resetto i valori delle aree corrette
    public void ResetCorrectTrigger()
    {
        List<string> keys = new List<string>(MappVelocita.Keys);
        foreach (string key in keys)
        {
            MappVelocita[key] = 0;
        }
    }

    // (DEBUG) estraggo e visualizzo la lista dei valori
    public void stampaMappa()
    {
        List<string> keys = new List<string>(MappVelocita.Keys);
        foreach (string key in keys)
        {
            Debug.Log("Chiave: " + key + " | Valore: " + MappVelocita[key]);
        }
        Debug.Log("------------------------------------------");
    }
}