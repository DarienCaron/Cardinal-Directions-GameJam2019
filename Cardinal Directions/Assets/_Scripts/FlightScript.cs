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

    float CenterOutTimer = 0;

    public bool IsSpinning = false;

    public float ThreeSixtyAdjustment = 0;


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


            if(Input.GetKeyDown(KeyCode.F))
            {
                Flap();
            }

            if (!NeedsToBeZeroedOut)
            {
                if (ZRotation > ZeroAdjustment) // Figures out the state of flight, change so that it works with percentages instead
                {
                    CurrentFlightState = FlightStates.TurningLeft;
                }
                if (ZRotation < -ZeroAdjustment)
                {
                    CurrentFlightState = FlightStates.TurningRight;
                }
                if (ZRotation > 40)
                {
                    CurrentFlightState = FlightStates.HardLeft;
                }
                if (ZRotation < -40)
                {
                    CurrentFlightState = FlightStates.HardRight;
                }
            }

            

            
            switch (CurrentFlightState)
            {
                case FlightStates.TurningLeft: // Slight left and right turns
                    YRotation -= 0.15f;
                    if (Input.GetKey(KeyCode.D) && !NeedsToBeZeroedOut)
                    {
                        CurrentFlightState = FlightStates.ZeroedOut;
                        NeedsToBeZeroedOut = true;
                    }
                    break;
                case FlightStates.TurningRight:
                    YRotation += 0.15f;
                    if(Input.GetKey(KeyCode.A) && !NeedsToBeZeroedOut)
                    {
                        CurrentFlightState = FlightStates.ZeroedOut;
                        NeedsToBeZeroedOut = true;
                    }
                    break;
                case FlightStates.HardLeft:

                    if (ZRotation >= 40)
                    {
                        YRotation -= 0.5f;
                        
                    }
                    else
                    {
                        //YRotation += Input.GetAxis("Horizontal");
                        YRotation += Input.GetAxis("Mouse X");
                    }

                    break;
                case FlightStates.HardRight:
                    
                    if(ZRotation <= -40)
                    {
                        YRotation += 0.5f;
                    }
                    else
                    {
                        //YRotation += Input.GetAxis("Horizontal");
                        YRotation += Input.GetAxis("Mouse X");
                    }
                    break;
                case FlightStates.ZeroedOut:
                  if(NeedsToBeZeroedOut)
                    {
                        ZRotation = Mathf.Lerp(ZRotation, 0, Time.deltaTime);
                        transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.Euler(XRotation, YRotation, ZRotation), Time.fixedDeltaTime * WingRotationSpeed); // Slerp the wing rotation
                       
                    }
                    break;
            }

            if (ZRotation >= -1f && ZRotation <= 1f)
            {
                NeedsToBeZeroedOut = false;
            }

            if (!IsSpinning)
            {


                //XRotation += Input.GetAxis("Vertical"); // Get our rotational axis
                //ZRotation += -Input.GetAxis("Horizontal");

                XRotation += Input.GetAxis("Mouse Y"); // Get our rotational axis
                ZRotation += -Input.GetAxis("Mouse X");
            }




            XRotation = Mathf.Clamp(XRotation, MinAngle - 15, MaxAngle + 15); // Clamp the rotation
            if (!IsSpinning)
            {
                ZRotation = Mathf.Clamp(ZRotation, MinAngle, MaxAngle);
            }





            if (IsSpinning)
            {
                transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.Euler(XRotation, YRotation, ZRotation + ThreeSixtyAdjustment), Time.fixedDeltaTime * WingRotationSpeed); // Slerp the wing rotation

            }
            else
            {
                transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.Euler(XRotation, YRotation, ZRotation), Time.fixedDeltaTime * WingRotationSpeed);
            }


            //transform.rotation = Quaternion.Euler(XRotation, YRotation, ZRotation);

            Vector3 Velocity = ForwardDirection * Time.deltaTime * FlightSpeed;

            Velocity.y -= Gravity * Time.deltaTime;

            transform.Translate(Velocity, Space.World);

            


            if(transform.forward.y >= 0.4f)
            {
                FlightSpeed -= Time.deltaTime * 2;
            }
            else if(transform.forward.y <= -0.4f)
            {
                FlightSpeed += Time.deltaTime * 2;
            }
            else
            {
                FlightSpeed -= Time.deltaTime;
            }

            FlightSpeed = Mathf.Clamp(FlightSpeed, 0, MaxSpeed);
           
            if(Input.GetKeyDown(KeyCode.Space) && !IsSpinning)
            {
                IsSpinning = true;
                
            }

            if(GlobalClock.CurrentTime >= 18)
            {
                Debug.Log("You Are Dead");
            }

            

            if(IsSpinning)
            {
                switch(CurrentFlightState)
                {
                    case FlightStates.TurningLeft:
                        ThreeSixtyAdjustment = Mathf.LerpUnclamped(ThreeSixtyAdjustment, -360, Time.deltaTime * 1.75f );
                        break;
                    case FlightStates.HardLeft:
                        ThreeSixtyAdjustment = Mathf.LerpUnclamped(ThreeSixtyAdjustment, -360, Time.deltaTime * 1.75f);
                        break;
                    case FlightStates.HardRight:
                        ThreeSixtyAdjustment = Mathf.LerpUnclamped(ThreeSixtyAdjustment, 360, Time.deltaTime * 1.75f);
                        break;
                    case FlightStates.TurningRight:
                        ThreeSixtyAdjustment = Mathf.LerpUnclamped(ThreeSixtyAdjustment, 360, Time.deltaTime * 1.75f);
                        break;
                    case FlightStates.ZeroedOut:
                        ThreeSixtyAdjustment = Mathf.LerpUnclamped(ThreeSixtyAdjustment, 360, Time.deltaTime * 1.75f);
                        break;
                }

                if(ThreeSixtyAdjustment >= 355 || ThreeSixtyAdjustment <= -355)
                {
                    IsSpinning = false;
                    ThreeSixtyAdjustment = 0;
                    
                }
                
            }

            
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                Landed = false;
                transform.position += new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
                FlightSpeed = 15;
            }
        }
    }


    public void Flap()
    {
        FlightSpeed += 5;
        
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
