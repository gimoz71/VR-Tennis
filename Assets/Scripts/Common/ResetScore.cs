using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetScore : MonoBehaviour {

    [Header("Conteggi")]
    private Text corretti;
    private Text totali;

    // Aggiorna il punteggio nel cruscotto dell'istruttore
    [Header("Conteggi (UI)")]
    private Text correttiPanel;
    private Text totaliPanel;

    // Istanza per il reset del punteggio
    private AreasManager areasManager;

    void Start()
    {
        
        
        areasManager = AreasManager.Instance;

        // Assegno in Runtime i gameobject relativi
        if (GameObject.Find("[DEBUGGER TEXT]") != null)
        {
            corretti = GameObject.Find("CorrettiTabellone").GetComponent<Text>();
            totali = GameObject.Find("TotaliTabellone").GetComponent<Text>();
        }

        correttiPanel = GameObject.Find("Corretti").GetComponent<Text>();
        totaliPanel = GameObject.Find("Totali").GetComponent<Text>();
    }

    public void Reset()
    {

        AreasManager.Instance.counter = 0;
        AreasManager.Instance.totalcounter = 0;

        if (GameObject.Find("[DEBUGGER TEXT]") != null)
        {
            corretti.text = "Corretti: " + AreasManager.Instance.counter;
            totali.text = "Totali: " + AreasManager.Instance.totalcounter;
        }

        correttiPanel.text = "Corretti: " + AreasManager.Instance.counter;
        totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
    }

}
