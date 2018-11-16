using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DetectAngleRacket : MonoBehaviour {

    // setto l'offset sull'asse Y del collisore della racchetta in base alla rotazione del controller: se supera un certo angolo rendo speculare la posizione del collisore stesso

    public GameObject racketController; // il controller da monitorare l'angolo (in pratica la rotazione sull'asse Y della racchetta rispetto all'asse principale del campo
    public LookAtConstraint collisoreRacket; // il collisore da specchiare

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (racketController.transform.rotation.eulerAngles.y <= 270 && racketController.transform.rotation.eulerAngles.y >= 90)
        {
            SetTransformAxis(180.0f);
            //Debug.Log("RacketY=" + racketController.transform.rotation.eulerAngles.y + "| sotto 270");
        }
        else
        {
            SetTransformAxis(0f);
            //Debug.Log("RacketY=" + racketController.transform.rotation.eulerAngles.y + "oltre 270");
        }

    }

    void SetTransformAxis(float n) // funzione per settare l'asse X
    {
        collisoreRacket.rotationOffset = new Vector3(collisoreRacket.rotationOffset.x, n, collisoreRacket.rotationOffset.z);
    }
}