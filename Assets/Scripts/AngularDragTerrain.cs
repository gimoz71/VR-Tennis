using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularDragTerrain : MonoBehaviour {

	// Use this for initialization

	void Start () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name == "Plane")
		{
			Debug.Log("OK");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
