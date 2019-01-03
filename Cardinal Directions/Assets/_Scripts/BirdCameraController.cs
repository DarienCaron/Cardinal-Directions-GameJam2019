using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCameraController : MonoBehaviour
{

    public FlightScript ObjectToFollow;

    public Transform LocationToSit;


    private void Awake()
    {
        
    }

    private void Update()
    {
        transform.position = LocationToSit.position;
    }

    private void LateUpdate()
    {
        if(ObjectToFollow)
        {
            
                transform.LookAt(ObjectToFollow.transform);
           
         

        }
    }
}
