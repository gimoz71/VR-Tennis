using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]

	public class BallSpawn : MonoBehaviour {

        [Header("Palla (Prefab)")]
        public GameObject Prefab;

        /*[Header("Punto di creazione palla")]*/
        private Transform ballSpawnPoint;

		// Use this for initialization
		void Start () {
			ballSpawnPoint = GameObject.Find ("BallSpawnPoint").GetComponent<Transform>();
		}

		// Update is called once per frame
		void Update () {
		}

		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				GameObject tennisBall=Instantiate(Prefab, ballSpawnPoint.position, Quaternion.identity) as GameObject;
				Destroy (tennisBall, 15);
			}
		}
	}
}


