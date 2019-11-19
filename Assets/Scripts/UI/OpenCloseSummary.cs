using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseSummary : MonoBehaviour {

    public GameObject sommarioUI;

	// Use this for initialization
	void Start () {
        // summaryPanel = GameObject.Find("Sommario");
        // sommarioUI.active = false;
    }
	
    public void OpenSommario()
    {
        sommarioUI.active = true;
    }

    public void CloseSommario()
    {
        sommarioUI.active = false;
    }
}
