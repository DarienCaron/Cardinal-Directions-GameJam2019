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

    public float FlightSpeed = 0;

    public float Gravity = 9.8f;

    public float MaxSpeed = 25;

    public float WingRotationSpeed = 50;

    Vector3 LocalVelocity;

    Rigidbody _rigidBody;

    float YRotation = 0;
    float XRotation = 0;
    float ZRotation = 0;

    public bool Landed { get; set; }
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        if(!Landed)
        {
            Vector3 ForwardDirection = transform.forward;

            ForwardDirection.Normalize();

            YRotation += Input.GetAxis("Horizontal") * Time.deltaTime * WingRotationSpeed;
            XRotation += -Input.GetAxis("Vertical") * Time.deltaTime * WingRotationSpeed;
            ZRotation += -Input.GetAxis("Horizontal") * Time.deltaTime * WingRotationSpeed;


            XRotation = Mathf.Clamp(XRotation, MinAngle, MaxAngle);
            ZRotation = Mathf.Clamp(ZRotation, MinAngle, MaxAngle);


            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(XRotation, YRotation, ZRotation), Time.deltaTime * WingRotationSpeed);

            //transform.rotation = Quaternion.Euler(XRotation, YRotation, ZRotation);



            Debug.Log(ForwardDirection);

            transform.Translate(ForwardDirection * Time.deltaTime * FlightSpeed, Space.World);
        }
    }


    public void Flap()
    {
       
        
    }
    
}
