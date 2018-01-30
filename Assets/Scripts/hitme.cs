using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {

	public class hitme : MonoBehaviour {

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {
		}

		void OnTriggerEnter(Collider other)
		{
			Pulse();
		}

		private void Pulse()
		{
			Debug.Log("PUM!!!!!!!");
		}
	}
}
