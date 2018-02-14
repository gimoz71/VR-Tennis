using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {



	public class hitme : MonoBehaviour {

		public GameObject physParent;
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

		private void Pulse( )
		{
			Hand hand = physParent.GetComponentInParent<Hand>();
			if ( hand != null )
			{
				hand.controller.TriggerHapticPulse( 500 );
				Debug.Log("PUM!!!!!!!");
			}

		}
	}
}
