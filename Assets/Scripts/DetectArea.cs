using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DetectArea : MonoBehaviour {

    private AreasManager areasManager;
    private PlayerState playerState;

    // Aggiorna il punteggio e collisioni d'errore nel tabellone in campo (DEBUG, da tenere?)
    [Header("Informazioni di errore")]
    public Text errori;

    [Header("Conteggi")]
    public Text corretti;
    public Text totali;

    // Aggiorna il punteggio nel cruscotto dell'istruttore
    [Header("Conteggi (UI)")]
    public Text correttiPanel;
    public Text totaliPanel;

    public AudioSource ErrorAreaClip;
    //public AudioSource CorrectAreaClip


    // Use this for initialization
    void Start () {

        // Assegno in Runtime i gameobject relativi
        errori = GameObject.Find("Errore").GetComponent<Text>();
		corretti = GameObject.Find("CorrettiTabellone").GetComponent<Text>();
		totali = GameObject.Find("TotaliTabellone").GetComponent<Text>();

		correttiPanel = GameObject.Find("Corretti").GetComponent<Text>();
		totaliPanel = GameObject.Find("Totali").GetComponent<Text>();

        AudioSource ErrorAreaClip = GetComponent<AudioSource>();

        playerState = PlayerState.Instance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
       
        areasManager = AreasManager.Instance;

        if (areasManager.CheckHitCorrect(other.gameObject.name))
        {

            // Aggiorno il conteggio dei colpi totali
            AreasManager.Instance.totalcounter += 1;
            totali.text = "Totali: " + AreasManager.Instance.totalcounter;
			totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
            playerState.totalcounter = AreasManager.Instance.totalcounter;

            if (areasManager.MappAreeCorrette[other.gameObject.name] == 1) // se colpisco due volte di seguito lo stesso settore riporto l'errore
            {
                Debug.Log("ERRORE: Colpito due volte " + other.gameObject.name);
                errori.text = "ERRORE Colpito due volte " + other.gameObject.name;

                ErrorAreaClip.Play();
            }
            else
            {
                // Aggiorno il conteggio dei colpi corretti
                AreasManager.Instance.counter += 1;
				corretti.text = "Corretti: " + AreasManager.Instance.counter;
				correttiPanel.text = "Corretti: " + AreasManager.Instance.counter;
                errori.text = other.gameObject.name;

                playerState.counter = AreasManager.Instance.counter;
            }

            

            // conteggio di debug delle mappe

            //Debug.Log("------------------------------------------");
            //Debug.Log("Quante Mappe? " + areasManager.MappAreeCorrette.Count);
            //Debug.Log("------------------------------------------");

            // (DEBUG) Stampo lo stato delle aree prima dell'aggiornamento del colpo
            //areasManager.stampaMappa();

            // pulisco la hashMap (reinizializzo)
            areasManager.ResetCorrectTrigger();

            // assegno valore 1 a quello colpito
            areasManager.MappAreeCorrette[other.gameObject.name] = 1;

            // (DEBUG) Stampo lo stato delle aree dopo dell'aggiornamento del colpo
            //areasManager.stampaMappa();

            // Disabilito il collisore dell'instanza della palla dopo la prima collisione
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;

            Debug.Log("********************" + playerState.counter);
            Debug.Log("********************" + playerState.totalcounter);
            Debug.Log("********************" + JsonUtility.ToJson(playerState));

        } else if (areasManager.CheckHitError(other.gameObject.name)) // se colpisco aree differenti da quelle della hashtable corretta
            {
            // aggiorno conteggi totali
            AreasManager.Instance.totalcounter += 1;
			totali.text = "Totali: " + AreasManager.Instance.totalcounter;
			totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
            playerState.totalcounter = AreasManager.Instance.totalcounter;

            // Riporto l'errore
            errori.text = "ERRORE: colpito " + other.gameObject.name;

            // pulisco la hashMap (reinizializzo)
            areasManager.ResetCorrectTrigger();

            ErrorAreaClip.Play();

            // Disabilito il collisore dell'instanza della palla dopo la prima collisione
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;

            Debug.Log("********************" + playerState.counter);
            Debug.Log("********************" + playerState.totalcounter);
            Debug.Log("********************" + JsonUtility.ToJson(playerState));
        }
    }
}
