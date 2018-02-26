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
    public GameObject multiColoreToggleGroup;
    public GameObject multiSimboloToggleGroup;
    public GameObject differenziazioneToggleGroup;
    public GameObject decisionMakingToggleGroup;

    [Header("Slider HEM")]
    public Slider EyeSlider;

    // inizializzo le variabili dei manager 
    private BallTextureManager balltextureManager;
    private AreasManager areasManager;


    private StroboManager stroboManager;

   
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
        multiColore.gameObject.SetActive(false);
        balltextureManager.setShutdownTextureIndex();

        multiSimbolo.gameObject.SetActive(false);

        decisionMaking.gameObject.SetActive(false);
        // decisionmakingManager.setShutdownDecisionManagerIndex(False);

        strobo.gameObject.SetActive(false);
        // set to default script (TODO)

        headEyeMovement.gameObject.SetActive(false);
        ResetHEM();

        differenziazione.gameObject.SetActive(false);

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

            balltextureManager.setShutdownTextureIndex(); // SPENTO

            multiColore.gameObject.SetActive(false);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(false);
            // decisionmakingManager.setShutdownDecisionManagerIndex(False);

            strobo.gameObject.SetActive(false);
            stroboManager.StopStrobo();

            headEyeMovement.gameObject.SetActive(false);
            ResetHEM();

            differenziazione.gameObject.SetActive(false);
            //
        }
        if (buttonClicked == cognitivoButton)
        {
            Debug.Log("COGNITIVO");
            m_Text.text = "Protocollo Cognitivo";
            
            balltextureManager.setDefaultTextureIndex(); // DEFAULT

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(true);

            decisionMaking.gameObject.SetActive(true);
            // 

            strobo.gameObject.SetActive(false);
            stroboManager.StopStrobo();

            headEyeMovement.gameObject.SetActive(true);
            ResetHEM();

            differenziazione.gameObject.SetActive(true);

            multiColoreToggleGroup.gameObject.SetActive(true);
            multiSimboloToggleGroup.gameObject.SetActive(false);
            decisionMakingToggleGroup.gameObject.SetActive(false);
            differenziazioneToggleGroup.gameObject.SetActive(false);

        }
        if (buttonClicked == visionButton)
        {
            Debug.Log("VISION");
            m_Text.text = "Vision Training";

            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(false);

            strobo.gameObject.SetActive(true);
            stroboManager.StartStrobo();

            headEyeMovement.gameObject.SetActive(true);
            ResetHEM();

            differenziazione.gameObject.SetActive(true);
            //


            multiColoreToggleGroup.gameObject.SetActive(true);
            multiSimboloToggleGroup.gameObject.SetActive(false);
            decisionMakingToggleGroup.gameObject.SetActive(false);
            differenziazioneToggleGroup.gameObject.SetActive(false);
        }
        if (buttonClicked == servizioButton)
        {
            Debug.Log("SERVIZIO");
            m_Text.text = "Risposta al Servizio";

            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(true);

            strobo.gameObject.SetActive(true);
            //stroboManager.StartStrobo();

            headEyeMovement.gameObject.SetActive(true);
            ResetHEM();

            differenziazione.gameObject.SetActive(false);


            multiColoreToggleGroup.gameObject.SetActive(true);
            multiSimboloToggleGroup.gameObject.SetActive(false);
            decisionMakingToggleGroup.gameObject.SetActive(false);
            differenziazioneToggleGroup.gameObject.SetActive(false);

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

    void ResetHEM()
    {
        EyeSlider.value = 0;
    }
}