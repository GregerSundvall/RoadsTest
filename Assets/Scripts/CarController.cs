using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float vertInput,horizInput;
    public GameObject frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;
    private WheelCollider frontLeftWC, frontRightWC, rearLeftWC, rearRightWC;
    private Transform frontLeftTr, frontRightTr, rearLeftTr, rearRightTr;
    private float steeringAngle, engineTorque, brakeTorque;
    public float maxTorque, maxSteer;
    private Rigidbody rigidBody;
    
  
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        frontLeftTr = frontLeftWheel.GetComponent<Transform>();
        frontRightTr = frontRightWheel.GetComponent<Transform>();
        rearLeftTr = rearLeftWheel.GetComponent<Transform>();
        rearRightTr = rearRightWheel.GetComponent<Transform>();
        frontLeftWC = frontLeftWheel.GetComponent<WheelCollider>();
        frontRightWC = frontRightWheel.GetComponent<WheelCollider>();
        rearLeftWC = rearLeftWheel.GetComponent<WheelCollider>();
        rearRightWC = rearRightWheel.GetComponent<WheelCollider>();
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Accelerate();
        Steer();
        Brake();
    }

    private void LateUpdate()
    {
        
    }

    private void GetInput()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        if (vertInput > 0)
        {
            brakeTorque = 0;
            if (engineTorque < maxTorque)
            {
                engineTorque += 5;
            }
        }
        else if (engineTorque > 0)
        {
            engineTorque -= 10;
        }
        else if (vertInput < 0)
        {
            engineTorque = 0;
            brakeTorque += 4;
        }

        if (horizInput > 0.1f && steeringAngle < maxSteer)
        {
            steeringAngle += 0.1f;
        }
        else if (horizInput < -0.1f && steeringAngle > -maxSteer)
        {
            steeringAngle -= 0.1f;
        } 
        else if (horizInput == 0 && steeringAngle > 0)
        {
            steeringAngle -= 0.2f;
        }
        else if (horizInput == 0 && steeringAngle < 0)
        {
            steeringAngle += 0.2f;
        }
        
    }

    private void Steer()
    {
        frontLeftWC.steerAngle = steeringAngle;
        frontRightWC.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontLeftWC.motorTorque = engineTorque;
        frontRightWC.motorTorque = engineTorque;
        rearLeftWC.motorTorque = engineTorque;
        rearRightWC.motorTorque = engineTorque;
    }

    private void Brake()
    {
        frontLeftWC.brakeTorque = brakeTorque;
        frontRightWC.brakeTorque = brakeTorque;
        rearLeftWC.brakeTorque = brakeTorque;
        rearRightWC.brakeTorque = brakeTorque;
    }
}
