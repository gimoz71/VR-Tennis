using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DetectArea : MonoBehaviour {

    private AreasManager areasManager;

    public Text myText;
    public Text myCounter;
    public Text myTotalCounter;


    //private int counter;
    //private int totalcounter;

    // Use this for initialization
    void Start () {

        myText = GameObject.Find("Score").GetComponent<Text>();
        myCounter = GameObject.Find("Counter").GetComponent<Text>();
        myTotalCounter = GameObject.Find("TotalCounter").GetComponent<Text>();

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

            if (areasManager.MappAree[other.gameObject.name] == 1)
            {
                Debug.Log("ERRORE: doppio colpo su " + other.gameObject.name);
                myText.text = "ERRORE " + other.gameObject.name;
            }
            else
            {
                AreasManager.Instance.counter += 1;
                myCounter.text = "Corretti: " + AreasManager.Instance.counter;
                myText.text = other.gameObject.name;
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
            myText.text = "ERRORE: colpito " + other.gameObject.name;
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;
        }
    }
}
