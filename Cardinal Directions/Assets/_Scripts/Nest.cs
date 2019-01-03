using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour {

    public Transform NestRestLocation;
    public SphereCollider Collider;


    private void Awake()
    {
        if(Collider)
        {
            Debug.Log("Collider is set");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<FlightScript>())
        {
            other.gameObject.GetComponent<FlightScript>().Landed = true;
            other.gameObject.transform.position = NestRestLocation.position;
            other.gameObject.transform.rotation = NestRestLocation.rotation;
            other.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            other.gameObject.GetComponent<Rigidbody>().MovePosition(NestRestLocation.position);
            other.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
    }
}
