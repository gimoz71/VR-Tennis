using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using System.Linq;

public class GetMacAddress : MonoBehaviour {

	public Text mac;
	public Button startButton;
	// Use this for initialization
	void Start () {
		var macAddr =
			(
				from nic in NetworkInterface.GetAllNetworkInterfaces()
				where nic.OperationalStatus == OperationalStatus.Up
				select nic.GetPhysicalAddress().ToString()
			).FirstOrDefault();
		

		if (macAddr.Equals( "0023AE83B346")) {
			Debug.Log ("--------------------OK---------------> " + macAddr);
			mac.text = "";
			startButton.interactable = true;
		} else {
			Debug.Log ("--------------------ERROR---------------> " + macAddr);
			mac.text = "Computer non abilitato";
			startButton.interactable = false;
		}
	}
}
