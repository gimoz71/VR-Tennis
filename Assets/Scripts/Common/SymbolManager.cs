using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// SINGLETON DElle textture (centralizzato)
public class SymbolManager {

    public static int SIMBOLO_PALLA_A = 1;
    public static int SIMBOLO_PALLA_B = 2;
    public static int SIMBOLO_PALLA_C = 3;
    public static int SIMBOLO_PALLA_D = 4;
    public static int SIMBOLO_PALLA_E = 5;

    private int maxSymbolIndex = 3;

    // genero l'hashtable dei simboli delle palle
    public Dictionary<int, Texture> MappSimboloPalla = new Dictionary<int, Texture>();

    private static SymbolManager instance;

    private SymbolManager() { }

    public static SymbolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SymbolManager();
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

    public Texture RandomSymbol()
    {
        System.Random rand = new System.Random();
        int next = rand.Next(1, maxSymbolIndex);
        return MappSimboloPalla[next];
    }
    
    public void setMaxSymbolIndex(int index)
    {
        maxSymbolIndex = index;
    }

    public int getMaxSymbolIndex()
    {
        return maxSymbolIndex;
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
