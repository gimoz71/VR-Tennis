using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {



	public class hitme : MonoBehaviour {

		private GameObject physParent;

        
		// Use this for initialization
		void Start () {
            physParent = GameObject.Find("racket");
		}

		// Update is called once per frame
		void Update () {
		}

		void OnTriggerEnter(Collider other)
		{
            if(other.name == "racket")
            {

                Pulse();
            }
		}

		private void Pulse( )
		{
			Hand hand = physParent.GetComponentInParent<Hand>();

			if ( hand != null )
			{
				hand.controller.TriggerHapticPulse(3000);
				//Debug.Log("PUM!!!!!!!");
			}

		}
	}
}
