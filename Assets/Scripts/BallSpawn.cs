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
			//Prefab = GameObject.Find("ThrowableBall");
			//playerTransform = GameObject.Find("BallSpawnPoint").transform;

		}

		// Update is called once per frame
		void Update () {
			
		}

		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				Debug.Log("Pressed left click.");
				Debug.Log("CLICK!");
				GameObject obj=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
			}
		}
	}
}


