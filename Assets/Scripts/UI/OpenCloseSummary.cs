using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseSummary : MonoBehaviour {

    public GameObject summaryPanel;

	// Use this for initialization
	void Start () {
        //summaryPanel = GameObject.Find("Sommario");
        summaryPanel.active = false;
    }
	
    public void OpenSommario()
    {
        summaryPanel.active = true;
    }

    public void CloseSommario()
    {
        summaryPanel.active = false;
    }
}
