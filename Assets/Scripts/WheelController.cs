using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour {

    public WheelAlignment[] steerableWheels;

    public float BreakPower;
    public float Horizontal;
    public float Vertical;
    public float wheelRotateSpeed;
    public float wheelSteeringAngle;
    public float wheelAcceleration;
    public float wheelMaxSpeed;
    
    private Rigidbody _rb;

    public InputSystem inputSystem;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        WheelControl(); 
    }
    private void WheelControl()
    {
        for (int i = 0; i < steerableWheels.Length; i++)
        {
            steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i].steeringAngle, 0, Time.deltaTime * wheelRotateSpeed);
            steerableWheels[i].wheelCol.motorTorque = -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, 0, Time.deltaTime * wheelAcceleration);

            Horizontal = inputSystem.steering;
            Vertical = inputSystem.acceleration;

            if (Vertical > 0.1)
            {
                steerableWheels[i].wheelCol.motorTorque = -Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, 
                    wheelMaxSpeed, Time.deltaTime * wheelAcceleration);
            }

            if (Vertical < -0.1)
            {
                steerableWheels[i].wheelCol.motorTorque = Mathf.Lerp(steerableWheels[i].wheelCol.motorTorque, 
                    wheelMaxSpeed, Time.deltaTime * wheelAcceleration * BreakPower);
                _rb.drag = 0.3f;
            }
            else
            {
                _rb.drag = 0;
            }


            if (Horizontal < -0.1)
            {
                steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i].steeringAngle, 
                    -wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            }

            if (Horizontal > 0.1)
            {
                steerableWheels[i].steeringAngle = Mathf.LerpAngle(steerableWheels[i].steeringAngle, 
                    wheelSteeringAngle, Time.deltaTime * wheelRotateSpeed);
            }
            
        }
    }

}
