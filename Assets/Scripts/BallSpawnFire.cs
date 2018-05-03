using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawnFire : MonoBehaviour {

    private BallTextureManager balltextureManager;
    private TargetManager targetManager;
    private ColorManager colorManager;

    public Animator AvatarAnim;

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

    public Toggle TGAreaPosterioreDX;
    public Toggle TGAreaAnterioreDX;
    public Toggle TGAreaPosterioreSX;
    public Toggle TGAreaAnterioreSX;

    [Header("Assegnazione Aree/Colori (DropDown)")]
    public Dropdown MCAreaPosterioreDX;
    public Dropdown MCAreaAnterioreDX;
    public Dropdown MCAreaPosterioreSX;
    public Dropdown MCAreaAnterioreSX;

    public Dropdown MSAreaPosterioreDX;
    public Dropdown MSAreaAnterioreDX;
    public Dropdown MSAreaPosterioreSX;
    public Dropdown MSAreaAnterioreSX;

    private int mx_ap_dx;
    private int mx_aa_dx;
    private int mx_ap_sx;
    private int mx_aa_sx;

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

    private float interval = 4;
    private float quantity = 10;
    private float delay = 4;

    private CanvasGroup CanvasSwitch;

    

    private void Start()
    {
        balltextureManager = BallTextureManager.Instance;
        targetManager = TargetManager.Instance;

        AvatarAnim = GameObject.Find("AvatarAvversario").GetComponent<Animator>();
        CanvasSwitch = GameObject.Find("[MENU ISTRUTTORE (UI)]").GetComponent<CanvasGroup>();


        protocolloAttivo = GameObject.Find("Protocollo Attivo").GetComponent<Text>();

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

    // Lancio palle come da parametri settati negli sliders
    public void SerialFire()
    {
        if (targetCanvasGroup.interactable == true)
        {
            tg_aa_dx = TGAreaAnterioreDX.GetComponent<Toggle>().isOn;
            tg_ap_dx = TGAreaPosterioreDX.GetComponent<Toggle>().isOn;
            tg_aa_sx = TGAreaAnterioreSX.GetComponent<Toggle>().isOn;
            tg_ap_sx = TGAreaPosterioreSX.GetComponent<Toggle>().isOn;

            Dictionary<string, bool> associazioneTargetArea = new Dictionary<string, bool>();

            associazioneTargetArea.Add("AreaAnterioreDX", tg_aa_dx);
            associazioneTargetArea.Add("AreaPosterioreDX", tg_ap_dx);
            associazioneTargetArea.Add("AreaAnterioreSX", tg_aa_sx);
            associazioneTargetArea.Add("AreaPosterioreSX", tg_ap_sx);

            targetManager.setAssociazioneTargetArea(associazioneTargetArea);
        }
        else
        {

            if (multiColoreCanvasGroup.interactable == true)
            {
                mx_aa_dx = MCAreaAnterioreDX.GetComponent<Dropdown>().value;
                mx_ap_dx = MCAreaPosterioreDX.GetComponent<Dropdown>().value;
                mx_aa_sx = MCAreaAnterioreSX.GetComponent<Dropdown>().value;
                mx_ap_sx = MCAreaPosterioreSX.GetComponent<Dropdown>().value;
            }
            else if (multiSimboloCanvasGroup.interactable == true)
            {
                mx_aa_dx = MSAreaAnterioreDX.GetComponent<Dropdown>().value;
                mx_ap_dx = MSAreaPosterioreDX.GetComponent<Dropdown>().value;
                mx_aa_sx = MSAreaAnterioreSX.GetComponent<Dropdown>().value;
                mx_ap_sx = MSAreaPosterioreSX.GetComponent<Dropdown>().value;
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
        StartCoroutine(ritardoLancio(delay, quantity, interval));
    }

    // Funzione parametro di ritardo per il loop base del lancio
    public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
    {
        //yield return new WaitForSeconds(3);
        yield return new WaitForSeconds(myDelay);
        yield return StartCoroutine(sequenzaLancio(myQuantity, myInterval));
    }

	// loop base del lancio
	public IEnumerator sequenzaLancio(float count, float separation) {

        
        CanvasSwitch.interactable = false;
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
                CanvasSwitch.interactable = true;
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
        Renderer mxMarkerTexture = mxMarker.GetComponent<Renderer>();
        pallaPrefab.material.mainTexture = balltextureManager.RandomTexture();
        Debug.Log("TEXTURE: "+pallaPrefab.material.mainTexture);
        mxMarkerTexture.material.mainTexture = balltextureManager.RandomTexture();

        // Distruggo la pallina dopo N secondi
        Destroy(tennisBall, 15);
    }
}

