using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DetectAngleRacket : MonoBehaviour {

    public GameObject racketController;
    public Transform racketTarget;
    public LookAtConstraint collisoreRacket;
	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    void Update() {
        //Debug.Log(collisoreRacket.rotationOffset);
        if (racketController.transform.rotation.eulerAngles.y <= 270 && racketController.transform.rotation.eulerAngles.y >= 90)
        {
            //collisoreRacket.rotationOffset.Set(0,0,90);
            SetTransformX(180.0f);
            Debug.Log("RacketY=" + racketController.transform.rotation.eulerAngles.y + "| sotto 270");
        }
        else
        {
            //collisoreRacket.rotationOffset.Set(0,180,90);
            SetTransformX(0f);
            Debug.Log("RacketY=" + racketController.transform.rotation.eulerAngles.y + "oltre 270");
        }

    }

    void SetTransformX(float n)
    {
        //racketTarget.transform.position = new Vector3(n, racketTarget.transform.position.y, racketTarget.transform.position.z);
        collisoreRacket.rotationOffset = new Vector3(collisoreRacket.rotationOffset.x, n, collisoreRacket.rotationOffset.z);
    }
}
