using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]

	public class BallSpawn : MonoBehaviour {
		
		public GameObject Prefab;
		public Transform playerTransform;

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
		}

		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				GameObject obj=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
				hand.controller.TriggerHapticPulse(2500);
			}
		}
	}
}


