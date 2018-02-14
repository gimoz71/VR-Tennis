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

		public int delay = 1;
		public int times = 5;

		public ToggleDifficulty _ToggleDifficulty;

		void Start () {
		}

		// Update is called once per frame
		void Update () {
			
		}

		public void SerialFire() {
			StartCoroutine (MyCounter(delay, times));
		}
		// spara palline con il controller del Vive
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				fire();
			}
		}


		// spara palline
		public void fire()
		{
			GameObject tennisBall=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
			tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);
			Destroy (tennisBall, 10);
		}

		public IEnumerator MyCounter(float interval, int count) {
			for (int i = 0; i < count; i++) {
				yield return new WaitForSeconds (1);
				_ToggleDifficulty.ActiveToggle ();
				fire();
			}
		}
	}
}

