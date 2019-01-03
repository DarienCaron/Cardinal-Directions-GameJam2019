﻿using System.Collections;
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

    public float WingRotationSpeed = 150;

    Vector3 LocalVelocity;

    Rigidbody _rigidBody;

    float YRotation = 0;
    float XRotation = 0;
    float ZRotation = 0;

    public float ZeroAdjustment = 2;

    public bool Landed { get; set; }

    public FlightStates CurrentFlightState;

    bool NeedsToBeZeroedOut = false;

    float AdjustmentValue = 1.5f;


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

         
           

            if(ZRotation > ZeroAdjustment) // Figures out the state of flight, change so that it works with percentages instead
            {
                CurrentFlightState = FlightStates.TurningLeft;
            }
            if (ZRotation < -ZeroAdjustment)
            {
                CurrentFlightState = FlightStates.TurningRight;
            }
            if (ZRotation > 35)
            {
                CurrentFlightState = FlightStates.HardLeft;
            }
            if (ZRotation < -35)
            {
                CurrentFlightState = FlightStates.HardRight;
            }

            if(ZRotation < ZeroAdjustment && ZRotation > -ZeroAdjustment)
            {
                CurrentFlightState = FlightStates.ZeroedOut;
            }

            
            switch (CurrentFlightState)
            {
                case FlightStates.TurningLeft: // Slight left and right turns
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        NeedsToBeZeroedOut = true;
                        AdjustmentValue = 1.5f;
                    }
                    if (NeedsToBeZeroedOut)
                    {
                        break;
                    }
                    else
                    {
                        YRotation += Input.GetAxis("Horizontal") / AdjustmentValue; // allow for y rotation
                        AdjustmentValue = Mathf.Lerp(AdjustmentValue, 0, Time.deltaTime); // lerp for a smooth transition
                        AdjustmentValue = Mathf.Clamp(AdjustmentValue, 0, 1.5f);

                    }
                    break;
                case FlightStates.TurningRight:
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        NeedsToBeZeroedOut = true;
                        AdjustmentValue = 1.5f;
                    }
                    if (NeedsToBeZeroedOut)
                    {
                        break;
                    }
                    else
                    {
                        YRotation += Input.GetAxis("Horizontal") / AdjustmentValue;
                        AdjustmentValue = Mathf.Lerp(AdjustmentValue, 0, Time.deltaTime);
                        AdjustmentValue = Mathf.Clamp(AdjustmentValue, 0, 1.5f);
                        
                    }
                    break;
                case FlightStates.HardLeft:
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        NeedsToBeZeroedOut = true;
                        break;
                    }

                    if(!NeedsToBeZeroedOut) // If we aren't trying to straighten out
                    YRotation += Input.GetAxis("Horizontal");
                
                    break;
                case FlightStates.HardRight:
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        NeedsToBeZeroedOut = true;
                        break;
                    }
                    if(!NeedsToBeZeroedOut)
                    YRotation += Input.GetAxis("Horizontal");
                    
                    break;
                case FlightStates.ZeroedOut:
                    NeedsToBeZeroedOut = false;
                    AdjustmentValue = 1.5f;
                    break;
            }


            XRotation += Input.GetAxis("Vertical"); // Get our rotational axis
            ZRotation += -Input.GetAxis("Horizontal");


            XRotation = Mathf.Clamp(XRotation, MinAngle, MaxAngle); // Clamp the rotation
            ZRotation = Mathf.Clamp(ZRotation, MinAngle, MaxAngle);

           



            

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(XRotation, YRotation, ZRotation), Time.fixedDeltaTime * WingRotationSpeed); // Slerp the wing rotation



            //transform.rotation = Quaternion.Euler(XRotation, YRotation, ZRotation);



            Debug.Log(ForwardDirection);

            transform.Translate(ForwardDirection * Time.deltaTime * FlightSpeed, Space.World);
        }
    }


    public void Flap()
    {
       
        
    }
    
    public enum FlightStates
    {
        HardLeft,
        HardRight,
        TurningLeft,
        TurningRight,
        ZeroedOut
    }
}
