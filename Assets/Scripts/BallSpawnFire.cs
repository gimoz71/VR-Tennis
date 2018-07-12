using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.modelli;

public class BallSpawnFire : MonoBehaviour {

    private BallTextureManager balltextureManager;
    private TargetManager targetManager;
    private ColorManager colorManager;
    private DMDrawManager dmDrawManager;
    private ScoreManager scoreManager;
    private StroboManager stroboManager;
	private HeadEyeMovementScaler headEyeMovementScaler;

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

    [Header("Marker Decision Making (Gameobject)")]
    public GameObject dmTargetDX;
    public GameObject dmTargetSX;
    public GameObject dmDistrattoreDX;
    public GameObject dmDistrattoreSX;

    [Header("Marker Decision Making L4 (Gameobject)")]
    public GameObject dmTargetAvatarADX;
    public GameObject dmTargetAvatarASX;
    public GameObject dmDistrattoreAvatarADX;
    public GameObject dmDistrattoreAvatarASX;
    public GameObject dmTargetAvatarPDX;
    public GameObject dmTargetAvatarPSX;
    public GameObject dmDistrattoreAvatarPDX;
    public GameObject dmDistrattoreAvatarPSX;

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

    private CanvasGroup canvasProtocolsSwitch; // pannello operatore Protocolli
    private CanvasGroup canvasOptionsSwitch; // pannello operatore Opzioni
    private CanvasGroup canvasCommonOptionsSwitch; // pannello operatore Variabili
    private CanvasGroup canvasUserdataSwitch; // pannello operatore dati


    private GameObject refYard; // pannello utente
    private GameObject countDownPanel; // pannello countdown
    private Image area1;
    private Image area2;
    private Image area3;
    private Image area4;

    // materiale e posizione del marker per i multix (forse obsoleto)
    private Renderer mxMarkerTexture; 
    private Transform mxMarkerTransform;

    Coroutine startRitardoLancio;
    private bool isRunning;
    public Text countDown;
    private float duration;

    private Partita partita;

    private void Start()
    {
        Time.timeScale = 1;
        balltextureManager = BallTextureManager.Instance;
        targetManager = TargetManager.Instance;
        dmDrawManager = DMDrawManager.Instance;
        scoreManager = ScoreManager.Instance;
		stroboManager = StroboManager.Instance;
		stroboManager = StroboManager.Instance;
		headEyeMovementScaler = HeadEyeMovementScaler.Instance;

        AvatarAnim = GameObject.Find("AvatarAvversario").GetComponent<Animator>(); // mi serve per attivare l'animazione dell'avversario al lancio
        canvasProtocolsSwitch = GameObject.Find("Sezione").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva
        canvasOptionsSwitch = GameObject.Find("Cruscotto Sezione").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva
        canvasCommonOptionsSwitch = GameObject.Find("Cruscotto elementi comuni").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva
        canvasUserdataSwitch = GameObject.Find("Punteggi").GetComponent<CanvasGroup>(); // i serve per disabilitare l'interfaccia operatore quando la sessione di lancio palle è attiva

        // gestione dell'interfaccia di ausilio utente e la setto su disabilitata all'avvio (appare soltanto durante la sessione)
        refYard = GameObject.Find("[PLAYER INFO]");
        countDownPanel = GameObject.Find("[COUNTDOWN]");
        refYard.SetActive(false);

        startButton.SetActive(true);
        stopButton.SetActive(false);

        isRunning = false;


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


        
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_BASE, BaseTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_A, RedTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_B, FucsiaTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_C, OrangeTexture);
        balltextureManager.MappColorePalla.Add(BallTextureManager.TEXTURE_D, BlueTexture);

        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_BASE, ATexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_A, BTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_B, CTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_C, DTexture);
        balltextureManager.MappSimboloPalla.Add(BallTextureManager.TEXTURE_D, ETexture);

       

    }

    // Condizioni per i tipo di prite Colore
    private Sprite getSpriteMCFromIndex(int index) {
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

    // Condizioni per i tipo di prite Simbolo
    private Sprite getSpriteMSFromIndex(int index)
    {
        switch (index)
        {
            case 1:
                return aUI;
            case 2:
                return bUI;
            case 3:
                return cUI;
            case 4:
                return dUI;
            default:
                return defaultUI;
        }
    }


    // Lancio palle come da parametri settati negli sliders
    public void SerialFire()
    {
        AreasManager.Instance.resetCounters();

        if (!isRunning) // per evitatare il doppio avvio
        {

            DateTime currentDate = DateTime.Now;
            scoreManager.PartitaTemporanea.Data = currentDate.ToString("dd/MM/yyyy HH:mm:ss");

            if (targetCanvasGroup.interactable)
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

                Debug.Log(tg_aa_dx + " " + tg_aa_sx + " " + tg_ap_dx + " " + tg_ap_sx);
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
            else if (multiColoreCanvasGroup.interactable)
            {
                refYard.SetActive(true);
                int[,] matrice;

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

                area1.sprite = getSpriteMCFromIndex(mx_ap_dx);
                area2.sprite = getSpriteMCFromIndex(mx_aa_dx);
                area3.sprite = getSpriteMCFromIndex(mx_aa_sx);
                area4.sprite = getSpriteMCFromIndex(mx_ap_sx);

                Debug.Log(mx_ap_dx + " " + mx_aa_dx + " " + mx_ap_sx + " " + mx_aa_sx);

                Dictionary<string, int> associazioneTextureArea = new Dictionary<string, int>();

                associazioneTextureArea.Add("AreaAnterioreDX", mx_aa_dx);
                associazioneTextureArea.Add("AreaPosterioreDX", mx_ap_dx);
                associazioneTextureArea.Add("AreaAnterioreSX", mx_aa_sx);
                associazioneTextureArea.Add("AreaPosterioreSX", mx_ap_sx);
                balltextureManager.setAssociazioneTextureArea(associazioneTextureArea);

            }
            else if (multiSimboloCanvasGroup.interactable)
            {
                refYard.SetActive(true);
                int[,] matrice;

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

                area1.sprite = getSpriteMSFromIndex(mx_ap_dx);
                area2.sprite = getSpriteMSFromIndex(mx_aa_dx);
                area3.sprite = getSpriteMSFromIndex(mx_aa_sx);
                area4.sprite = getSpriteMSFromIndex(mx_ap_sx);

                Debug.Log(mx_ap_dx + " " + mx_aa_dx + " " + mx_ap_sx + " " + mx_aa_sx);

                Dictionary<string, int> associazioneTextureArea = new Dictionary<string, int>();

                associazioneTextureArea.Add("AreaAnterioreDX", mx_aa_dx);
                associazioneTextureArea.Add("AreaPosterioreDX", mx_ap_dx);
                associazioneTextureArea.Add("AreaAnterioreSX", mx_aa_sx);
                associazioneTextureArea.Add("AreaPosterioreSX", mx_ap_sx);
                balltextureManager.setAssociazioneTextureArea(associazioneTextureArea);
            }
            else if (decisionMakingCanvasGroup.interactable)
            {

                countDownPanel.SetActive(true);
                Dictionary<string, int[]> associazioneMarkersArea = new Dictionary<string, int[]>();

                //condidero la rete a destra
                associazioneMarkersArea.Add("AreaAnterioreDX", new int[] { 0, 1 });
                associazioneMarkersArea.Add("AreaPosterioreDX", new int[] { 0, 0 });
                associazioneMarkersArea.Add("AreaAnterioreSX", new int[] { 1, 1 });
                associazioneMarkersArea.Add("AreaPosterioreSX", new int[] { 1, 0 });

                //considero la rete in basso
                //associazioneMarkersArea.Add("AreaAnterioreDX", new int[] { 1, 1 });
                //associazioneMarkersArea.Add("AreaPosterioreDX", new int[] { 0, 1 });
                //associazioneMarkersArea.Add("AreaAnterioreSX", new int[] { 1, 0 });
                //associazioneMarkersArea.Add("AreaPosterioreSX", new int[] { 0, 0 });

                dmDrawManager.setAssociazioneDMArea(associazioneMarkersArea);
                dmDrawManager.setFirst();
            }


            float interval = IntervalSlider.GetComponent<Slider>().value;
            float quantity = QuantitySlider.GetComponent<Slider>().value;
            float delay = DelaySlider.GetComponent<Slider>().value;

            scoreManager.PartitaTemporanea.NumeroPalle = (int) quantity;
            scoreManager.PartitaTemporanea.TempoTraLePalle = (int)interval;

            //setto parametri strobo
            scoreManager.PartitaTemporanea.FrequenzaStrobo = stroboManager.getFrequenzaStrobo();
            scoreManager.PartitaTemporanea.PercentualeBuioStrobo = stroboManager.getPercentualeBuio();
            scoreManager.PartitaTemporanea.LenteChiusa = stroboManager.getLenteChiusa();
            scoreManager.PartitaTemporanea.Alternanza = stroboManager.getAlternanzaLenti();
			scoreManager.PartitaTemporanea.PercentualeHeadEyeMovement = headEyeMovementScaler.getSliderScaler() + "";

            Debug.Log("stroboManager.getFrequenzaStrobo(): " + stroboManager.getFrequenzaStrobo() 
                + "stroboManager.getPercentualeBuio(): " + stroboManager.getPercentualeBuio() 
                + "stroboManager.getLenteChiusa(): " + stroboManager.getLenteChiusa() 
                + "stroboManager.getAlternanzaLenti(): " + stroboManager.getAlternanzaLenti());

            

            // avvio la routine di ritardo (che lancia la sequenza lancio)
            startRitardoLancio = StartCoroutine(ritardoLancio(delay, quantity, interval));
        }
    }

    // Funzione parametro di ritardo per il loop base del lancio
    public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
    {
        countDownPanel.SetActive(true);
        isRunning = true;

        canvasProtocolsSwitch.interactable = false;
        canvasOptionsSwitch.interactable = false;
        canvasCommonOptionsSwitch.interactable = false;
        canvasUserdataSwitch.interactable = false;
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

        yield return StartCoroutine(CloseCountDown());

        for (int i = 0; i < count; i++) {
            _ToggleDifficultyScript.ActiveToggle();
            
            // toggle avatar animation
            yield return new WaitForSeconds(0.5f);
            AvatarAnim.SetBool("Start", true);
            yield return new WaitForSeconds(0.75f);
            
            fire();
            if (decisionMakingCanvasGroup.interactable)
            {
                if (dmDrawManager.getLivello() < 3)
                {

                    
                    drawMarkers();
                }
                else
                {
                    yield return new WaitForSeconds(1f);
                    drawMarkers();
                }
            }
            else {
                cleanMarkers();
            }
            yield return new WaitForSeconds(0);
            AvatarAnim.SetBool("Start", false);

            yield return new WaitForSeconds (separation);
            if(i == count-1)
            {
                //Debug.Log("FINE LOOP----------------");
                canvasProtocolsSwitch.interactable = true;
                canvasOptionsSwitch.interactable = true;
                canvasCommonOptionsSwitch.interactable = true;
                canvasUserdataSwitch.interactable = true;
                refYard.SetActive(false);
                startButton.SetActive(true);
                stopButton.SetActive(false);
                isRunning = false;
                cleanMarkers();
            }

            
        }

        //salvataggio partita

        scoreManager.PartitaTemporanea.ColpiCorretti = AreasManager.Instance.counter;
        scoreManager.PartitaTemporanea.ColpiSbagliati = AreasManager.Instance.wrongCounter;
        scoreManager.PartitaTemporanea.ColpiFuori = AreasManager.Instance.outCounter;

        scoreManager.PartitaTemporanea.MediaDalCentroRacchetta = AreasManager.Instance.distanceCounter / AreasManager.Instance.racketHitCounter;
        scoreManager.PartitaTemporanea.Accuratezza = ((float)((float)AreasManager.Instance.counter / (float)AreasManager.Instance.totalcounter) * 100) + "%";

        scoreManager.salvaPartita();
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
        if (multiSimboloCanvasGroup.interactable)
        {
            GameObject palla = tennisBall.transform.Find("TennisBall/ball").gameObject;
            GameObject billBoard = tennisBall.transform.Find("BillBoardOrigin/BillBoard").gameObject;
            GameObject trail = tennisBall.transform.Find("TrailRender").gameObject;

            Renderer pallaRenderer = palla.GetComponent<Renderer>();
            pallaRenderer.enabled = false;

            Renderer trailRenderer = trail.GetComponent<Renderer>();
           // trailRenderer.enabled = false;

            Renderer billBoardRenderer = billBoard.GetComponent<Renderer>();

            billBoardRenderer.enabled = true;

            billBoardRenderer.material.mainTexture = balltextureManager.RandomTexture();
        } else
        {
            GameObject palla = tennisBall.transform.Find("TennisBall/ball").gameObject;
            GameObject billBoard = tennisBall.transform.Find("BillBoardOrigin/BillBoard").gameObject;

            Renderer billBoardRenderer = billBoard.GetComponent<Renderer>();
            billBoardRenderer.enabled = false;

            Renderer pallaRenderer = palla.GetComponent<Renderer>();

            pallaRenderer.material.mainTexture = balltextureManager.RandomTexture();
        }
        
        //Debug.Log("TEXTURE: "+pallaPrefab.material.mainTexture);
        //mxMarkerTexture.material.mainTexture = balltextureManager.RandomTexture();

        // Distruggo la pallina dopo N secondi
        Destroy(tennisBall, 15);
    }

    
    public void stopLancio()
    {
        StopAllCoroutines();
        canvasProtocolsSwitch.interactable = true;
        canvasOptionsSwitch.interactable = true;
        canvasCommonOptionsSwitch.interactable = true;
        canvasUserdataSwitch.interactable = true;
        refYard.SetActive(false);
        startButton.SetActive(true);
        stopButton.SetActive(false);
        isRunning = false;
        cleanMarkers();
        countDownPanel.SetActive(false);
    }

    private void drawMarkers() {
        //1 recupero la matrice dal DMDrawManager
        int[,] matrice = dmDrawManager.getMarkersMatrix();
        
        //2 abilitare/disabilitare gli oggetti corretti in base alla matrice
        cleanMarkers();
        if (dmDrawManager.getLivello() < 3)
        {
            //marker a cerchietto
            dmTargetSX.SetActive(matrice[1,1] == DMDrawManager.VALORE_MATRICE_TARGET);
            dmDistrattoreSX.SetActive(matrice[1, 1] == DMDrawManager.VALORE_MATRICE_DISTRATTORE);
            dmTargetDX.SetActive(matrice[0, 1] == DMDrawManager.VALORE_MATRICE_TARGET);
            dmDistrattoreDX.SetActive(matrice[0,1] == DMDrawManager.VALORE_MATRICE_DISTRATTORE);
        }
        else {
            //marker a sagome
            //TOGLIERE QUESTA VERSIONE ATTUALE
            System.Random rand = new System.Random();
            int current = rand.Next(0, 2);

            if (current.Equals(0))
            {
                dmTargetAvatarASX.SetActive(matrice[1, 1] == DMDrawManager.VALORE_MATRICE_TARGET);
                dmDistrattoreAvatarASX.SetActive(matrice[1, 1] == DMDrawManager.VALORE_MATRICE_DISTRATTORE);
                dmTargetAvatarADX.SetActive(matrice[0, 1] == DMDrawManager.VALORE_MATRICE_TARGET);
                dmDistrattoreAvatarADX.SetActive(matrice[0, 1] == DMDrawManager.VALORE_MATRICE_DISTRATTORE);
            }
            else {
                dmTargetAvatarPSX.SetActive(matrice[1, 1] == DMDrawManager.VALORE_MATRICE_TARGET);
                dmDistrattoreAvatarPSX.SetActive(matrice[1, 1] == DMDrawManager.VALORE_MATRICE_DISTRATTORE);
                dmTargetAvatarPDX.SetActive(matrice[0, 1] == DMDrawManager.VALORE_MATRICE_TARGET);
                dmDistrattoreAvatarPDX.SetActive(matrice[0, 1] == DMDrawManager.VALORE_MATRICE_DISTRATTORE);
            }

            Debug.Log("CURRENT RANDOM NUMBER: " + current);
        }

    }

    private void cleanMarkers() {
        dmTargetSX.SetActive(false);
        dmDistrattoreSX.SetActive(false);
        dmTargetDX.SetActive(false);
        dmDistrattoreDX.SetActive(false);

        dmTargetAvatarASX.SetActive(false);
        dmDistrattoreAvatarASX.SetActive(false);
        dmTargetAvatarADX.SetActive(false);
        dmDistrattoreAvatarADX.SetActive(false);
        dmTargetAvatarPSX.SetActive(false);
        dmDistrattoreAvatarPSX.SetActive(false);
        dmTargetAvatarPDX.SetActive(false);
        dmDistrattoreAvatarPDX.SetActive(false);
    }

    public IEnumerator CloseCountDown()
    {
        yield return new WaitForSeconds(1);
        countDownPanel.SetActive(false);
    }

    private void printMatrix(int[,] matrice) {
        Debug.Log(matrice[0,0] + " | " + matrice[0,1]);
        Debug.Log("---------------------");
        Debug.Log(matrice[1, 0] + " | " + matrice[1, 1]);
        Debug.Log("*********************");
    }
}

