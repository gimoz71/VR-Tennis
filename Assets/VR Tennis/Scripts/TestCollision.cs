using UnityEngine;
using System.Collections;

public class TestCollision : MonoBehaviour
{
    public bool DebugBreak = false;
    public float FirePower;
    public float RacketMoveSpeed;
    public Transform Racket;
    public Transform TennisBall;

    private Vector3 RacketOrigPos;
    private Vector3 TennisBallOrigPos;
    private bool Testing = false;
    public LayerMask InteractiveLayer;
	// Use this for initialization
	void Start ()
    {
        RacketOrigPos = Racket.position;
        TennisBallOrigPos = TennisBall.position;
        PrePosOfRacket = Racket.position;
        PrePosOfTennisBall = TennisBall.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.T))
        {
            Testing = true;
            FireBall();

            if(DebugBreak)
                Debug.Break();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }


	}

    private void Reset()
    {
        Testing = false;
        TennisBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Racket.position = RacketOrigPos;
        TennisBall.position = TennisBallOrigPos;
        PrePosOfRacket = Racket.position;
        PrePosOfTennisBall = TennisBall.position;
    }

    Vector3 PrePosOfRacket = Vector3.zero;
    Vector3 PrePosOfTennisBall = Vector3.zero;

    private void MoveRacket()
    {
        Vector3 moveDelta = PrePosOfRacket - Racket.position;
        float moveDist = moveDelta.magnitude;
        PrePosOfRacket = Racket.position;
        Vector3 ballMoveDelta = PrePosOfTennisBall - TennisBall.position;
        PrePosOfTennisBall = TennisBall.position;
        Vector3 moveforward = Vector3.left * RacketMoveSpeed;

        Racket.Translate(moveforward);
        Debug.LogError("move Dist:" + moveDist);
        RaycastHit hit;
        if(Physics.BoxCast(Racket.position, Vector3.one * 0.2f, Racket.forward,out hit, Racket.rotation, moveDist, InteractiveLayer))
        {
            TennisBall.position = hit.point;
            TennisBall.GetComponent<Rigidbody>().velocity = -hit.normal * ((moveDist + ballMoveDelta.magnitude) / Time.fixedDeltaTime);
            Debug.LogError("hit");
        }
        
    }

    private void FireBall()
    {
        TennisBall.GetComponent<Rigidbody>().velocity = Vector3.back * FirePower;
    }

    void FixedUpdate()
    {
        if (Testing)
        {
            MoveRacket();
        }
    }

    void OnDrawGizmos()
    {
    }
}
