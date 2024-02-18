using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("YourMenu/Wheel Controller")]
public class WheelController : MonoBehaviour
{
    // Wheel settings
    private GameObject wheelGameObject;
    private Image wheelTexture;
    public float input = 0f;
    private float wheelAngle = 0f;
    public float maxWheelAngle = 45f;
    public float resetSpeed = 20f;
    public float centerDeadZoneRadius = 5f;

    private RectTransform wheelRect;
    private CanvasGroup wheelCanvasGroup;

    private float tempAngle, newAngle;
    private bool wheelPressed;

    private Vector2 wheelCenter, touchPos;

    private EventTrigger eventTrigger;

    void Awake()
    {
        wheelTexture = GetComponent<Image>();
    }

    void Update()
    {
        if (!wheelRect && wheelTexture)
        {
            WheelInit();
        }

        WheelControlling();

        input = GetWheelInput();
    }

    void WheelInit()
    {
        wheelGameObject = wheelTexture.gameObject;
        wheelRect = wheelTexture.rectTransform;
        wheelCanvasGroup = wheelTexture.GetComponent<CanvasGroup>();
        wheelCenter = wheelRect.position;

        WheelEventsInit();
    }

    void WheelEventsInit()
    {
        eventTrigger = wheelGameObject.GetComponent<EventTrigger>();

        var pointerDownEvent = new EventTrigger.TriggerEvent();
        pointerDownEvent.AddListener(data =>
        {
            var eventData = (PointerEventData)data;
            data.Use();

            wheelPressed = true;
            touchPos = eventData.position;
            tempAngle = Vector2.Angle(Vector2.up, eventData.position - wheelCenter);
        });

        eventTrigger.triggers.Add(new EventTrigger.Entry { callback = pointerDownEvent, eventID = EventTriggerType.PointerDown });

        var dragEvent = new EventTrigger.TriggerEvent();
        dragEvent.AddListener(data =>
        {
            var eventData = (PointerEventData)data;
            data.Use();
            touchPos = eventData.position;
        });

        eventTrigger.triggers.Add(new EventTrigger.Entry { callback = dragEvent, eventID = EventTriggerType.Drag });

        var endDragEvent = new EventTrigger.TriggerEvent();
        endDragEvent.AddListener(data =>
        {
            wheelPressed = false;
        });

        eventTrigger.triggers.Add(new EventTrigger.Entry { callback = endDragEvent, eventID = EventTriggerType.EndDrag });
    }

    public float GetWheelInput()
    {
        return Mathf.Round(wheelAngle / maxWheelAngle * 100) / 100;
    }

    public bool IsWheelPressed()
    {
        return wheelPressed;
    }

    void WheelControlling()
    {
        if (!wheelCanvasGroup || !wheelRect)
        {
            if (wheelGameObject)
                wheelGameObject.SetActive(false);
            return;
        }

        if (!wheelGameObject.activeSelf)
            wheelGameObject.SetActive(true);

        if (wheelPressed)
        {
            newAngle = Vector2.Angle(Vector2.up, touchPos - wheelCenter);

            if (Vector2.Distance(touchPos, wheelCenter) > centerDeadZoneRadius)
            {
                if (touchPos.x > wheelCenter.x)
                    wheelAngle += newAngle - tempAngle;
                else
                    wheelAngle -= newAngle - tempAngle;
            }

            if (wheelAngle > maxWheelAngle)
                wheelAngle = maxWheelAngle;
            else if (wheelAngle < -maxWheelAngle)
                wheelAngle = -maxWheelAngle;

            tempAngle = newAngle;
        }
        else
        {
            if (!Mathf.Approximately(0f, wheelAngle))
            {
                float deltaAngle = resetSpeed;

                if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                {
                    wheelAngle = 0f;
                    return;
                }

                if (wheelAngle > 0f)
                    wheelAngle -= deltaAngle;
                else
                    wheelAngle += deltaAngle;
            }
        }

        wheelRect.eulerAngles = new Vector3(0f, 0f, -wheelAngle);
    }

    void OnDisable()
    {
        wheelPressed = false;
        input = 0f;
    }
}
