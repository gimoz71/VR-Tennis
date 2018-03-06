using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class hitme : MonoBehaviour {

    private SpeedManager speedManager;

    private GameObject physParent;
    public AudioSource RacketHit;
    private BatCapsuleFollower bcf;

    private GameObject racketCenter;

    private Text velocita;
    private Text distanceText;

    private float speed;
    private Hand hand;

    // Use this for initialization
    void Start () {
            
        AudioSource source = GetComponent<AudioSource>();
		if (GameObject.Find ("racket") != null) {
			physParent = GameObject.Find("racket");
			bcf = GameObject.Find ("Racket Follower(Clone)").GetComponent<BatCapsuleFollower> ();
			velocita = GameObject.Find("Velocita").GetComponent<Text>();
            racketCenter = GameObject.Find("racketCenter");
            distanceText = GameObject.Find("distanceText").GetComponent<Text>();
            hand = physParent.GetComponentInParent<Hand>();
        }
            
    }

    // Registro eventi alla collisione con la racchetta
	void OnTriggerEnter(Collider other)
	{
        if(other.name == "racket")
        {
            // calcolo la distanza della palla dal centro della racchetta all'impatto
            float dist = Vector3.Distance(transform.position, racketCenter.transform.position);
            //Debug.Log(string.Format("La distanza tra {0} and {1} è: {2}", transform.position, racketCenter.transform.position, dist));
            distanceText.text = dist.ToString();


            speed = bcf._speed;
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

            speedManager.ResetVelocityTrigger();
            speedManager.MappVelocita[key] = 1;

            //speedManager.stampaMappa();
            Pulse();
        }
	}

    // Setto la potenza dell'Haptic sul controller in base alla velocità di impatto

	private void Pulse( )
	{
		if ( hand != null )
		{
            RumbleController(speed/350, speed*500);
			//hand.controller.TriggerHapticPulse(3000);
            RacketHit.Play();
        }

	}

    void RumbleController(float duration, float strength)
    {
        StartCoroutine(RumbleControllerRoutine(duration, strength));
    }

    IEnumerator RumbleControllerRoutine(float duration, float strength)
    {
        strength = Mathf.Clamp01(strength);
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime <= duration)
        {
            int valveStrength = Mathf.RoundToInt(Mathf.Lerp(0, 3999, strength));

            hand.controller.TriggerHapticPulse((ushort)valveStrength);

            yield return null;
        }
    }

}