using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// SINGLETON DElle textture (centralizzato)
public class SymbolManager {

    public static int SIMBOLO_PALLA_A = 0;
    public static int SIMBOLO_PALLA_B = 1;
    public static int SIMBOLO_PALLA_C = 2;
    public static int SIMBOLO_PALLA_D = 3;
    public static int SIMBOLO_PALLA_E = 4;

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
            }
            
            return instance;
        }
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

    public void setShutdownSymbolIndex()
    {
        maxSymbolIndex = 2;
    }

    public void setDefaultSymbolIndex()
    {
        maxSymbolIndex = 3;
    }
}
