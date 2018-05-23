using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampVel : MonoBehaviour
{

    public float maxSpeed = 13.5f;

    private Rigidbody _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    /*void Update()
    {
        // Trying to Limit Speed
        if (_rigidbody.velocity.magnitude > maxSpeed)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Racket Follower(Clone)")
        {
            if (_rigidbody.velocity.magnitude > maxSpeed)
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
            }
        }
    }
}
