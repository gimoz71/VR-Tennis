using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadEyeMovementScaler : MonoBehaviour {

    public Slider SliderScaler;

   
    private Vector3 originalScale;

	// Use this for initialization
	void Start () {

        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = originalScale - new Vector3(SliderScaler.value, SliderScaler.value, SliderScaler.value)/300;
    }
}
