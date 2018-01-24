using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngularDrag : MonoBehaviour {

	public Rigidbody rb;
	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	void Update() {
		if (Input.GetKeyDown("space"))
			rb.angularDrag = 0.8F;
		else
			rb.angularDrag = 0;
	}
}
