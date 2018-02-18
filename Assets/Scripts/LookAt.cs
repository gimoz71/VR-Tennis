using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// molto meglio di lookAt
		transform.right = (target.position - transform.position);
		//transform.LookAt(target);
	}
}