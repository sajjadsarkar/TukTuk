using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CarController CarController;
    private bool isOn = false;
    private bool isOnLight = false;
    public ButtonController buttonControllerForward;

    public Light[] lights;

    private void Start()
    {
        buttonControllerForward.enabled = false;
        SetLightsEnabled(false);
    }
    public void StartStop()
    {
        if (isOn)
        {
            CarController.enabled = false;
            isOn = false;
            buttonControllerForward.enabled = false;
        }
        else
        {
            CarController.enabled = true;
            isOn = true;
            buttonControllerForward.enabled = true;
        }
    }


    public void LightOnOff()
    {
        if (isOnLight)
        {
            SetLightsEnabled(false); // Turn off all lights.
            isOnLight = false;
        }
        else
        {
            SetLightsEnabled(true); // Turn on all lights.
            isOnLight = true;
        }
    }

    private void SetLightsEnabled(bool isEnabled)
    {
        foreach (Light light in lights)
        {
            light.enabled = isEnabled;
        }
    }
}
