using UnityEngine;
using UnityEngine.UI;

public class LeftRight : MonoBehaviour
{
    public float input = 0f;

    // Reference to the UI buttons
    public Button leftButton;
    public Button rightButton;
    public float inputSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        // Add listeners to the buttons
        leftButton.onClick.AddListener(LeftButtonClicked);
        rightButton.onClick.AddListener(RightButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to handle left button click
    public void LeftButtonClicked()
    {
        input = -1f;
    }

    // Function to handle right button click
    public void RightButtonClicked()
    {
        input = 1f;
    }
}
