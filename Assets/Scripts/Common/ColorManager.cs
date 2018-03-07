using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// SINGLETON DElle textture (centralizzato)
public class ColorManager {

    public static int COLORE_PALLA_BASE = 0;
    public static int COLORE_PALLA_RED = 1;
    public static int COLORE_PALLA_FUCSIA = 2;
    public static int COLORE_PALLA_ORANGE = 3;
    public static int COLORE_PALLA_BLUE = 4;

    private int maxColorIndex = 3;

    // genero l'hashtable dei colori delle palle
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
            }
            
            return instance;
        }
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

    public void setShutdownColorIndex()
    {
        maxColorIndex = 2;
    }

    public void setDefaultColorIndex()
    {
        maxColorIndex = 3;
    }
}
