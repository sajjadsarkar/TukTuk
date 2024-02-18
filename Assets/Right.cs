using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Right : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float maxTurnRate = 1f; // Maximum turn rate per second
    public float releaseSpeed = 0.5f; // Speed at which input resets to 0 when not pressed

    public float input = 0f;

    public void OnPointerDown(PointerEventData eventData)
    {
            input = 1f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = 0f;
    }

    private void Update()
    {
        // Slowly decrease input when not pressing buttons
        if (input == 0f)
        {
            input = Mathf.MoveTowards(input, 0f, releaseSpeed * Time.deltaTime);
        }

        // Apply input to car movement
        float turnAmount = input * maxTurnRate * Time.deltaTime;
        transform.Rotate(0f, turnAmount, 0f);
    }
}
