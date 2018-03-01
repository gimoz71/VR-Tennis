using System.Collections.Generic;
using UnityEngine;


// SINGLETON DEI PUNTEGGI (centralizzato)
public class AreasManager {

    // genero l'hashtable delle aree corrette
    public Dictionary<string, int> MappAreeCorrette = new Dictionary<string, int>();

    // genero l'hashtable delle aree errate
    public Dictionary<string, int> MappAreeErrate = new Dictionary<string, int>();

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


    // inizializzo i contatori incrementali usati in DetectArea.cs (DA CENTRALIZZARE!!!)
    public int counter;
    public int totalcounter;

    // aggiungo le voci nella hashtable allo start
    public void Start()
    {

        // Aree corrette
        MappAreeCorrette.Add("AreaAnterioreDX", 0);
        MappAreeCorrette.Add("AreaAnterioreSX", 0);
        MappAreeCorrette.Add("AreaPosterioreDX", 0);
        MappAreeCorrette.Add("AreaPosterioreSX", 0);

        // Aree Errate
        MappAreeErrate.Add("RecintoCampo", 0);
        MappAreeErrate.Add("CollisioneRete", 0);
        MappAreeErrate.Add("AreaInterna", 0);
        MappAreeErrate.Add("TribunaSX", 0);
        MappAreeErrate.Add("TribunaDX", 0);
        MappAreeErrate.Add("TribunaAvversario", 0);
        MappAreeErrate.Add("TribunaUtente", 0);
    }

    // Estraggo i valori delle aree corrette
    public bool CheckHitCorrect(string obj)
    {
        List<string> Keys = new List<string>(MappAreeCorrette.Keys);
        return Keys.Contains(obj);
    }

    // Estraggo i valori delle aree errate
    public bool CheckHitError(string obj)
    {
        List<string> Keys = new List<string>(MappAreeErrate.Keys);
        return Keys.Contains(obj);
    }

    // Resetto i valori delle aree corrette
    public void ResetCorrectTrigger()
    {
        List<string> keys = new List<string>(MappAreeCorrette.Keys);
        foreach (string key in keys)
        {
            MappAreeCorrette[key] = 0;
        }
    }

    // (DEBUG) estraggo e visualizzo la lista dei valori
    public void stampaMappa()
    {
        List<string> keys = new List<string>(MappAreeCorrette.Keys);
        foreach (string key in keys)
        {
            Debug.Log("Chiave: " + key + " | Valore: " + MappAreeCorrette[key]);
        }
        Debug.Log("------------------------------------------");
    }
}
