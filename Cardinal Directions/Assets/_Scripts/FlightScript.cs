using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightScript : MonoBehaviour
{



    public float RotationSpeedZ = 3;
    public float RotationSpeedX = 3;

    public Vector3 LocalRotation;

    public float MinAngle = -45;
    public float MaxAngle = 45;

    public float FlightSpeed = 45;

    public float Gravity = 9.8f;

    Vector3 LocalVelocity;

    Rigidbody _rigidBody;

    private void Awake()
    {
       
    }

    void FixedUpdate ()
    {


        
       

        float Pitch = Input.GetAxis("Vertical"); // Speed variable
        float Roll = -Input.GetAxis("Horizontal") * 3; // Speed Variable
        transform.Rotate( Pitch, 0, Roll);

        

        transform.position = LocalVelocity;

        
        
    }
}
