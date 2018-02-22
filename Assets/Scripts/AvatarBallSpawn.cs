using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]



	public class AvatarBallSpawn : MonoBehaviour {
		
		public BallSpawnFire _ballspawnfire;
		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
		}

		// Avvio la funzone SerialFire dalla scena (temporaneo?)
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				_ballspawnfire.SerialFire();
			}
		}
	}
}


