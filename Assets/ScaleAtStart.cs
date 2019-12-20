using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAtStart : MonoBehaviour
{
    //public Camera cam;
    public float objectScale = 1.0f;
    private Vector3 initialScale;
    private float dist;
    public GameObject ballSizeTarget;

    private float timer = 0.0f;
    private float waitTime = 3.0f;
    

// set the initial scale, and setup reference camera
void Start()
    {
        // record initial scale, use this as a basis
        initialScale = transform.localScale;

        ballSizeTarget = GameObject.Find("Ball Size Target");

        
    }

    // scale object relative to distance from camera plane
    void Update()
    {

        Plane plane = new Plane(ballSizeTarget.transform.forward, ballSizeTarget.transform.position);
        dist = plane.GetDistanceToPoint(transform.position);

        timer += Time.deltaTime;

        if (timer < waitTime)
        {
            if (dist >= 4)
            {
                transform.localScale = initialScale * dist / 4 * objectScale;
            }
            else
            {
                transform.localScale = initialScale;
            }
        }
        else
        {
            transform.localScale = initialScale;
        }
        Debug.Log(timer);

    }
}