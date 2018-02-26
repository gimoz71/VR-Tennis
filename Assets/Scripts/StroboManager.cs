using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroboManager : MonoBehaviour {

    public Toggle ToggleLenteSX;
    public Toggle ToggleLenteDX;
    public Toggle ToggleAlternanzaLenti;
    public Slider SliderFrequenza;
    public Slider SliderTrasparenza;

    public GameObject LenteSX;
    public GameObject LenteDX;

    public Color lenteSXMeshRenderer;
    public Color lenteDXMeshRenderer;

    private int counter = 1;
    

    void Start()
    {
        Color lenteSXMeshRenderer = LenteSX.GetComponent<Renderer>().material.color;
        Color lenteDXMeshRenderer = LenteDX.GetComponent<Renderer>().material.color;

    }

    void Update() {

        Debug.Log("TRASPARENZA: " + SliderTrasparenza.value);
        Debug.Log("TRASPARENZA TEXTURE: " + lenteSXMeshRenderer.a);
        lenteSXMeshRenderer.a = SliderTrasparenza.value;
        lenteDXMeshRenderer.a = SliderTrasparenza.value;
    }

    public void StartStrobo()
    {
        StartCoroutine(Strobe());
    }

    public void StopStrobo()
    {
        StopCoroutine(Strobe());
        LenteSX.SetActive(false);
        LenteDX.SetActive(false);
    }

    public IEnumerator Strobe()

    {
        while(true) {

            yield return new WaitForSeconds(SliderFrequenza.value);
            if (ToggleLenteSX.isOn) { 
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
            }
            if (ToggleLenteDX.isOn) {

                if (LenteDX.activeSelf)
                {
                    LenteDX.SetActive(false);
                }
                else
                {
                    LenteDX.SetActive(true);
            
                }
            } else
            {
                LenteDX.SetActive(true);
            }
        }

    }
    void DisappearanceLogic()
    {
        
    }

}
