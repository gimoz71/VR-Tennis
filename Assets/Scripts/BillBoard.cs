using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

    private Transform m_Camera;

    void Start()
    {
        m_Camera = GameObject.Find("CamLeft (eye)").GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(m_Camera);
    }
}
