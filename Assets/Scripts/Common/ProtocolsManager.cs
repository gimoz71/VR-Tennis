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

    [Header("Lista Opzioni")]
    public GameObject multiColore;
    public GameObject multiSimbolo;
    public GameObject decisionMaking;
    public GameObject strobo;
    public GameObject headEyeMovement;
    public GameObject differenziazione;

    [Header("Slider HEM")]
    public Slider EyeSlider;

    // inizializzo le variabili dei manager 
    private ColorManager colorManager;
    private SymbolManager symbolManager;
    private AreasManager areasManager;


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

        colorManager = ColorManager.Instance;
        symbolManager = SymbolManager.Instance;
        areasManager = AreasManager.Instance;


        m_Text.text = "Protocollo Base";
        multiColore.gameObject.SetActive(false);

        multiSimbolo.gameObject.SetActive(false);
        symbolManager.setShutdownSymbolIndex(); // SPENTO

        decisionMaking.gameObject.SetActive(false);

        strobo.gameObject.SetActive(false);

        headEyeMovement.gameObject.SetActive(false);
        ResetHEM();

        differenziazione.gameObject.SetActive(false);

        pairButtonIcon();
        //inactiveColor = iconBG.GetComponent<Image>().color;

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

            multiColore.gameObject.SetActive(false);
            

            multiSimbolo.gameObject.SetActive(false);
            symbolManager.setShutdownSymbolIndex(); // SPENTO

            decisionMaking.gameObject.SetActive(false);
            // SPENTO

            strobo.gameObject.SetActive(false);
            // SPENTO

            headEyeMovement.gameObject.SetActive(false);
            ResetHEM();

            differenziazione.gameObject.SetActive(false);
            //
        }
        if (buttonClicked == cognitivoButton)
        {
            Debug.Log("COGNITIVO");
            m_Text.text = "Protocollo Cognitivo";

            multiColore.gameObject.SetActive(true);
           

            multiSimbolo.gameObject.SetActive(true);
            symbolManager.setDefaultSymbolIndex(); // DEFAULT

            decisionMaking.gameObject.SetActive(true);
            // 

            strobo.gameObject.SetActive(false);
            // 

            headEyeMovement.gameObject.SetActive(true);
            ResetHEM();

            differenziazione.gameObject.SetActive(true);
            // 
        }
        if (buttonClicked == visionButton)
        {
            Debug.Log("VISION");
            m_Text.text = "Vision Training";

            multiColore.gameObject.SetActive(true);

            multiSimbolo.gameObject.SetActive(false);
            symbolManager.setDefaultSymbolIndex(); //  DEFAULT

            decisionMaking.gameObject.SetActive(false);
            // 

            strobo.gameObject.SetActive(true);
            //

            headEyeMovement.gameObject.SetActive(true);
            ResetHEM();

            differenziazione.gameObject.SetActive(true);
            //

        }
        if (buttonClicked == servizioButton)
        {
            Debug.Log("SERVIZIO");
            m_Text.text = "Risposta al Servizio";

            multiColore.gameObject.SetActive(false);

            multiSimbolo.gameObject.SetActive(false);

            decisionMaking.gameObject.SetActive(true);

            strobo.gameObject.SetActive(true);

            headEyeMovement.gameObject.SetActive(true);
            ResetHEM();

            differenziazione.gameObject.SetActive(false);

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