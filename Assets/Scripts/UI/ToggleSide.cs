using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSide : MonoBehaviour {


    private Transform launcherTarget;
    private Transform latoSX;
    private Transform latoDX;
    private bool mancino;

    // Use this for initialization
    void Start () {
        mancino = false;
        launcherTarget = GameObject.Find("[LAUNCHER TARGET]").GetComponent<Transform>();
        latoDX = GameObject.Find("latoDX").GetComponent<Transform>();
        latoSX = GameObject.Find("latoSX").GetComponent<Transform>();
        mancino = false;
        launcherTarget.transform.position = latoDX.transform.position;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void mancinoToggle()
    {
        if (!mancino)
        {
            mancino = true;
            launcherTarget.transform.position = latoSX.transform.position;
        } else
        {
            mancino = false;
            launcherTarget.transform.position = latoDX.transform.position;
        }
    }
}
