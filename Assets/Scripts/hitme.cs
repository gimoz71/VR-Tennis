using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Valve.VR.InteractionSystem {

    public class hitme : MonoBehaviour {

        private SpeedManager speedManager;

        private GameObject physParent;
        public AudioSource RacketHit;
        public BatCapsuleFollower bcf;

        public Text velocita;

        // Use this for initialization
        void Start () {
            physParent = GameObject.Find("racket");
            AudioSource source = GetComponent<AudioSource>();
            bcf = GameObject.Find("Racket Follower(Clone)").GetComponent<BatCapsuleFollower>();
            velocita = GameObject.Find("Velocita").GetComponent<Text>();
        }

		// Update is called once per frame
		void Update () {
		}

		void OnTriggerEnter(Collider other)
		{
            if(other.name == "racket")
            {

                float speed = bcf._speed;
                string key = BatCapsuleFollower.GetSpeedKey(speed);

                speedManager = SpeedManager.Instance;
                //speedManager.stampaMappa();

                velocita.text = "velocità momentanea: " + speed;
                if (speedManager.MappVelocita[key] == 1)
                {
                    //Debug.Log("ERRORE STESSA VELOCITA' PRECEDENTE");
                    velocita.text += " ERRORE STESSA VELOCITA': " + key;
                } else
                {
                    velocita.text += " VELOCITA': " + key;
                }


                speedManager.ResetCorrectTrigger();
                speedManager.MappVelocita[key] = 1;

                //speedManager.stampaMappa();
                Pulse();
            }
		}

		private void Pulse( )
		{
			Hand hand = physParent.GetComponentInParent<Hand>();

			if ( hand != null )
			{
				hand.controller.TriggerHapticPulse(3000);
                RacketHit.Play();
            }

		}
	}
}
