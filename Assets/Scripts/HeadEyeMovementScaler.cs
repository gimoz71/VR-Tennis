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
		instance.sliderScaler = GameObject.Find("SliderChiusura").GetComponent<Slider>();
        //rendererLeft = GameObject.Find("MaskEyeLeft").GetComponent<Renderer>();
        //rendererRight = GameObject.Find("MaskEyeRight").GetComponent<Renderer>();
		//instance.originalScaleLeft = rendererRight.transform.localScale;
		//instance.originalScaleRight = rendererRight.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
		if (instance.startedHEM) {
			//rendererLeft.transform.localScale = originalScaleLeft - new Vector3 (sliderScaler.value, sliderScaler.value, sliderScaler.value) / 270;
			//rendererRight.transform.localScale = originalScaleRight - new Vector3 (sliderScaler.value, sliderScaler.value, sliderScaler.value) / 270;
		}
    }

    public void enableHEM()
    {
		if (instance.startedHEM) {
			instance.resetHEM ();
			//rendererLeft.enabled = true;
			//rendererRight.enabled = true;
		}
    }

    public void disableHEM()
    {
		if (instance.startedHEM) {
			instance.resetHEM ();
			//rendererLeft.enabled = false;
			//rendererRight.enabled = false;
		}
    }

    public void resetHEM()
    {
        if (sliderScaler)
        {
			instance.sliderScaler.value = 0;
        }
    }

	public int getSliderScaler() {
		if (instance.startedHEM) {
			return (int)instance.sliderScaler.value;
		} else {
			return (int)instance.sliderScaler.value;
		}
	}
}
