using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;


[RequireComponent( typeof( Interactable ) )]

public class BallSpawnController : MonoBehaviour {

    [Header("Palla (Prefab)")]
    public GameObject Prefab;

    /*[Header("Punto di creazione palla")]*/
    private Transform ballSpawnPoint;

	// Use this for initialization
	void Start () {
		ballSpawnPoint = GameObject.Find ("BallSpawnPoint").GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update () {
	}
    
    public void InitBall()
    {
        //SetArea();
        GameObject tennisBall = Instantiate(Prefab, ballSpawnPoint.position, Quaternion.identity) as GameObject;
        Destroy(tennisBall, 15);
    }
}


