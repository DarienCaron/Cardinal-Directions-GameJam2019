using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestTracker : MonoBehaviour {

    public NestPlacer Placer;
    public GameObject Cardinal;
    public GameObject Compass;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Cardinal.GetComponent<FlightScript>().ArrowLocation.position;


        transform.LookAt(Placer.CurrentNest.position);
    }
}
