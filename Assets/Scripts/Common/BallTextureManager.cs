using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// SINGLETON delle texture (centralizzato)
public class BallTextureManager {

    public static int TEXTURE_A = 1;
    public static int TEXTURE_B = 2;
    public static int TEXTURE_C = 3;
    public static int TEXTURE_D = 4;
    public static int TEXTURE_E = 5;

    public static int MAP_INDEX_COLORI = 0;
    public static int MAP_INDEX_SIMBOLI = 1;

    private int maxTextureIndex = 3;

    // genero l'hashtable dei simboli delle palle
    public Dictionary<int, Texture> MappSimboloPalla = new Dictionary<int, Texture>();
    public Dictionary<int, Texture> MappColorePalla = new Dictionary<int, Texture>();


    public Dictionary<int, Texture>[] mappe;
    private int mapIndex = 0;

    private static BallTextureManager instance;

    private BallTextureManager() { }

    public static BallTextureManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BallTextureManager();
                instance.Start();
            }
            
            return instance;
        }
    }

    public int getMapIndex ()
    {
        return mapIndex;
    }

    public void setMapIndex(int index)
    {
        mapIndex = index;
    }

    // aggiungo le voci nella hashtable allo start
    public void Start()
    {
        mappe = new Dictionary<int, Texture>[2];
        mappe[0] = MappColorePalla;
        mappe[1] = MappSimboloPalla;
    }

    public Texture RandomTexture()
    {
        System.Random rand = new System.Random();
        int next = rand.Next(1, maxTextureIndex);
        return mappe[mapIndex][next];
    }
    
    public void setMaxTextureIndex(int index)
    {
        maxTextureIndex = index;
    }

    public int getMaxTextureIndex()
    {
        return maxTextureIndex;
    }

    public void setShutdownTextureIndex()
    {
        maxTextureIndex = 2;
    }
    public void setDefaultTextureIndex()
    {
        maxTextureIndex = 3;
    }



    // (DEBUG) estraggo e visualizzo la lista dei valori
    public void stampaMappa()
    {
        List<int> keys = new List<int>(MappSimboloPalla.Keys);
        foreach (int key in keys)
        {
            Debug.Log("Chiave: " + key + " | Valore: " + maxTextureIndex);
        }
        Debug.Log("------------------------------------------");
    }
}
