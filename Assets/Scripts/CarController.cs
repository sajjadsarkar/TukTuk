using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CarController : MonoBehaviour
{
    public float Currentspeed = 10.0f; //Min Car's movement speed
    public float maxSpeed = 30.0f; // Maximum speed for the car
    public float turnSpeed = 100.0f; // Car's turning speed
    public TextMeshProUGUI speedText;
    public WheelCollider[] frontWheels; // Front wheel colliders
    public WheelCollider[] rearWheels; // Rear wheel colliders
    public GameObject[] frontWheelVisuals; // Front wheel visual GameObjects
    public GameObject[] rearWheelVisuals; // Rear wheel visual GameObjects

    private float horizontalInput;
    private float verticalInput;

    public Left wheelController;
    public Right wheelController2;
    private bool isMoving = false;

    public float steeringRatio = 2.0f;
    public float breakforce = 2f;
    public float accelerationRate = 5.0f;
    private void Update()
    {
        // Get input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (Currentspeed < maxSpeed)
        {
            Currentspeed += accelerationRate * Time.deltaTime;
            Currentspeed = Mathf.Clamp(Currentspeed, 0f, maxSpeed);
        }
        // Calculate the steering angle
        float turnAngle = wheelController.input * steeringRatio;
        turnAngle = Mathf.Clamp(turnAngle, -45, 45);

        float secondTurnAngle = wheelController2.input * steeringRatio;
        secondTurnAngle = Mathf.Clamp(secondTurnAngle, -45, 45);

        // Apply the calculated steering angle to the front wheels for the second wheel controller
        // Apply the calculated steering angle to the front wheels
        foreach (var wheel in frontWheels)
        {
            wheel.steerAngle = turnAngle + secondTurnAngle;
        }


        /* float forwardTorque = verticalInput * speed;

         // Apply speed limit
         forwardTorque = Mathf.Clamp(forwardTorque, -maxSpeed, maxSpeed);

         foreach (var wheel in rearWheels)
         {
             wheel.motorTorque = forwardTorque;
         }*/
    }
    public void MoveForward()
    {
        // Move the car forward
        float forwardTorque = Currentspeed;
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = forwardTorque;
        }
    }

    public void MoveBackward()
    {
        // Move the car backward
        float backwardTorque = -Currentspeed;
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = backwardTorque;
        }
    }
    public void Brake()
    {
        // Apply braking force to all wheels
        float brakingForce = Currentspeed * breakforce; 

        foreach (var wheel in rearWheels)
        {
            wheel.brakeTorque = brakingForce;
        }

        foreach (var wheel in frontWheels)
        {
            wheel.brakeTorque = brakingForce;
        }
    }
    public void BrakeReset()
    {
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = 0.0f;
        }
        foreach (var wheel in rearWheels)
        {
            wheel.brakeTorque = 0.0f;
        }

        foreach (var wheel in frontWheels)
        {
            wheel.brakeTorque = 0.0f;
        }
        MoveForward();
    }
    private void UpdateWheelVisuals()
    {
        UpdateWheelVisuals(frontWheels, frontWheelVisuals);
        UpdateWheelVisuals(rearWheels, rearWheelVisuals);
    }

    private void UpdateWheelVisuals(WheelCollider[] colliders, GameObject[] visuals)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            WheelCollider collider = colliders[i];
            GameObject visualWheel = visuals[i];

            Vector3 pos;
            Quaternion rot;
            collider.GetWorldPose(out pos, out rot);
            visualWheel.transform.position = pos;
            visualWheel.transform.rotation = rot;
        }
    }

    private void LateUpdate()
    {
        // Update wheel visuals in LateUpdate
        UpdateWheelVisuals();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Light"))
        {
            // Output a debug message
            Debug.Log(" Red Light");
        }
    }
}
