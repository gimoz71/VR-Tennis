using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Text m_Text;
    public Color activeColor;
    public Color inactiveColor;

    [Header("Opzione in focus")]
    public GameObject iconBG;

    [Header("Lista Pulsante Opzioni)")]
    public Button TGButton;
    public Button MCButton;
    public Button MSButton;
    public Button DIFFButton;
    public Button DMButton;

    [Header("Background Pulsante Protocolli)")]
    public Text TGText;
    public Text MCText;
    public Text MSText;
    public Text DIFFText;
    public Text DMText;

    [Header("Lista Pannelli Canvas Group Opzioni")]
    public CanvasGroup targetCanvasGroup;
    public CanvasGroup multiColoreCanvasGroup;
    public CanvasGroup multiSimboloCanvasGroup;
    public CanvasGroup differenziazioneCanvasGroup;
    public CanvasGroup decisionMakingCanvasGroup;


    // inizializzo le variabili dei manager 
    private BallTextureManager balltextureManager;
    private AreasManager areasManager;

    // Genero L'hashtable dei pulsanti
    public Dictionary<Button, Text> MapOpzioni = new Dictionary<Button, Text>();

    void pairButtonIcon()
    {
        MapOpzioni.Add(TGButton, TGText);
        MapOpzioni.Add(MCButton, MCText);
        MapOpzioni.Add(MSButton, MSText);
        MapOpzioni.Add(DIFFButton, DIFFText);
        MapOpzioni.Add(DMButton, DMText);

    }

    void Start()
    {
        balltextureManager = BallTextureManager.Instance;
        areasManager = AreasManager.Instance;

        pairButtonIcon();
        TGText.GetComponent<Text>().text = "ON";
        MCText.GetComponent<Text>().text = "OFF";
        MSText.GetComponent<Text>().text = "OFF";
        DIFFText.GetComponent<Text>().text = "OFF";
        DMText.GetComponent<Text>().text = "OFF";

        targetCanvasGroup.interactable = true;
        multiColoreCanvasGroup.interactable = false;
        multiSimboloCanvasGroup.interactable = false;
        decisionMakingCanvasGroup.interactable = false;
        differenziazioneCanvasGroup.interactable = false;

    }

    // Use this for initialization
    void buttonCallBack(Button buttonClicked)
    {

        for (int i = 0; i < MapOpzioni.Count; i++)
        {
            var item = MapOpzioni.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            if (buttonClicked == itemKey)
            {
                itemValue.GetComponent<Text>().text = "ON";
            }
            else
            {
                itemValue.GetComponent<Text>().text = "OFF";
            }
        }
        if (buttonClicked == TGButton)
        {
            targetCanvasGroup.interactable = true;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = false;

            balltextureManager.setDefaultTextureIndex(); // DEFAULT
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);

        }
        if (buttonClicked == MCButton)
        {
            targetCanvasGroup.interactable = false;
            multiColoreCanvasGroup.interactable = true;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = false;

            balltextureManager.setDefaultTextureIndex(); // DEFAULT
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);


        }
        if (buttonClicked == MSButton)
        {
            targetCanvasGroup.interactable = false;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = true;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = false;

            balltextureManager.setDefaultTextureIndex(); // DEFAULT
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_SIMBOLI);
        }
        if (buttonClicked == DIFFButton)
        {
            targetCanvasGroup.interactable = false;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = false;
            differenziazioneCanvasGroup.interactable = true;

            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);
        }
        if (buttonClicked == DMButton)
        {
            targetCanvasGroup.interactable = false;
            multiColoreCanvasGroup.interactable = false;
            multiSimboloCanvasGroup.interactable = false;
            decisionMakingCanvasGroup.interactable = true;
            differenziazioneCanvasGroup.interactable = false;

            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);
        }
    }

    void OnEnable()
    {
        TGButton.onClick.AddListener(() => buttonCallBack(TGButton));
        MCButton.onClick.AddListener(() => buttonCallBack(MCButton));
        MSButton.onClick.AddListener(() => buttonCallBack(MSButton));
        DIFFButton.onClick.AddListener(() => buttonCallBack(DIFFButton));
        DMButton.onClick.AddListener(() => buttonCallBack(DMButton));
    }

    void OnDisable()
    {

    }

}