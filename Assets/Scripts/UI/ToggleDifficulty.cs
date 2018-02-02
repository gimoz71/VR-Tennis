using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleDifficulty : MonoBehaviour {

	public GameObject _target;

	public Toggle easy;
	public Toggle medium;
	public Toggle hard;
	// Use this for initialization

	void Start() {
		
	}
	public void ActiveToggle () {
		if (easy.isOn) {
			_target.transform.position = GameObject.Find("area1lento").transform.position;
			Debug.Log("EASY");

		} else if (medium.isOn) {
			_target.transform.position = GameObject.Find("area1medio").transform.position;
			Debug.Log("MEDIUM");
		} else if (hard.isOn) {
			_target.transform.position = GameObject.Find("area1veloce").transform.position;
			Debug.Log("HARD");
		}
	}

	public void OnSubmit () {
		Debug.Log("Selected difficulty:");

		ActiveToggle();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
 