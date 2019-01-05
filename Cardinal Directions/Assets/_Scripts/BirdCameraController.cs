using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCameraController : MonoBehaviour
{

    public FlightScript ObjectToFollow;

    public Transform LocationToSit;

    public float CameraRotationSpeed;
    public float CameraFollowSpeed;

    public Vector3 LocationToLook;


    private void Awake()
    {
        LocationToLook = ObjectToFollow.transform.position - transform.position;
    }

    private void Update()
    {
        transform.position = LocationToSit.position;
    }

    private void LateUpdate()
    {
        if(ObjectToFollow)
        {
            Vector3 Position = ObjectToFollow.transform.position - transform.position; 
            Position.Normalize();

            LocationToLook = Vector3.Slerp(LocationToLook, Position, Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(LocationToLook, Vector3.up);

            




        }
       
    }
}
