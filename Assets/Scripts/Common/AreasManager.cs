using System.Collections.Generic;
using UnityEngine;


// SINGLETON DEI PUNTEGGI (centralizzato)
public class AreasManager {

    // genero l'hashtable delle aree corrette
    public List<string> listaAreeCorrette = new List<string>();

    // genero l'hashtable delle aree errate
    public List<string> listaAreeErrate = new List<string>();

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
        listaAreeCorrette.Add("AreaAnterioreDX");
        listaAreeCorrette.Add("AreaPosterioreDX");
        listaAreeCorrette.Add("AreaAnterioreSX");
        listaAreeCorrette.Add("AreaPosterioreSX");

        // Aree Errate
        listaAreeErrate.Add("RecintoCampo");
        listaAreeErrate.Add("CollisioneRete");
        listaAreeErrate.Add("AreaInterna");
        listaAreeErrate.Add("TribunaSX");
        listaAreeErrate.Add("TribunaDX");
        listaAreeErrate.Add("TribunaAvversario");
        listaAreeErrate.Add("TribunaUtente");
        listaAreeErrate.Add("AreaInterna");
        listaAreeErrate.Add("FuoriCampoSX");
        listaAreeErrate.Add("FuoriCampoDX");
        listaAreeErrate.Add("FuoriCampoAvversario");
        listaAreeErrate.Add("FuoriCampoPlayer");
    }

    // Estraggo i valori delle aree corrette
    public bool CheckHitCorrect(string obj)
    {
        return listaAreeCorrette.Contains(obj);
    }

    // Estraggo i valori delle aree errate
    public bool CheckHitError(string obj)
    {
        return listaAreeErrate.Contains(obj);
    }
}
