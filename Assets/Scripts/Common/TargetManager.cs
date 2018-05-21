using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// SINGLETON delle texture (centralizzato)
public class TargetManager {

  

    public Dictionary<string, bool> associazioneTargetArea = new Dictionary<string, bool>();

   
    private int mapIndex = 0;

    private static TargetManager instance;

    private TargetManager() { }

    public static TargetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TargetManager();
                instance.Start();
            }
            
            return instance;
        }
    }

    public void setAssociazioneTargetArea(Dictionary<string, bool> dictionary)
    {
        associazioneTargetArea = dictionary;
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
    }

}
