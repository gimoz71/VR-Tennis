using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ProtocolsManager : MonoBehaviour
{
    public Text m_Text;
    public Color activeColor;
    public Color inactiveColor;

    [Header("Pulsante in focus")]
    public GameObject iconBG;

    [Header("Lista Pulsante Protocolli)")]
    public Button baseButton;
    public Button cognitivoButton;
    public Button visionButton;
    public Button servizioButton;

    [Header("Background Pulsante Protocolli)")]
    public GameObject baseiconBG;
    public GameObject cognitivoiconBG;
    public GameObject visioniconBG;
    public GameObject servizioiconBG;

    [Header("Lista Pannelli Opzioni")]
    public GameObject multiColore;
    public GameObject multiSimbolo;
    public GameObject decisionMaking;
    public GameObject strobo;
    public GameObject headEyeMovement;
    public GameObject differenziazione;

    [Header("Lista Toggle Group nei Pannelli  Opzioni")]
    public CanvasGroup targetCanvasGroup;
    public CanvasGroup multiColoreCanvasGroup;
    public CanvasGroup multiSimboloCanvasGroup;
    public CanvasGroup differenziazioneCanvasGroup;
    public CanvasGroup decisionMakingCanvasGroup;

    [Header("Posizione giocatori")]
    public GameObject playerPositionDefault;
    public GameObject playerPositionServizio;
    public GameObject avversarioPositionDefault;
    public GameObject avversarioPositionServizio;

    // inizializzo le variabili dei manager 
    private BallTextureManager balltextureManager;
    private AreasManager areasManager;
    private HeadEyeMovementScaler hemObject;

    private StroboManager stroboManager;


    //setto la posizione e la visibilità dei giocatori in base al protocollo
    private GameObject playerPosition;
    private GameObject avversarioPosition;

    private GameObject avatarAvversario;
    private GameObject ballLauncher;



    


    // Genero L'hashtable dei pulsanti
    public Dictionary<Button, GameObject> buttonIconPair = new Dictionary<Button, GameObject>();

    void pairButtonIcon()
    {
        buttonIconPair.Add(baseButton, baseiconBG);
        buttonIconPair.Add(cognitivoButton, cognitivoiconBG);
        buttonIconPair.Add(visionButton, visioniconBG);
        buttonIconPair.Add(servizioButton, servizioiconBG);

    }

    void Start()
    {
        
        playerPosition = GameObject.Find("Player");
        avversarioPosition = GameObject.Find("AvversarioOrigine");

        avatarAvversario = GameObject.Find("AvatarAvversario");
        ballLauncher = GameObject.Find("ballLauncher");

        avatarAvversario.SetActive(false);
        ballLauncher.SetActive(true);

        avversarioPosition.transform.position = avversarioPositionDefault.transform.position;
        playerPosition.transform.position = playerPositionDefault.transform.position;

        hemObject = GameObject.Find("SliderChiusura").GetComponent<HeadEyeMovementScaler>();
        balltextureManager = BallTextureManager.Instance;
        areasManager = AreasManager.Instance;

        //stroboManager = new StroboManager();

        StroboManager[] components = Resources.FindObjectsOfTypeAll<StroboManager>();

        if (components.Length > 0)
        {
            stroboManager = components[0];
        }
        Debug.Log("ARRAY: " + components.Length);

        m_Text.text = "Protocollo Base";
        balltextureManager.setShutdownTextureIndex();
        multiColore.gameObject.SetActive(false);
        multiSimbolo.gameObject.SetActive(false);
        decisionMaking.gameObject.SetActive(false);
        // decisionmakingManager.setShutdownDecisionManagerIndex(False);
        strobo.gameObject.SetActive(false);
        headEyeMovement.gameObject.SetActive(false);
        differenziazione.gameObject.SetActive(false);
        hemObject.disableHEM();

        pairButtonIcon();
        //inactiveColor = iconBG.GetComponent<Image>().color;


        // Setto i pulsanti nelo stato (visivo) di default
        baseiconBG.GetComponent<Image>().color = activeColor;
        cognitivoiconBG.GetComponent<Image>().color = inactiveColor;
        visioniconBG.GetComponent<Image>().color = inactiveColor;
        servizioiconBG.GetComponent<Image>().color = inactiveColor;

    }

    // Use this for initialization
    void buttonCallBack(Button buttonClicked)
    {
       
        for (int i = 0; i < buttonIconPair.Count; i++)
        {
            var item = buttonIconPair.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            if (buttonClicked == itemKey)
            {
                itemValue.GetComponent<Image>().color = activeColor;
            }
            else
            {
                itemValue.GetComponent<Image>().color = inactiveColor;
            }
        }
        if (buttonClicked == baseButton)
        {

            Debug.Log("BASE");
            m_Text.text = "Protocollo Base";

            avversarioPosition.transform.position = avversarioPositionDefault.transform.position;
            playerPosition.transform.position = playerPositionDefault.transform.position;

            avatarAvversario.SetActive(false);
            ballLauncher.SetActive(true);

            balltextureManager.setShutdownTextureIndex(); // SPENTO

            multiColore.gameObject.SetActive(false);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(false);

            strobo.gameObject.SetActive(false);
            stroboManager.StopStrobo();

            headEyeMovement.gameObject.SetActive(false);
            hemObject.disableHEM();

            differenziazione.gameObject.SetActive(false);
            
        }
        if (buttonClicked == cognitivoButton)
        {
            Debug.Log("COGNITIVO");
            m_Text.text = "Protocollo Cognitivo";

            avversarioPosition.transform.position = avversarioPositionDefault.transform.position;
            playerPosition.transform.position = playerPositionDefault.transform.position;

            avatarAvversario.SetActive(false);
            ballLauncher.SetActive(true);

            balltextureManager.setDefaultTextureIndex(); // DEFAULT

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(true);

            decisionMaking.gameObject.SetActive(true);

            strobo.gameObject.SetActive(false);
            stroboManager.StopStrobo();

            headEyeMovement.gameObject.SetActive(true);
            hemObject.enableHEM();

            differenziazione.gameObject.SetActive(true);
            

            //settaggi di partenza
            targetCanvasGroup.interactable = true;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = false;

           


        }
        if (buttonClicked == visionButton)
        {
            Debug.Log("VISION");
            m_Text.text = "Vision Training";

            avversarioPosition.transform.position = avversarioPositionDefault.transform.position;
            playerPosition.transform.position = playerPositionDefault.transform.position;

            avatarAvversario.SetActive(false);
            ballLauncher.SetActive(true);

            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(false);

            strobo.gameObject.SetActive(true);

            headEyeMovement.gameObject.SetActive(true);
            hemObject.enableHEM();

            differenziazione.gameObject.SetActive(true);
            

            //settaggi di partenza
            targetCanvasGroup.interactable = true;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = false;
        }
        if (buttonClicked == servizioButton)
        {
            
            Debug.Log("SERVIZIO");
            m_Text.text = "Risposta al Servizio";

            avversarioPosition.transform.position = avversarioPositionServizio.transform.position;
            playerPosition.transform.position = playerPositionServizio.transform.position;

            avatarAvversario.SetActive(true);
            ballLauncher.SetActive(false);

            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(true);

            strobo.gameObject.SetActive(true);

            headEyeMovement.gameObject.SetActive(true);
            hemObject.enableHEM();

            differenziazione.gameObject.SetActive(false);
            

            //settaggi di partenza
            targetCanvasGroup.interactable = true;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = false;

        }
    }

    void OnEnable()
    {
        baseButton.onClick.AddListener(() => buttonCallBack(baseButton));
        cognitivoButton.onClick.AddListener(() => buttonCallBack(cognitivoButton));
        visionButton.onClick.AddListener(() => buttonCallBack(visionButton));
        servizioButton.onClick.AddListener(() => buttonCallBack(servizioButton));
    }


    void OnDisable()
    {

    }
    
}