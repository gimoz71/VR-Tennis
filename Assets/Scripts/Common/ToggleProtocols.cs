using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleProtocols : MonoBehaviour
{
    public Text m_Text;

    public Toggle ProtocolloBase;
    public Toggle ProtocolloCognitivo;
    public Toggle ProtocolloVision;
    public Toggle ProtocolloServizio;

    public GameObject multiColore;
    public GameObject multiSimbolo;
    public GameObject decisionMaking;
    public GameObject strobo;
    public GameObject headEyeMovement;
    public GameObject differenziazione;

    void Start()
    {


        ProtocolloBase.onValueChanged.AddListener((bool on) => {
            if (on) {
                m_Text.text = "Protocollo Base";
                multiColore.gameObject.SetActive(false);
                multiSimbolo.gameObject.SetActive(false);
                decisionMaking.gameObject.SetActive(false);
                strobo.gameObject.SetActive(false);
                headEyeMovement.gameObject.SetActive(false);
                differenziazione.gameObject.SetActive(false);
            } 
        });
        //Toggle pc = ProtocolloCognitivo.GetComponent<Toggle>();
        ProtocolloCognitivo.onValueChanged.AddListener((bool on) => {
            if (on)
            {
                m_Text.text = "Protocollo Cognitivo";
                multiColore.gameObject.SetActive(true);
                multiSimbolo.gameObject.SetActive(true);
                decisionMaking.gameObject.SetActive(true);
                strobo.gameObject.SetActive(false);
                headEyeMovement.gameObject.SetActive(true);
                differenziazione.gameObject.SetActive(true);
            }
        });
        //Toggle pv = ProtocolloBase.GetComponent<Toggle>();
        ProtocolloVision.onValueChanged.AddListener((bool on) => {
            if (on)
            {
                m_Text.text = "Vision Trainig";
                multiColore.gameObject.SetActive(true);
                multiSimbolo.gameObject.SetActive(false);
                decisionMaking.gameObject.SetActive(false);
                strobo.gameObject.SetActive(true);
                headEyeMovement.gameObject.SetActive(true);
                differenziazione.gameObject.SetActive(true);
            }
        });
        //Toggle ps = ProtocolloServizio.GetComponent<Toggle>();
        ProtocolloServizio.onValueChanged.AddListener((bool on) => {
            if (on)
            {
                m_Text.text = "Risposta al Servizio";
                multiColore.gameObject.SetActive(false);
                multiSimbolo.gameObject.SetActive(false);
                decisionMaking.gameObject.SetActive(true);
                strobo.gameObject.SetActive(true);
                headEyeMovement.gameObject.SetActive(true);
                differenziazione.gameObject.SetActive(false);
            }
        });

       
    }

    


   
}