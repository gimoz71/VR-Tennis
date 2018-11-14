using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadEyeMovementScaler : MonoBehaviour {

	public Slider sliderScaler;
	public Renderer rendererLeft;
	public Renderer rendererRight;
	public Vector3 originalScaleLeft;
	public Vector3 originalScaleRight;

	private ScoreManager scoreManager;

	private static HeadEyeMovementScaler instance;

	public bool startedHEM;

	private HeadEyeMovementScaler() { }

	public static HeadEyeMovementScaler Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new HeadEyeMovementScaler();
			}

			return instance;
		}
	}

    // Use this for initialization

	public void Start() {
		instance.startedHEM = false;
	}

    public void Init () {
        instance.startedHEM = true;
        instance.resetHEM();
		instance.sliderScaler = GameObject.Find("SliderChiusura").GetComponent<Slider>();
        instance.rendererLeft = GameObject.Find("MaskEyeLeft").GetComponent<Renderer>();
        instance.rendererRight = GameObject.Find("MaskEyeRight").GetComponent<Renderer>();
		instance.originalScaleLeft = rendererRight.transform.localScale;
		instance.originalScaleRight = rendererRight.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		if (instance.startedHEM) {
            instance.rendererLeft.transform.localScale = instance.originalScaleLeft - new Vector3 (instance.sliderScaler.value, instance.sliderScaler.value, instance.sliderScaler.value) / 270;
            instance.rendererRight.transform.localScale = instance.originalScaleRight - new Vector3 (instance.sliderScaler.value, instance.sliderScaler.value, instance.sliderScaler.value) / 270;
		}
    }

    public void enableHEM()
    {
		if (instance.startedHEM) {
			instance.resetHEM ();
            instance.rendererLeft.enabled = true;
            instance.rendererRight.enabled = true;
		}
    }

    public void disableHEM()
    {
		if (instance.startedHEM) {
			instance.resetHEM ();
            instance.rendererLeft.enabled = false;
            instance.rendererRight.enabled = false;
            instance.rendererLeft.transform.localScale = instance.originalScaleLeft - new Vector3(instance.sliderScaler.value, instance.sliderScaler.value, instance.sliderScaler.value) / 270;
            instance.rendererRight.transform.localScale = instance.originalScaleRight - new Vector3(instance.sliderScaler.value, instance.sliderScaler.value, instance.sliderScaler.value) / 270;
        }
    }

    public void resetHEM()
    {
        if (sliderScaler)
        {
			instance.sliderScaler.value = 0;
            instance.rendererLeft.transform.localScale = instance.originalScaleLeft - new Vector3(instance.sliderScaler.value, instance.sliderScaler.value, instance.sliderScaler.value) / 270;
            instance.rendererRight.transform.localScale = instance.originalScaleRight - new Vector3(instance.sliderScaler.value, instance.sliderScaler.value, instance.sliderScaler.value) / 270;
        }
    }

	public int getSliderScaler() {
		if (instance.startedHEM) {
			return (int)instance.sliderScaler.value;
		} else {
			return 0;
		}
	}
}
