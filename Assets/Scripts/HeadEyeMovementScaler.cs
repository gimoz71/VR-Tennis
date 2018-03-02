using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadEyeMovementScaler : MonoBehaviour {

    private Slider sliderScaler;
    private Renderer rendererLeft;
    private Renderer rendererRight;
    private Vector3 originalScaleLeft;
    private Vector3 originalScaleRight;

    // Use this for initialization
    void Start () {
        sliderScaler = GameObject.Find("SliderChiusura").GetComponent<Slider>();
        rendererLeft = GameObject.Find("MaskEyeLeft").GetComponent<Renderer>();
        rendererRight = GameObject.Find("MaskEyeRight").GetComponent<Renderer>();
        originalScaleLeft = rendererRight.transform.localScale;
        originalScaleRight = rendererRight.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        rendererLeft.transform.localScale = originalScaleLeft - new Vector3(sliderScaler.value, sliderScaler.value, sliderScaler.value) / 270;
        rendererRight.transform.localScale = originalScaleRight - new Vector3(sliderScaler.value, sliderScaler.value, sliderScaler.value) / 270;
    }

    public void enableHEM()
    {
        resetHEM();
        rendererLeft.enabled = true;
        rendererRight.enabled = true;
    }

    public void disableHEM()
    {
        resetHEM();
        rendererLeft.enabled = false;
        rendererRight.enabled = false;
    }

    public void resetHEM()
    {
        if (sliderScaler)
        {
            sliderScaler.value = 0;
        }
    }
}
