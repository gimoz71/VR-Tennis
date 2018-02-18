using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DetectArea : MonoBehaviour {

    private AreasManager areasManager;

	// Aggiorna il punteggio e collisioni d'errore nel tabellone in campo
    public Text myError;
    public Text myCounter;
    public Text myTotalCounter;

	// Aggiorna il punteggio Ne Menu Istruttore
	public Text myCounterPanel;
	public Text myTotalCounterPanel;


    //private int counter;
    //private int totalcounter;

    // Use this for initialization
    void Start () {


        myError = GameObject.Find("Errore").GetComponent<Text>();
		myCounter = GameObject.Find("CorrettiTabellone").GetComponent<Text>();
		myTotalCounter = GameObject.Find("TotaliTabellone").GetComponent<Text>();


		myCounterPanel = GameObject.Find("Corretti").GetComponent<Text>();
		myTotalCounterPanel = GameObject.Find("Totali").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
       
        areasManager = AreasManager.Instance;

        if (areasManager.CheckHit(other.gameObject.name))
        {
            AreasManager.Instance.totalcounter += 1;

            myTotalCounter.text = "Totali: " + AreasManager.Instance.totalcounter;
			myTotalCounterPanel.text = "Totali: " + AreasManager.Instance.totalcounter;

            if (areasManager.MappAree[other.gameObject.name] == 1)
            {
                Debug.Log("ERRORE: doppio colpo su " + other.gameObject.name);
                myError.text = "ERRORE " + other.gameObject.name;
            }
            else
            {
                AreasManager.Instance.counter += 1;
				myCounter.text = "Corretti: " + AreasManager.Instance.counter;
				myCounterPanel.text = "Corretti: " + AreasManager.Instance.counter;
                myError.text = other.gameObject.name;
            }

            Debug.Log("------------------------------------------");
            Debug.Log("Quante Mappe? " + areasManager.MappAree.Count);
            Debug.Log("------------------------------------------");
            // Stampo la mappa per debug
            areasManager.stampaMappa();

            // pulisco la hashMap (reinizializzo)
            areasManager.ResetTrigger();

            // assegno valore 1 a quello colpito
            areasManager.MappAree[other.gameObject.name] = 1;


            //Debug.Log(other.gameObject.name);
            areasManager.stampaMappa();

            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;

        } else if (other.gameObject.name == "recinto_campo" || other.gameObject.name == "CollisioneRete")
        {
            AreasManager.Instance.totalcounter += 1;
			myTotalCounter.text = "Totali: " + AreasManager.Instance.totalcounter;
			myTotalCounterPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
            myError.text = "ERRORE: colpito " + other.gameObject.name;
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;
        }
    }
}
