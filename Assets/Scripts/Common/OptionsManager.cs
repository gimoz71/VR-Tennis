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
    public Button MCButton;
    public Button MSButton;
    public Button DIFFButton;
    public Button DMButton;

    [Header("Background Pulsante Protocolli)")]
    public Text MCtext;
    public Text MSText;
    public Text DIFFText;
    public Text DMText;

    [Header("Lista Pannelli Toggle Group Opzioni")]
    public GameObject multiColoreToggleGroup;
    public GameObject multiSimboloToggleGroup;
    public GameObject differenziazioneToggleGroup;
    public GameObject decisionMakingToggleGroup;


    // inizializzo le variabili dei manager 
    private BallTextureManager balltextureManager;
    private AreasManager areasManager;

    // Genero L'hashtable dei pulsanti
    public Dictionary<Button, Text> MapOpzioni = new Dictionary<Button, Text>();

    void pairButtonIcon()
    {
        MapOpzioni.Add(MCButton, MCtext);
        MapOpzioni.Add(MSButton, MSText);
        MapOpzioni.Add(DIFFButton, DIFFText);
        MapOpzioni.Add(DMButton, DMText);

    }

    void Start()
    {
        balltextureManager = BallTextureManager.Instance;
        areasManager = AreasManager.Instance;

        pairButtonIcon();
        MCtext.GetComponent<Text>().text = "ON";
        MSText.GetComponent<Text>().text = "OFF";
        DIFFText.GetComponent<Text>().text = "OFF";
        DMText.GetComponent<Text>().text = "OFF";

        multiColoreToggleGroup.gameObject.SetActive(true);
        multiSimboloToggleGroup.gameObject.SetActive(false);
        decisionMakingToggleGroup.gameObject.SetActive(false);
        differenziazioneToggleGroup.gameObject.SetActive(false);

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
        if (buttonClicked == MCButton)
        {
            multiColoreToggleGroup.gameObject.SetActive(true);
            multiSimboloToggleGroup.gameObject.SetActive(false);
            differenziazioneToggleGroup.gameObject.SetActive(false);
            decisionMakingToggleGroup.gameObject.SetActive(false);
            balltextureManager.setDefaultTextureIndex(); // DEFAULT
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);

        }
        if (buttonClicked == MSButton)
        {
            multiColoreToggleGroup.gameObject.SetActive(false);
            multiSimboloToggleGroup.gameObject.SetActive(true);
            differenziazioneToggleGroup.gameObject.SetActive(false);
            decisionMakingToggleGroup.gameObject.SetActive(false);
            balltextureManager.setDefaultTextureIndex(); // DEFAULT
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_SIMBOLI);
        }
        if (buttonClicked == DIFFButton)
        {
            multiColoreToggleGroup.gameObject.SetActive(false);
            multiSimboloToggleGroup.gameObject.SetActive(false);
            differenziazioneToggleGroup.gameObject.SetActive(true);
            decisionMakingToggleGroup.gameObject.SetActive(false);
            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);
        }
        if (buttonClicked == DMButton)
        {
            multiColoreToggleGroup.gameObject.SetActive(false);
            multiSimboloToggleGroup.gameObject.SetActive(false);
            differenziazioneToggleGroup.gameObject.SetActive(false);
            decisionMakingToggleGroup.gameObject.SetActive(true);
            balltextureManager.setShutdownTextureIndex(); // SHUTDOWN
            balltextureManager.setMapIndex(BallTextureManager.MAP_INDEX_COLORI);
        }
    }

    void OnEnable()
    {
        MCButton.onClick.AddListener(() => buttonCallBack(MCButton));
        MSButton.onClick.AddListener(() => buttonCallBack(MSButton));
        DIFFButton.onClick.AddListener(() => buttonCallBack(DIFFButton));
        DMButton.onClick.AddListener(() => buttonCallBack(DMButton));
    }

    void OnDisable()
    {

    }

}