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
    public Slider SliderTrasparenza;

    public GameObject LenteSX;
    public GameObject LenteDX;

    public CanvasGroup CanvasSwitch;


    private Material lenteSXMeshRendererAlpha;
    private Material lenteDXMeshRendererAlpha;

    private bool switchButton = false;


    void Start()
    {
        ButtonAccessoText.text = "OFF";
        lenteSXMeshRendererAlpha = LenteSX.GetComponent<MeshRenderer>().material;
        lenteDXMeshRendererAlpha = LenteDX.GetComponent<MeshRenderer>().material;

        lenteSXMeshRendererAlpha.color = new Color(0,0,0, (SliderTrasparenza.value / 100));
        lenteDXMeshRendererAlpha.color = new Color(0, 0, 0, (SliderTrasparenza.value / 100));
        CanvasSwitch.interactable = false;

    }

    void Update() {
        //lenteSXMeshRendererAlpha.GetColor("_Color");
        //lenteDXMeshRendererAlpha.GetColor("_color");
        //Debug.Log("TRASPARENZA: " + SliderTrasparenza.value);
        //Debug.Log("OGGETTO TRASPARENZA" + LenteSX + " | " + lenteSXMeshRendererAlpha);
        lenteSXMeshRendererAlpha.color = new Color(0, 0, 0, (SliderTrasparenza.value / 100));
        lenteDXMeshRendererAlpha.color = new Color(0, 0, 0, (SliderTrasparenza.value / 100));
    }

    public void ButtonSwitch()
    {
        if (!switchButton)
        {
            StartStrobo();
            //Debug.Log("ON");
            GlobalScore.StroboStatus = "Strobo: attivo";
        }
        else
        {
            StopStrobo();
            //Debug.Log("OFF");
            GlobalScore.StroboStatus = "";
        }
    }

    public void StartStrobo()
    {
        StartCoroutine(Strobe());
        CanvasSwitch.interactable = true;
        ButtonAccessoText.text = "ON";
        switchButton = true;
    }

    public void StopStrobo()
    {
        StopAllCoroutines();
        LenteSX.SetActive(false);
        LenteDX.SetActive(false);
        CanvasSwitch.interactable = false;
        ButtonAccessoText.text = "OFF";
        switchButton = false;
    }

    public IEnumerator Strobe()
    {
        while (true) {
            yield return new WaitForSeconds(SliderFrequenza.value);
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
        } else
        {
            LenteSX.SetActive(true);
            LenteDX.SetActive(true);
        }
    }
        
}