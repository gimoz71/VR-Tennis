using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// SINGLETON DElle textture (centralizzato)
public class ColorManager {

    public static int COLORE_PALLA_BASE = 1;
    public static int COLORE_PALLA_BLUE = 2;
    public static int COLORE_PALLA_FUCSIA = 3;
    public static int COLORE_PALLA_ORANGE = 4;
    public static int COLORE_PALLA_RED = 5;

    private int maxColorIndex = 3;

    // genero l'hashtable delle aree errate
    public Dictionary<int, Texture> MappColorePalla = new Dictionary<int, Texture>();

    private static ColorManager instance;

    private ColorManager() { }

    public static ColorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ColorManager();
                instance.Start();
            }
            
            return instance;
        }
    }

    //public int counter;
    //public int totalcounter;

    // aggiungo le voci nella hashtable allo start
    public void Start()
    {

    }

    public Texture RandomColor()
    {
        System.Random rand = new System.Random();
        int next = rand.Next(1, maxColorIndex);
        return MappColorePalla[next];
    }
    
    public void setMaxColorIndex(int index)
    {
        maxColorIndex = index;
    }

    public int getMaxColorIndex()
    {
        return maxColorIndex;
    }

    //// Resetto i valori delle aree corrette
    //public void ResetCorrectTrigger()
    //{
    //    List<string> keys = new List<string>(MappColore.Keys);
    //    foreach (string key in keys)
    //    {
    //        MappColore[key] = 0;
    //    }
    //}

    // (DEBUG) estraggo e visualizzo la lista dei valori
    //public void stampaMappa()
    //{
    //    List<string> keys = new List<string>(MappColore.Keys);
    //    foreach (string key in keys)
    //    {
    //        Debug.Log("Chiave: " + key + " | Valore: " + MappColore[key]);
    //    }
    //    Debug.Log("------------------------------------------");
    //}
}
