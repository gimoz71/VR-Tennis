using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectArea : MonoBehaviour {


    public Text myText;
    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider other)
    {
       // Debug.Log("Collided with " + other.gameObject.name);
       if (other.gameObject.name == "AreaAnterioreDX")
        {
            Debug.Log("AreaAnterioreDX");
            myText.text = "AreaAnterioreDX";
        }
        if (other.gameObject.name == "AreaAnterioreSX")
        {
            Debug.Log("AreaAnterioreSX");
            myText.text = "AreaAnterioreSX";
        }
        if (other.gameObject.name == "AreaPosterioreDX")
        {
            Debug.Log("AreaPosterioreDX");
            myText.text = "AreaPosterioreDX";
        }
        if (other.gameObject.name == "AreaPosterioreDX")
        {
            Debug.Log("AreaPosterioreDX");
            myText.text = "AreaPosterioreDX";
        }
        if (other.gameObject.name == "CollisioneRete")
        {
            Debug.Log("CollisioneRete");
            myText.text = "CollisioneRete";
        }
    }
}
