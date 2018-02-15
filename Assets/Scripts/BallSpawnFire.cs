using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]

	public class BallSpawnFire : MonoBehaviour {
		
		public GameObject Prefab;
		public Transform playerTransform;
		public Transform target;
		public Transform source;
		public int pulseForce;
		public GameObject GetActiveToggle;

        public GameObject IntervalSlider;
        public GameObject QuantitySlider;
        public GameObject DelaySlider;

        private float interval;
        private float quantity;
        private float delay;

		public ToggleDifficulty _ToggleDifficultyScript;
        

		void Start () {
           
        }

		// Update is called once per frame
		void Update () {
			
		}

		public void SerialFire() {
            float interval = IntervalSlider.GetComponent<Slider>().value;
            float quantity = QuantitySlider.GetComponent<Slider>().value;
            float delay = DelaySlider.GetComponent<Slider>().value;

            StartCoroutine(ritardoLancio(delay, quantity, interval));
		}
		// spara palline con il controller del Vive
		private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
                float interval = IntervalSlider.GetComponent<Slider>().value;
                float quantity = QuantitySlider.GetComponent<Slider>().value;
                float delay = DelaySlider.GetComponent<Slider>().value;

                StartCoroutine(ritardoLancio(delay, quantity, interval));
            }
		}


		// spara palline
		public void fire()
		{
            

            GameObject tennisBall=Instantiate(Prefab,playerTransform.position, Quaternion.identity) as GameObject;
			tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);
			Destroy (tennisBall, 15);
		}


        public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
        {
            yield return new WaitForSeconds(myDelay);
            Debug.Log(myInterval);
            Debug.Log(myQuantity);
            Debug.Log(myDelay);
            yield return StartCoroutine(sequenzaLancio(myQuantity, myInterval));
        }

		// loop
		public IEnumerator sequenzaLancio(float count, float separation) {
			for (int i = 0; i < count; i++) {

				_ToggleDifficultyScript.ActiveToggle ();
                fire();
                yield return new WaitForSeconds (separation);
			}
		}
	}
}

