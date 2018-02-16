using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {



	public class hitme : MonoBehaviour {

		private GameObject physParent;
        //public GameObject impactForce;


        // Use this for initialization
        void Start () {
            physParent = GameObject.Find("racket");
            //impactForce = GameObject.Find("Racket Follower");
		}

		// Update is called once per frame
		void Update () {
		}

		void OnTriggerEnter(Collider other)
		{
            if(other.name == "racket")
            {
             
                //impactForce.GetComponent<BatCapsuleFollower>
                //Pulse();
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
