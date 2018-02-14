using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]

	public class BallSpawnFire : MonoBehaviour {
		
		public GameObject Prefab;
		public Transform playerTransform;
		public Transform target;
		public Transform source;
		public int pulseForce;
		public GameObject GetActiveToggle;

		public int interval = 1;
		public int quantity = 5;
		public int delay = 1;

		public ToggleDifficulty _ToggleDifficultyScript;

		void Start () {
		}

		// Update is called once per frame
		void Update () {
			
		}

		public void SerialFire() {
			StartCoroutine (sequenzaLancio(interval, quantity, delay));
		}
		// spara palline con il controller del Vive
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				_ToggleDifficultyScript.ActiveToggle ();
				fire();
			}
		}


		// spara palline
		public void fire()
		{
			GameObject tennisBall=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
			tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);
			Destroy (tennisBall, 15);
		}


		// loop
		public IEnumerator sequenzaLancio(float interval, int count, int delay) {
			for (int i = 0; i < count; i++) {

				_ToggleDifficultyScript.ActiveToggle ();
				yield return new WaitForSeconds (delay);
				fire();
			}
		}
	}
}

