using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour {
	
	public GameObject Prefab;
	public Transform playerTransform;


	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}
	void onMouseDown () {
		Debug.Log("CLICK!");
		GameObject obj=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
	}
}
