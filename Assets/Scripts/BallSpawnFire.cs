using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*namespace Valve.VR.InteractionSystem {

	[RequireComponent( typeof( Interactable ) )]*/

	public class BallSpawnFire : MonoBehaviour {

        private ColorManager colorManager;
        private SymbolManager symbolManager;

        [Header("Colore Palla (Prefab)")]
        public Texture BaseTexture;
        public Texture BlueTexture;
        public Texture FucsiaTexture;
        public Texture OrangeTexture;
        public Texture RedTexture;

        [Header("Simbolo Palla (Prefab)")]
        public Texture ATexture;
        public Texture BTexture;
        public Texture CTexture;
        public Texture DTexture;
        public Texture ETexture;

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

        private float interval = 4;
        private float quantity = 10;
        private float delay = 4;


        private void Start()
        {
            colorManager = ColorManager.Instance;
            symbolManager = SymbolManager.Instance;

            colorManager.MappColorePalla.Add(ColorManager.COLORE_PALLA_BASE, BaseTexture);
            colorManager.MappColorePalla.Add(ColorManager.COLORE_PALLA_BLUE, BlueTexture);
            colorManager.MappColorePalla.Add(ColorManager.COLORE_PALLA_FUCSIA, FucsiaTexture);
            colorManager.MappColorePalla.Add(ColorManager.COLORE_PALLA_ORANGE, OrangeTexture);
            colorManager.MappColorePalla.Add(ColorManager.COLORE_PALLA_RED, RedTexture);

            symbolManager.MappSimboloPalla.Add(SymbolManager.SIMBOLO_PALLA_A, ATexture);
            symbolManager.MappSimboloPalla.Add(SymbolManager.SIMBOLO_PALLA_B, BTexture);
            symbolManager.MappSimboloPalla.Add(SymbolManager.SIMBOLO_PALLA_C, CTexture);
            symbolManager.MappSimboloPalla.Add(SymbolManager.SIMBOLO_PALLA_D, DTexture);
            symbolManager.MappSimboloPalla.Add(SymbolManager.SIMBOLO_PALLA_E, ETexture);
        }
       

        // Avvio la funzone SerialFire dalla scena (temporaneo?)
        /*private void HandHoverUpdate( Hand hand )
		{
			if ( hand.GetStandardInteractionButtonDown() || ( ( hand.controller != null ) && hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_Grip ) ) )
			{
                SerialFire();
            }
		}*/

        // Lancio palle come da parametri settati negli sliders
        public void SerialFire()
        {
            float interval = IntervalSlider.GetComponent<Slider>().value;
            float quantity = QuantitySlider.GetComponent<Slider>().value;
            float delay = DelaySlider.GetComponent<Slider>().value;

            // avvio la routine di ritardo (che lancia la sequenza lancio)
            StartCoroutine(ritardoLancio(delay, quantity, interval));
        }

        // Funzione parametro di ritardo per il loop base del lancio
        public IEnumerator ritardoLancio(float myDelay, float myQuantity, float myInterval)
        {
            yield return new WaitForSeconds(3);
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
                yield return new WaitForSeconds(0);
                fire();
                yield return new WaitForSeconds (separation);
			}
		}

        // Funziona base lancio palle
        public void fire()
        {

            // Creo L'istanza del prefab "ThrowableBall" (pallina)
            GameObject tennisBall = Instantiate(Prefab, playerTransform.position, Quaternion.identity) as GameObject;
           
            // Lancio l'istanza nella scena in base ai parametri di forza
            tennisBall.GetComponent<Rigidbody>().AddForce((target.position - source.position) * pulseForce);

            // trovo la mesh della pallina (child di ThrowableBall)) e gli assegno una texture random tra quelle definite in ColorManager.cs
            GameObject palla = tennisBall.transform.Find("TennisBall/ball").gameObject;
            Renderer pallaPrefab = palla.GetComponent<Renderer>();
            pallaPrefab.material.mainTexture = colorManager.RandomColor();

            // Distruggo la pallina dopo N secondi
            Destroy(tennisBall, 15);
        }
    }
//}

