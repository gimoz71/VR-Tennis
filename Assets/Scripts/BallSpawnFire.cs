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

		/*float x;
		float y;
		float z;
		Vector3 pos;*/

		Vector3 pulse = new Vector3 (3,1,0);

		// Use this for initialization
		void Start () {
		}

		// Update is called once per frame
		void Update () {
			
			if ( Input.GetMouseButtonDown(0) )
			{
				fire();
			}

			/*x = Random.Range(-1, 1);
			y = 1;
			z = Random.Range(-1, 1);
			pos = new Vector3(x, y, z);
			target.position = pos;*/
		}

		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
				fire();
			}
		}

		private void fire()
		{
			GameObject obj=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
			//obj.GetComponent<Rigidbody>().AddForce(pulse * pulseForce);
			obj.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);

		}
	}
}

