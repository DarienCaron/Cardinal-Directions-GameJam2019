using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCameraController : MonoBehaviour
{

    public FlightScript ObjectToFollow;

    public Transform LocationToSit;

    public float CameraRotationSpeed;
    public float CameraFollowSpeed;


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

            Vector3 Position = ObjectToFollow.transform.position;

            

            transform.LookAt(Position);
           



        }
       
    }
}
