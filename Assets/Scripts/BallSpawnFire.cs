using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]

	public class BallSpawnFire : MonoBehaviour {

        [Header("Palla (Prefab)")]
        public GameObject Prefab;

        [Header("Origine lancio")]
        public Transform playerTransform;
        
        [Header("Sorgente Lancio")]
        public Transform source;

        [Header("Target Lancio")]
        public Transform target;

        [Header("Modificatore di potenza")]
        public int pulseForce;

        [Header("Selettore difficoltà (UI)")]
        public GameObject GetActiveToggle;

        [Header("Modificatori di sequenza lancio (UI)")]
        public GameObject IntervalSlider;
        public GameObject QuantitySlider;
        public GameObject DelaySlider;

        [Header("Aggancio difficoltà (trigger ActiveToggle)")]
        public ToggleDifficulty _ToggleDifficultyScript;

        private float interval;
        private float quantity;
        private float delay;

       

        // Avvio la funzone SerialFire dalla scena (temporaneo?)
        private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
                SerialFire();
                /*float interval = IntervalSlider.GetComponent<Slider>().value;
                float quantity = QuantitySlider.GetComponent<Slider>().value;
                float delay = DelaySlider.GetComponent<Slider>().value;

                StartCoroutine(ritardoLancio(delay, quantity, interval));*/
            }
		}

        // Lancio palle come da parametri settati negli sliders
        public void SerialFire()
        {
            float interval = IntervalSlider.GetComponent<Slider>().value;
            float quantity = QuantitySlider.GetComponent<Slider>().value;
            float delay = DelaySlider.GetComponent<Slider>().value;

            StartCoroutine(ritardoLancio(delay, quantity, interval));
        }

        // Funzione parametro di ritardo per il loop base del lancio
        public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
        {
            yield return new WaitForSeconds(myDelay);
            Debug.Log(myInterval);
            Debug.Log(myQuantity);
            Debug.Log(myDelay);
            yield return StartCoroutine(sequenzaLancio(myQuantity, myInterval));
        }

		// loop base del lancio
		public IEnumerator sequenzaLancio(float count, float separation) {
			for (int i = 0; i < count; i++) {
				_ToggleDifficultyScript.ActiveToggle ();
                fire();
                yield return new WaitForSeconds (separation);
			}
		}

        // Funziona base lancio palle
        public void fire()
        {

            GameObject tennisBall = Instantiate(Prefab, playerTransform.position, Quaternion.identity) as GameObject;
            tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);
            Destroy(tennisBall, 15);
        }
    }
}

