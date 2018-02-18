using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour {

	public Text myText;
	public Text myCounter;
	public Text myTotalCounter;

	// Use this for initialization
	void Start () {
		myText = GameObject.Find("Score").GetComponent<Text>();
		myCounter = GameObject.Find("Counter").GetComponent<Text>();
		myTotalCounter = GameObject.Find("TotalCounter").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
