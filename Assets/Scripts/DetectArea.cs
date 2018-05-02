using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;


public class DetectArea : MonoBehaviour
{

    private AreasManager areasManager;
    private DiffManager diffManager;
    private PlayerState playerState;
    private BallTextureManager ballTextureManager;
    private TargetManager targetManager;

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

    [Header("Lista Toggle Group nei Pannelli  Opzioni")]

    private CanvasGroup targetCanvasGroup;
    private CanvasGroup multiColoreCanvasGroup;
    private CanvasGroup multiSimboloCanvasGroup;
    private CanvasGroup differenziazioneCanvasGroup;
    private CanvasGroup decisionMakingCanvasGroup;

    private Dropdown MCAreaPosterioreDX;
    private Dropdown MCAreaAnterioreDX;
    private Dropdown MCAreaPosterioreSX;
    private Dropdown MCAreaAnterioreSX;

    private Text protocolloAttivo;

    public AudioSource ErrorAreaClip;
    //public AudioSource CorrectAreaClip

    //private SpeedManager speedManager;

    private GameObject physParent;
    public AudioSource RacketHit;
    private BatCapsuleFollower bcf;

    private GameObject racketCenter;

    private Text velocita;
    private Text distanceText;

    private string ballSpeed = "";

    private float speed;
    private Hand hand;


    // Use this for initialization
    void Start()
    {
        ballTextureManager = BallTextureManager.Instance;
        targetManager = TargetManager.Instance;

        // Assegno in Runtime i gameobject relativi
        errori = GameObject.Find("Errore").GetComponent<Text>();
        corretti = GameObject.Find("CorrettiTabellone").GetComponent<Text>();
        totali = GameObject.Find("TotaliTabellone").GetComponent<Text>();

        correttiPanel = GameObject.Find("Corretti").GetComponent<Text>();
        totaliPanel = GameObject.Find("Totali").GetComponent<Text>();

        protocolloAttivo = GameObject.Find("Protocollo Attivo").GetComponent<Text>();

        targetCanvasGroup = GameObject.Find("PanelTG").GetComponent<CanvasGroup>();

        if (protocolloAttivo.text.Equals("Protocollo Base"))
        {
            //multiColoreCanvasGroup = GameObject.Find("PanelMC").GetComponent<CanvasGroup>();
            //multiSimboloCanvasGroup = GameObject.Find("PanelMS").GetComponent<CanvasGroup>();
            //differenziazioneCanvasGroup = GameObject.Find("PanelDIFF").GetComponent<CanvasGroup>();
            //decisionMakingCanvasGroup = GameObject.Find("PanelDM").GetComponent<CanvasGroup>();

        } else if (protocolloAttivo.text.Equals("Protocollo Cognitivo"))
        {
            multiColoreCanvasGroup = GameObject.Find("PanelMC").GetComponent<CanvasGroup>();
            multiSimboloCanvasGroup = GameObject.Find("PanelMS").GetComponent<CanvasGroup>();
            differenziazioneCanvasGroup = GameObject.Find("PanelDIFF").GetComponent<CanvasGroup>();
            decisionMakingCanvasGroup = GameObject.Find("PanelDM").GetComponent<CanvasGroup>();

        } else if (protocolloAttivo.text.Equals("Vision Training "))
        {
            multiColoreCanvasGroup = GameObject.Find("PanelMC").GetComponent<CanvasGroup>();
            //multiSimboloCanvasGroup = GameObject.Find("PanelMS").GetComponent<CanvasGroup>();
            differenziazioneCanvasGroup = GameObject.Find("PanelDIFF").GetComponent<CanvasGroup>();
            decisionMakingCanvasGroup = GameObject.Find("PanelDM").GetComponent<CanvasGroup>();

        } else if (protocolloAttivo.text.Equals("Risposta al Servizio"))
        {
            multiColoreCanvasGroup = GameObject.Find("PanelMC").GetComponent<CanvasGroup>();
            //multiSimboloCanvasGroup = GameObject.Find("PanelMS").GetComponent<CanvasGroup>();
           // differenziazioneCanvasGroup = GameObject.Find("PanelDIFF").GetComponent<CanvasGroup>();
            decisionMakingCanvasGroup = GameObject.Find("PanelDM").GetComponent<CanvasGroup>();

        }


        AudioSource ErrorAreaClip = GetComponent<AudioSource>();

        playerState = PlayerState.Instance;
        areasManager = AreasManager.Instance;
        diffManager = DiffManager.Instance;

        AudioSource source = GetComponent<AudioSource>();
        if (GameObject.Find("racket") != null)
        {
            physParent = GameObject.Find("racket");
            bcf = GameObject.Find("Racket Follower(Clone)").GetComponent<BatCapsuleFollower>();
            velocita = GameObject.Find("Velocita").GetComponent<Text>();
            racketCenter = GameObject.Find("racketCenter");
            distanceText = GameObject.Find("distanceText").GetComponent<Text>();
            hand = physParent.GetComponentInParent<Hand>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "racket")
        {
            // calcolo la distanza della palla dal centro della racchetta all'impatto
            float dist = Vector3.Distance(transform.position, racketCenter.transform.position);
            //Debug.Log(string.Format("La distanza tra {0} and {1} è: {2}", transform.position, racketCenter.transform.position, dist));
            distanceText.text = dist.ToString();

            speed = bcf._speed;

            string key = BatCapsuleFollower.GetSpeedKey(speed);
            ballSpeed = key;

            //speedManager = SpeedManager.Instance;
            //speedManager.stampaMappa();

            /*velocita.text = "velocità momentanea: " + speed;
            if (speedManager.MappVelocita[key] == 1)
            {
                //Debug.Log("ERRORE STESSA VELOCITA' PRECEDENTE");
                velocita.text += " ERRORE STESSA VELOCITA': " + key;
            }
            else
            {
                velocita.text += " VELOCITA': " + key;
            }

            speedManager.ResetVelocityTrigger();
            speedManager.MappVelocita[key] = 1;*/

            //speedManager.stampaMappa();
            Pulse();
        } else if (areasManager.CheckHitCorrect(other.gameObject.name)) // se colpisco aree della hashtable corretta
        {
            if (ballSpeed.Equals(""))
            {
                //lancio errore perchè manca velocità della pallina
                Debug.LogError("Parametro Speed vuoto");
                ballSpeed = "Lento";
            }

            //TARGET
            if ( targetCanvasGroup.interactable == true)
            {
                AreasManager.Instance.totalcounter += 1;
                totali.text = "Totali: " + AreasManager.Instance.totalcounter;
                totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
                playerState.totalcounter = AreasManager.Instance.totalcounter;

                string zone = other.gameObject.name;
                if (targetManager.associazioneTargetArea[zone])
                {
                    AreasManager.Instance.counter += 1;
                    correttiPanel.text = "Corretti: " + AreasManager.Instance.counter;
                    Debug.Log("OK AREA!!");
                    errori.text = "OK AREA";
                }
                else
                {
                    Debug.Log("AREA SBAGLIATA!!");
                    ErrorAreaClip.Play();
                    errori.text = "Area Sbagliata";
                }
            }

            //MULTICOLORE
            if (protocolloAttivo.text != "Protocollo Base" && multiColoreCanvasGroup.interactable == true) 
            {
                AreasManager.Instance.totalcounter += 1;
                totali.text = "Totali: " + AreasManager.Instance.totalcounter;
                totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
                playerState.totalcounter = AreasManager.Instance.totalcounter;

                int ballTexture = ballTextureManager.current;
                string zone = other.gameObject.name;
                Debug.Log("texture: " + ballTexture  + " area: " + ballTextureManager.associazioneTextureArea[zone] + " zone: " + zone);
                if (ballTextureManager.associazioneTextureArea[zone] == ballTexture)
                {
                    AreasManager.Instance.counter += 1;
                    correttiPanel.text = "Corretti: " + AreasManager.Instance.counter;
                    Debug.Log("OK COLORE!!");
                    errori.text = "OK COLORE";
                } else
                {
                    Debug.Log("AREA SBAGLIATA!!");
                    ErrorAreaClip.Play();
                    errori.text = "Area Sbagliata";
                }
            }

            //MULTISIMBOLO
            if (protocolloAttivo.text != "Protocollo Base" && multiSimboloCanvasGroup.interactable == true)
            {
                AreasManager.Instance.totalcounter += 1;
                totali.text = "Totali: " + AreasManager.Instance.totalcounter;
                totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
                playerState.totalcounter = AreasManager.Instance.totalcounter;

                int ballTexture = ballTextureManager.current;
                string zone = other.gameObject.name;

                if (ballTextureManager.associazioneTextureArea[zone] == ballTexture)
                {
                    AreasManager.Instance.counter += 1;
                    correttiPanel.text = "Corretti: " + AreasManager.Instance.counter;
                    Debug.Log("OK SIMBOLO!!");
                }
                else
                {
                    ErrorAreaClip.Play();
                    errori.text = "Area Sbagliata";
                }
            }

            //DIFFERENZIAZIONE
            else if (protocolloAttivo.text == "Vision Training" || protocolloAttivo.text == "Protocollo Cognitivo" && differenziazioneCanvasGroup.interactable == true) 
            {
                // Aggiorno il conteggio dei colpi totali
                AreasManager.Instance.totalcounter += 1;
                totali.text = "Totali: " + AreasManager.Instance.totalcounter;
                totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
                playerState.totalcounter = AreasManager.Instance.totalcounter;

                string zone = other.gameObject.name;
                if (ballSpeed.Equals(""))
                {
                    //lancio errore perchè manca velocità della pallina
                    Debug.LogError("Parametro Speed vuoto");
                    ballSpeed = "Lento";
                }
                else {
                    // Gestione aree corrette DIFFERENZIAZIONE
                    if (!diffManager.checkCombination(ballSpeed, zone)) // se colpisco due volte di seguito lo stesso settore riporto l'errore
                    {
                        //ZONA COLPITA E/O VELOCITA' PALLINA NON SONO OK
                        Debug.Log("ERRORE VELOCITA': " + ballSpeed);
                        Debug.Log("Colpito due volte: " + other.gameObject.name + " " + ballSpeed + " con livello " + diffManager.getLivello());
                        errori.text = "Colpito due volte: " + other.gameObject.name + " " + ballSpeed + " con livello " + diffManager.getLivello();

                        ErrorAreaClip.Play();
                    }
                    else //corretto
                    {
                        //LA ZONA COLPITA E LA VELOCITA' DELLA PALLINA SONO OK

                        // Aggiorno il conteggio dei colpi corretti
                        AreasManager.Instance.counter += 1;
                        corretti.text = "Corretti: " + AreasManager.Instance.counter;
                        correttiPanel.text = "Corretti: " + AreasManager.Instance.counter;
                        Debug.Log("OK VELOCITA': " + ballSpeed);
                        Debug.Log("Colpo OK: " + other.gameObject.name + " " + ballSpeed + " con livello " + diffManager.getLivello());
                        errori.text = "Colpo OK: " + other.gameObject.name + " " + ballSpeed + " con livello " + diffManager.getLivello();

                        playerState.counter = AreasManager.Instance.counter;
                    }

                    ballSpeed = "Lento";

                }


            }

            //DECISION MAKING
            else if (protocolloAttivo.text == "Risposta al Servizio" || protocolloAttivo.text == "Protocollo Cognitivo" && decisionMakingCanvasGroup.interactable == true)
            {
                if (ballSpeed.Equals(""))
                {
                    //lancio errore perchè manca velocità della pallina
                    Debug.LogError("Parametro Speed vuoto");
                    ballSpeed = "Lento";
                }
                //ESTRARRE LA ZONA DI CAMPO SUGGERITA DAL TARGET

                //ESTRARRE LA ZONA DI CAMPO COLPITA (other.gameObject.name)

                //CONFRONTO

                //GESTIONE OK OPPURE ERRORE
            }

            // Disabilito il collisore dell'instanza della palla dopo la prima collisione
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;


        }

        // se colpisco aree differenti da quelle della hashtable corretta
        else if (areasManager.CheckHitError(other.gameObject.name)) 
        {
            // aggiorno conteggi totali
            AreasManager.Instance.totalcounter += 1;
            totali.text = "Totali: " + AreasManager.Instance.totalcounter;
            totaliPanel.text = "Totali: " + AreasManager.Instance.totalcounter;
            playerState.totalcounter = AreasManager.Instance.totalcounter;

            // Riporto l'errore
            errori.text = "ERRORE: colpito " + other.gameObject.name;

            // pulisco la hashMap (reinizializzo)

            ErrorAreaClip.Play();

            // Disabilito il collisore dell'instanza della palla dopo la prima collisione
            (gameObject.GetComponent(typeof(SphereCollider)) as Collider).enabled = false;
        }

       
    }

    private void Pulse()
    {
        if (hand != null)
        {
            RumbleController(speed / 350, speed * 500);
            //hand.controller.TriggerHapticPulse(3000);
            RacketHit.Play();
        }

    }

    void RumbleController(float duration, float strength)
    {
        StartCoroutine(RumbleControllerRoutine(duration, strength));
    }

    IEnumerator RumbleControllerRoutine(float duration, float strength)
    {
        strength = Mathf.Clamp01(strength);
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime <= duration)
        {
            int valveStrength = Mathf.RoundToInt(Mathf.Lerp(0, 3999, strength));

            hand.controller.TriggerHapticPulse((ushort)valveStrength);

            yield return null;
        }
    }
}
