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

    [Header("Lista Pannelli Opzioni")]
    public GameObject multiColore;
    public GameObject multiSimbolo;
    public GameObject differenziazione;
    public GameObject decisionMaking;

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
        pairButtonIcon();
        MCtext.GetComponent<Text>().text = "ON";
        MSText.GetComponent<Text>().text = "OFF";
        DIFFText.GetComponent<Text>().text = "OFF";
        DMText.GetComponent<Text>().text = "OFF";

        multiColore.gameObject.SetActive(true);
        multiSimbolo.gameObject.SetActive(false);
        decisionMaking.gameObject.SetActive(false);
        differenziazione.gameObject.SetActive(false);

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
            multiColore.gameObject.SetActive(true);
            multiSimbolo.gameObject.SetActive(false);
            decisionMaking.gameObject.SetActive(false);
            differenziazione.gameObject.SetActive(false);
        }
        if (buttonClicked == MSButton)
        {
            multiColore.gameObject.SetActive(false);
            multiSimbolo.gameObject.SetActive(true);
            decisionMaking.gameObject.SetActive(false);
            differenziazione.gameObject.SetActive(false);
        }
        if (buttonClicked == DIFFButton)
        {
            multiColore.gameObject.SetActive(false);
            multiSimbolo.gameObject.SetActive(false);
            decisionMaking.gameObject.SetActive(true);
            differenziazione.gameObject.SetActive(false);
        }
        if (buttonClicked == DMButton)
        {
            multiColore.gameObject.SetActive(false);
            multiSimbolo.gameObject.SetActive(false);
            decisionMaking.gameObject.SetActive(false);
            differenziazione.gameObject.SetActive(true);
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