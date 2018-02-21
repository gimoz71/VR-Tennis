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
    public GameObject iconBG;
    public Button baseButton, cognitivoButton, visionButton, servizioButton;

    public GameObject baseiconBG, cognitivoiconBG, visioniconBG, servizioiconBG;
    public GameObject multiColore, multiSimbolo, decisionMaking, strobo, headEyeMovement, differenziazione;
   

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

        m_Text.text = "Protocollo Base";
        multiColore.gameObject.SetActive(false);
        multiSimbolo.gameObject.SetActive(false);
        decisionMaking.gameObject.SetActive(false);
        strobo.gameObject.SetActive(false);
        headEyeMovement.gameObject.SetActive(false);
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

        //My Code
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
            decisionMaking.gameObject.SetActive(false);
            strobo.gameObject.SetActive(false);
            headEyeMovement.gameObject.SetActive(false);
            differenziazione.gameObject.SetActive(false);
        }
        if (buttonClicked == cognitivoButton)
        {
            Debug.Log("COGNITIVO");
            m_Text.text = "Protocollo Cognitivo";
            multiColore.gameObject.SetActive(true);
            multiSimbolo.gameObject.SetActive(true);
            decisionMaking.gameObject.SetActive(true);
            strobo.gameObject.SetActive(false);
            headEyeMovement.gameObject.SetActive(true);
            differenziazione.gameObject.SetActive(true);
        }
        if (buttonClicked == visionButton)
        {
            Debug.Log("VISION");
            m_Text.text = "Vision Training";
            multiColore.gameObject.SetActive(true);
            multiSimbolo.gameObject.SetActive(false);
            decisionMaking.gameObject.SetActive(false);
            strobo.gameObject.SetActive(true);
            headEyeMovement.gameObject.SetActive(true);
            differenziazione.gameObject.SetActive(true);
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
}