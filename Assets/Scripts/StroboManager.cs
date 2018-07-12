using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroboManager : MonoBehaviour {

    public Button ButtonAcceso;
    public Text ButtonAccessoText;
    public Toggle ToggleLenteSX;
    public Toggle ToggleLenteDX;
    public Toggle ToggleAlternanzaLenti;
    public Slider SliderFrequenza;
    public Slider SliderBuio;
    public Slider SliderTrasparenza;

    public GameObject LenteSX;
    public GameObject LenteDX;

    public CanvasGroup CanvasSwitch;
    
    private Material lenteSXMeshRendererAlpha;
    private Material lenteDXMeshRendererAlpha;

    public bool switchButton;

    private ScoreManager scoreManager;

    private static StroboManager instance;

    public bool started;

    private StroboManager() { }

    public static StroboManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new StroboManager();
            }

            return instance;
        }
    }

    public void Start() {
        instance.switchButton = false;
        instance.started = false;


        scoreManager = ScoreManager.Instance;
    }

    public void Init()
    {
        instance.started = true;
        ButtonAccessoText.text = "OFF";
        lenteSXMeshRendererAlpha = LenteSX.GetComponent<MeshRenderer>().material;
        lenteDXMeshRendererAlpha = LenteDX.GetComponent<MeshRenderer>().material;

        lenteSXMeshRendererAlpha.color = new Color(0,0,0, (SliderTrasparenza.value / 100));
        lenteDXMeshRendererAlpha.color = new Color(0, 0, 0, (SliderTrasparenza.value / 100));
        CanvasSwitch.interactable = false;

        instance.SliderFrequenza = GameObject.Find("SliderFrequenza").GetComponent<Slider>();
        instance.SliderTrasparenza = GameObject.Find("SliderTrasparenza").GetComponent<Slider>();
        instance.SliderBuio = GameObject.Find("SliderBuio").GetComponent<Slider>();
        instance.ToggleLenteSX = GameObject.Find("LenteSX").GetComponent<Toggle>();
        instance.ToggleLenteDX = GameObject.Find("LenteDX").GetComponent<Toggle>();

        instance.ToggleAlternanzaLenti = GameObject.Find("AltLenti").GetComponent<Toggle>();
    }

    public void Update() {
        if (instance.started) {
            lenteSXMeshRendererAlpha.color = new Color(0, 0, 0, (SliderTrasparenza.value / 100));
            lenteDXMeshRendererAlpha.color = new Color(0, 0, 0, (SliderTrasparenza.value / 100));
        }
        
    }

    public void ButtonSwitch()
    {
        if (!switchButton)
        {
            StartStrobo();
        }
        else
        {
            StopStrobo();
            instance.started = false;
        }
    }

    public void StartStrobo()
    {
        StartCoroutine(Strobe());
        CanvasSwitch.interactable = true;
        ButtonAccessoText.text = "ON";
        switchButton = true;
        scoreManager.PartitaTemporanea.FrequenzaStrobo = (int)SliderFrequenza.value;
        if (!ToggleLenteSX.isOn)
        {
            scoreManager.PartitaTemporanea.LenteChiusa = "SX";
        }
        else if (!ToggleLenteDX.isOn)
        {
            scoreManager.PartitaTemporanea.LenteChiusa = "DX";
        }
        else if (!ToggleLenteDX.isOn && !ToggleLenteSX.isOn)
        {
            scoreManager.PartitaTemporanea.LenteChiusa = "DX SX";
        }
    }

    public float getFrequenzaStrobo() {
        if (instance.started)
        {
            return instance.SliderFrequenza.value;
        }
        else {
            return -1;
        }
        
    }

    public string getPercentualeBuio() {
        if (instance.started)
        {
            return ((instance.SliderBuio.value * 100f) / (instance.SliderFrequenza.value + instance.SliderBuio.value)) + "%";
        }
        else {
            return "";
        }
    }

    public string getLenteChiusa() {
        if (instance.started)
        {
            if (!instance.ToggleLenteSX.isOn)
            {
                return "SX";
            }
            else if (!instance.ToggleLenteDX.isOn)
            {
                return "DX";
            }
            else if (!instance.ToggleLenteDX.isOn && !instance.ToggleLenteSX.isOn)
            {
                return "DX SX";
            }
            else {
                return "";
            }
        }
        else {
            return "";
        }
    }

    public string getAlternanzaLenti() {
        if (instance.started)
        {
            if (instance.ToggleAlternanzaLenti.isOn)
            {
                return "Si";
            }
            else
            {
                return "";
            }
        }
        else {
            return "";
        }
    }

    public void StopStrobo()
    {
        StopAllCoroutines();
        LenteSX.SetActive(false);
        LenteDX.SetActive(false);
        CanvasSwitch.interactable = false;
        ButtonAccessoText.text = "OFF";
        switchButton = false;
        scoreManager.PartitaTemporanea.FrequenzaStrobo = -1;
        scoreManager.PartitaTemporanea.LenteChiusa = "";
    }

    public IEnumerator Strobe()
    {
        while (true) {
            float ritardo = 0f;
            if (LenteSX.activeSelf) {//il controllo lo faccio solo su una lente perchè sono sincronizzate
                ritardo = SliderFrequenza.value + SliderBuio.value;
            } else {
                ritardo = SliderFrequenza.value;
            }
            yield return new WaitForSeconds(ritardo);
            if (ToggleLenteSX.isOn)
            {  
                if (LenteSX.activeSelf)
                {
                    LenteSX.SetActive(false);
                }
                else
                {
                    LenteSX.SetActive(true);
                }
            }
            else
            {
                LenteSX.SetActive(true);
            }

            if (ToggleLenteDX.isOn)
            {

                if (LenteDX.activeSelf)
                {
                    LenteDX.SetActive(false);
                }
                else
                {
                    LenteDX.SetActive(true);

                }
            }
            else
            {
                LenteDX.SetActive(true);
            }
        }
    }

    public void AltLenti()
    {
        if (ToggleAlternanzaLenti.isOn)
        {
            if (LenteSX.activeSelf)
            {
                LenteSX.SetActive(false);

            }
            else
            {
                LenteSX.SetActive(true);
            }
            scoreManager.PartitaTemporanea.Alternanza = "Si";
        } else
        {
            LenteSX.SetActive(true);
            LenteDX.SetActive(true);
            scoreManager.PartitaTemporanea.Alternanza = "";
        }
    }
        
}