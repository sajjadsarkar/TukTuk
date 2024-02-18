using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Break : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CarController carController;
    private bool isPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed= true;
        UpdateBreak();

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        UpdateBreak();
    }

    public void UpdateBreak()
    {
        if (isPressed) {
            carController.Brake();
        }
        else
        {
        }
    }
}