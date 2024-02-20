using UnityEngine;

public class News : MonoBehaviour
{
    public float speed = 5f; // Speed of the tuktuk
    public float sideMovementSpeed = 10f; // Speed of side movement

    public WheelCollider frontWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    // Update is called once per frame
    void Update()
    {
        // Move the tuktuk forward automatically
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check for input to move left or right
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMovingLeft = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isMovingLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isMovingRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isMovingRight = false;
        }

        // Move the tuktuk left or right
        if (isMovingLeft)
        {
            MoveTuktuk(-1);
        }
        else if (isMovingRight)
        {
            MoveTuktuk(1);
        }

        // Update wheel rotation
        UpdateWheelRotation(frontWheelCollider, frontWheelTransform);
        UpdateWheelRotation(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelRotation(rearRightWheelCollider, rearRightWheelTransform);
    }

    // Move the tuktuk left or right based on direction (-1 for left, 1 for right)
    void MoveTuktuk(int direction)
    {
        Vector3 targetPosition = transform.position + Vector3.right * direction;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, sideMovementSpeed * Time.deltaTime);
    }

    // Update the visual rotation of the wheel based on WheelCollider's rotation
    void UpdateWheelRotation(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
