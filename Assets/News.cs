using UnityEngine;

public class News : MonoBehaviour
{
    public float speed = 5f;
    public float sideMovementSpeed = 10f;
    public float steeringWheelRotationSpeed = 100f;
    public float maxSteerAngle = 40f;

    public WheelCollider frontWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform steeringWheelTransform;

    private Rigidbody rb;
    private float horizontalInput;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Continuous input while the buttons are held down
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        MoveForward();
        RotateSteeringWheel(horizontalInput);
        ApplySideMovement();
        KeepCarGrounded();
        UpdateWheelRotations();
        SmoothRotation();
    }

    private void MoveForward()
    {
        if (isGrounded)
        {
            Vector3 moveDirection = transform.forward * speed * horizontalInput;
            rb.velocity = moveDirection;
        }
    }


    private void RotateSteeringWheel(float direction)
    {
        if (isGrounded)
        {
            float targetRotation = Mathf.Lerp(-maxSteerAngle, maxSteerAngle, (direction + 1f) / 2f);
            steeringWheelTransform.localRotation = Quaternion.Euler(0f, targetRotation, 0f);
            frontWheelCollider.steerAngle = targetRotation;
        }
    }

    private void ApplySideMovement()
    {
        if (isGrounded)
        {
            float sideSpeed = horizontalInput * sideMovementSpeed * Time.fixedDeltaTime;
            rb.AddForce(transform.right * sideSpeed, ForceMode.VelocityChange);
        }
    }

    private void KeepCarGrounded()
    {
        RaycastHit hit;
        float raycastLength = 1.0f; // Adjust this based on your car's size

        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, raycastLength);
        if (isGrounded)
        {
            float upwardForce = Physics.gravity.magnitude * rb.mass;
            rb.AddForce(transform.up * upwardForce);
        }

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void UpdateWheelRotations()
    {
        UpdateSingleWheelRotation(frontWheelCollider, frontWheelCollider.transform);
        UpdateSingleWheelRotation(rearLeftWheelCollider, rearLeftWheelCollider.transform);
        UpdateSingleWheelRotation(rearRightWheelCollider, rearRightWheelCollider.transform);
    }

    private void UpdateSingleWheelRotation(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    private void SmoothRotation()
    {
        if (isGrounded && rb.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, steeringWheelRotationSpeed * Time.fixedDeltaTime);
        }
    }
}
