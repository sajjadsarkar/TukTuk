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

    public WheelController wheelController;
    private bool isMoving = false;

    public float steeringRatio = 2.0f;
    public float breakforce = 2f;

    private void Update()
    {
        // Get input from the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        float scaledSpeed = (Currentspeed - 500.0f) * 0.1f; // Adjust the scaling factor as needed

        // Update the UI Text with the scaled speed
        if (speedText != null)
        {
            speedText.text = scaledSpeed.ToString("F1") + " KM/H"; // Display one decimal place
        }
        // Calculate the steering angle
        float turnAngle = wheelController.input * steeringRatio;
        turnAngle = Mathf.Clamp(turnAngle, -45, 45);

        // Apply the calculated steering angle to the front wheels
        foreach (var wheel in frontWheels)
        {
            wheel.steerAngle = turnAngle;
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
