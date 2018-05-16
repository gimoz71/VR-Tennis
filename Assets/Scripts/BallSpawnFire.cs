using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawnFire : MonoBehaviour {

    private BallTextureManager balltextureManager;
    private TargetManager targetManager;
    private ColorManager colorManager;

    public Animator AvatarAnim;

    [Header("Pulsanti Start e Stop")]
    public GameObject startButton;
    public GameObject stopButton;

    [Header("Colore Palla (Prefab)")]
    public Texture BaseTexture;
    public Texture RedTexture;
    public Texture FucsiaTexture;
    public Texture OrangeTexture;
    public Texture BlueTexture;

    [Header("Simbolo Palla (Prefab)")]
    public Texture ATexture;
    public Texture BTexture;
    public Texture CTexture;
    public Texture DTexture;
    public Texture ETexture;

    [Header("Sprite per pannello di supporto utente")]

    // per opzione target
    public Sprite checkUI;
    public Sprite disabledUI;

    // per opzioni MultiX
    public Sprite defaultUI;//0

    // per opzione MultiColore
    public Sprite redUI;//1
    public Sprite fucsiaUI;//2
    public Sprite orangeUI;//3
    public Sprite blueUI;//4

    // per opzione MultiSimbolo
    
    public Sprite aUI;
    public Sprite bUI;
    public Sprite cUI;
    public Sprite dUI;

    [Header("Aree per opzione Target (Toggle)")]
    public Toggle TGAreaPosterioreDX;
    public Toggle TGAreaAnterioreDX;
    public Toggle TGAreaPosterioreSX;
    public Toggle TGAreaAnterioreSX;

    [Header("Aree per opzioni Colori e Simboli (DropDown)")]
    public Dropdown MCAreaPosterioreDX;
    public Dropdown MCAreaAnterioreDX;
    public Dropdown MCAreaPosterioreSX;
    public Dropdown MCAreaAnterioreSX;

    public Dropdown MSAreaPosterioreDX;
    public Dropdown MSAreaAnterioreDX;
    public Dropdown MSAreaPosterioreSX;
    public Dropdown MSAreaAnterioreSX;

    // variabili per associazione aree opzioni MultiX (colori/simboli)
    private int mx_ap_dx;
    private int mx_aa_dx;
    private int mx_ap_sx;
    private int mx_aa_sx;

    // variabili per associazioni aree opzione Target
    private bool tg_ap_dx;
    private bool tg_aa_dx;
    private bool tg_ap_sx;
    private bool tg_aa_sx;


    [Header("Lista Toggle Group nei Pannelli  Opzioni")]

    private CanvasGroup targetCanvasGroup;
    private CanvasGroup multiColoreCanvasGroup;
    private CanvasGroup multiSimboloCanvasGroup;
    private CanvasGroup differenziazioneCanvasGroup;
    private CanvasGroup decisionMakingCanvasGroup;


    private Text protocolloAttivo;

    [Header("Palla (Prefab)")]
    public GameObject ballPrefab;

    [Header("Target Area")]
    public GameObject mxMarker;

    [Header("Origine lancio")]
    public Transform playerTransform;
        
    [Header("Sorgente Lancio")]
    public Transform source;

    [Header("Target Lancio")]
    public Transform target;

    [Header("Modificatore di potenza")]
    public int pulseForce;

    [Header("Selettore difficoltà (UI)")]
    public GameObject GetActiveToggle;

    [Header("Modificatori di sequenza lancio (UI)")]
    public GameObject IntervalSlider;
    public GameObject QuantitySlider;
    public GameObject DelaySlider;

    [Header("Aggancio difficoltà (trigger ActiveToggle)")]
    public ToggleDifficulty _ToggleDifficultyScript;

    /*private float interval = 4;
    private float quantity = 10;
    private float delay = 4;*/

    private CanvasGroup canvasProtocolsSwitch; // pannello operatore
    private CanvasGroup canvasOptionsSwitch; // pannello operatore
    private CanvasGroup canvasCommonOptionsSwitch; // pannello operatore


    private GameObject refYard; // pannello utente
    private Image area1;
    private Image area2;
    private Image area3;
    private Image area4;

    // materiale e posizione del marker per i multix (forse obsoleto)
    private Renderer mxMarkerTexture; 
    private Transform mxMarkerTransform;

    Coroutine startRitardoLancio;
    public Text countDown;
    private float duration;

    private void Start()
    {
        Time.timeScale = 1;
        balltextureManager = BallTextureManager.Instance;
        targetManager = TargetManager.Instance;

        AvatarAnim = GameObject.Find("AvatarAvversario").GetComponent<Animator>(); // mi serve per attivare l'animazione dell'avversario al lancio
        canvasProtocolsSwitch = GameObject.Find("Sezione").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva
        canvasOptionsSwitch = GameObject.Find("Cruscotto Sezione").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva
        canvasCommonOptionsSwitch = GameObject.Find("Cruscotto elementi comuni").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva

        // gestione dell'interfaccia di ausilio utente e la setto su disabilitata all'avvio (appare soltanto durante la sessione)
        refYard = GameObject.Find("[PLAYER INFO]");
        refYard.SetActive(false);

        startButton.SetActive(true);
        stopButton.SetActive(false);




        // setto le proprietà per il settaggio del marker MultiX (forse obsoleto)
        mxMarkerTexture = mxMarker.GetComponent<Renderer>();
        mxMarkerTransform = mxMarker.GetComponent<Transform>();

        // aggancio al titolo del protocollo 
        protocolloAttivo = GameObject.Find("Protocollo Attivo").GetComponent<Text>();

        // aggancio i pannelli delle opzioni (quando sono attivi)
        targetCanvasGroup = GameObject.Find("PanelTG").GetComponent<CanvasGroup>();

        if (protocolloAttivo.text != "Protocollo Base")
        {
            multiColoreCanvasGroup = GameObject.Find("PanelMC").GetComponent<CanvasGroup>();
            multiSimboloCanvasGroup = GameObject.Find("PanelMS").GetComponent<CanvasGroup>();
            differenziazioneCanvasGroup = GameObject.Find("PanelDIFF").GetComponent<CanvasGroup>();
            decisionMakingCanvasGroup = GameObject.Find("PanelDM").GetComponent<CanvasGroup>();
        }


        
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_A, BaseTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_B, RedTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_C, FucsiaTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_D, OrangeTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_E, BlueTexture);

        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_A, ATexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_B, BTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_C, CTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_D, DTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_E, ETexture);

       

    }

    private Sprite getSpriteFromIndex(int index) {
        switch (index) {
            case 1:
                return redUI;
            case 2:
                return fucsiaUI;
            case 3:
                return orangeUI;
            case 4:
                return blueUI;
            default:
                return defaultUI;
        }
    }
 

    // Lancio palle come da parametri settati negli sliders
    public void SerialFire()
    {
       
        

        if (targetCanvasGroup.interactable == true)
        {
            

            refYard.SetActive(true);

            area1 = GameObject.Find("Area1").GetComponent<Image>();
            area2 = GameObject.Find("Area2").GetComponent<Image>();
            area3 = GameObject.Find("Area3").GetComponent<Image>();
            area4 = GameObject.Find("Area4").GetComponent<Image>();

            tg_aa_dx = TGAreaAnterioreDX.GetComponent<Toggle>().isOn;
            tg_ap_dx = TGAreaPosterioreDX.GetComponent<Toggle>().isOn;
            tg_aa_sx = TGAreaAnterioreSX.GetComponent<Toggle>().isOn;
            tg_ap_sx = TGAreaPosterioreSX.GetComponent<Toggle>().isOn;

            Debug.Log(tg_aa_dx + " "+ tg_aa_sx + " " + tg_ap_dx + " " + tg_ap_sx);
            Dictionary<string, bool> associazioneTargetArea = new Dictionary<string, bool>();

            associazioneTargetArea.Add("AreaAnterioreDX", tg_aa_dx);
            associazioneTargetArea.Add("AreaPosterioreDX", tg_ap_dx);
            associazioneTargetArea.Add("AreaAnterioreSX", tg_aa_sx);
            associazioneTargetArea.Add("AreaPosterioreSX", tg_ap_sx);

            targetManager.setAssociazioneTargetArea(associazioneTargetArea);
            
            area1.sprite = (tg_ap_dx ? checkUI : disabledUI);
            area2.sprite = (tg_aa_dx ? checkUI : disabledUI);
            area3.sprite = (tg_aa_sx ? checkUI : disabledUI);
            area4.sprite = (tg_ap_sx ? checkUI : disabledUI);
            
        }
        else
        {

            refYard.SetActive(true);
            int[,] matrice;

            if (multiColoreCanvasGroup.interactable == true)
            {
                area1 = GameObject.Find("Area1").GetComponent<Image>();
                area2 = GameObject.Find("Area2").GetComponent<Image>();
                area3 = GameObject.Find("Area3").GetComponent<Image>();
                area4 = GameObject.Find("Area4").GetComponent<Image>();

                SelectMX selMc = GameObject.Find("PanelMC").GetComponent<SelectMX>();
                matrice = selMc.getMatrice();
                mx_aa_dx = MCAreaAnterioreDX.GetComponent<Dropdown>().value;
                mx_ap_dx = MCAreaPosterioreDX.GetComponent<Dropdown>().value;
                mx_aa_sx = MCAreaAnterioreSX.GetComponent<Dropdown>().value;
                mx_ap_sx = MCAreaPosterioreSX.GetComponent<Dropdown>().value;

                area1.sprite = getSpriteFromIndex(mx_ap_dx);
                area2.sprite = getSpriteFromIndex(mx_aa_dx);
                area3.sprite = getSpriteFromIndex(mx_aa_sx);
                area4.sprite = getSpriteFromIndex(mx_ap_sx);
            }
            else if (multiSimboloCanvasGroup.interactable == true)
            {
                area1 = GameObject.Find("Area1").GetComponent<Image>();
                area2 = GameObject.Find("Area2").GetComponent<Image>();
                area3 = GameObject.Find("Area3").GetComponent<Image>();
                area4 = GameObject.Find("Area4").GetComponent<Image>();

                SelectMX selMs = GameObject.Find("PanelMS").GetComponent<SelectMX>();
                matrice = selMs.getMatrice();
                mx_aa_dx = MSAreaAnterioreDX.GetComponent<Dropdown>().value;
                mx_ap_dx = MSAreaPosterioreDX.GetComponent<Dropdown>().value;
                mx_aa_sx = MSAreaAnterioreSX.GetComponent<Dropdown>().value;
                mx_ap_sx = MSAreaPosterioreSX.GetComponent<Dropdown>().value;

                area1.sprite = getSpriteFromIndex(mx_ap_dx);
                area2.sprite = getSpriteFromIndex(mx_aa_dx);
                area3.sprite = getSpriteFromIndex(mx_aa_sx);
                area4.sprite = getSpriteFromIndex(mx_ap_sx);
            }

            

            Debug.Log(mx_ap_dx + " " + mx_aa_dx + " " + mx_ap_sx + " " + mx_aa_sx);

            Dictionary<string, int> associazioneTextureArea = new Dictionary<string, int>();

            associazioneTextureArea.Add("AreaAnterioreDX", mx_aa_dx);
            associazioneTextureArea.Add("AreaPosterioreDX", mx_ap_dx);
            associazioneTextureArea.Add("AreaAnterioreSX", mx_aa_sx);
            associazioneTextureArea.Add("AreaPosterioreSX", mx_ap_sx);
            balltextureManager.setAssociazioneTextureArea(associazioneTextureArea);
        }

        

        


        float interval = IntervalSlider.GetComponent<Slider>().value;
        float quantity = QuantitySlider.GetComponent<Slider>().value;
        float delay = DelaySlider.GetComponent<Slider>().value;

        // avvio la routine di ritardo (che lancia la sequenza lancio)
        startRitardoLancio = StartCoroutine(ritardoLancio(delay, quantity, interval));
    }

    // Funzione parametro di ritardo per il loop base del lancio
    public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
    {
        //yield return new WaitForSeconds(3);

        canvasProtocolsSwitch.interactable = false;
        canvasOptionsSwitch.interactable = false;
        canvasCommonOptionsSwitch.interactable = false;

        startButton.SetActive(false);
        stopButton.SetActive(true);

        duration = myDelay;

        countDown.text = "" + duration;
        while (duration > 0)
        {
            yield return new WaitForSeconds(1);
            duration--;
            countDown.text = "" + duration;
        }
        //yield return new WaitForSeconds(myDelay);
        yield return StartCoroutine(sequenzaLancio(myQuantity, myInterval));
    }

	// loop base del lancio
	public IEnumerator sequenzaLancio(float count, float separation) {
        for (int i = 0; i < count; i++) {
            _ToggleDifficultyScript.ActiveToggle();
            
            // toggle avatar animation
            yield return new WaitForSeconds(0.5f);
            AvatarAnim.SetBool("Start", true);
            yield return new WaitForSeconds(0.75f);
            fire();
            yield return new WaitForSeconds(2);
            AvatarAnim.SetBool("Start", false);

            yield return new WaitForSeconds (separation);
            if(i == count-1)
            {
                //Debug.Log("FINE LOOP----------------");
                canvasProtocolsSwitch.interactable = true;
                canvasOptionsSwitch.interactable = true;
                canvasCommonOptionsSwitch.interactable = true;
                refYard.SetActive(false);
                startButton.SetActive(true);
                stopButton.SetActive(false);
            }

            
        }
        
    }

    // Funziona base lancio palle
    public void fire()
    {

        // Creo L'istanza del prefab della pallina
        GameObject tennisBall = Instantiate(ballPrefab, playerTransform.position, Quaternion.identity) as GameObject;
           
        // Lancio l'istanza nella scena in base ai parametri di forza e rotazione
        tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);
        //tennisBall.GetComponent<Rigidbody>().AddTorque(0,0,-0.2f);

        // trovo la mesh della pallina (child della pallina)) e gli assegno una texture random tra quelle definite in textureManager.cs
        GameObject palla = tennisBall.transform.Find("TennisBall/ball").gameObject;
        Renderer pallaPrefab = palla.GetComponent<Renderer>();
        pallaPrefab.material.mainTexture = balltextureManager.RandomTexture();
        //Debug.Log("TEXTURE: "+pallaPrefab.material.mainTexture);
        mxMarkerTexture.material.mainTexture = balltextureManager.RandomTexture();

        // Distruggo la pallina dopo N secondi
        Destroy(tennisBall, 15);
    }

    
    public void stopLancio()
    {
        StopAllCoroutines();
        canvasProtocolsSwitch.interactable = true;
        canvasOptionsSwitch.interactable = true;
        canvasCommonOptionsSwitch.interactable = true;
        refYard.SetActive(false);
        startButton.SetActive(true);
        stopButton.SetActive(false);
    }
}

