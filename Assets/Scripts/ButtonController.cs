using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CarController carController;
    public bool isForward = true;
    public float decelerationRate = 0.2f;
    public bool isPressed = true;
    public float accelerationRate = 0.1f; // Rate at which the car accelerates (increase this value for faster acceleration)

    private void Update()
    {
        if (isPressed)
        {
            if (isForward)
            {
                // Gradually increase the car's speed
                carController.Currentspeed += accelerationRate * Time.deltaTime;
                carController.MoveForward();
            }
            else
            {
                // Gradually increase the car's speed in reverse
                carController.Currentspeed += accelerationRate * Time.deltaTime;
                carController.MoveBackward();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        carController.BrakeReset();
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        carController.Brake();
        StartCoroutine(SlowDownCar());
    }

    private IEnumerator SlowDownCar()
    {
        while (carController.Currentspeed > 500)
        {
            carController.Currentspeed -= decelerationRate * Time.deltaTime;
            yield return null;
        }
        carController.Currentspeed = 500;
    }
}
