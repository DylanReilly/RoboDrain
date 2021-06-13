using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float gravityValue = -9.81f;
    public float turnSmoothTime = 0.1f;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    public float currentSpeed;
    public float jumpStrength = 19.6f;



    private Vector2 velocity;
    private Vector3 fallVelocity = new Vector3(0f, 0f, 0f);
    private bool groundedPlayer;
    private float turnSmoothVelocity;
    private float targetAngle;
    private float angle;
    private Vector3 moveDirection;
    private float moveSpeed;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && fallVelocity.y < 0)
        {
            fallVelocity.y = 0f;
        }

        if(direction.magnitude>=0.1)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            moveDirection = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }

        if(Input.GetKeyDown("space") && groundedPlayer)
        {
            fallVelocity.y = jumpStrength;
        }

        fallVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(((moveDirection.normalized * moveSpeed) + fallVelocity) * Time.deltaTime);
        currentSpeed = (moveDirection.normalized * moveSpeed * Time.deltaTime).magnitude;


        /*
        // Move.
        IsRunning = canRun && Input.GetKey(runningKey);
        float movingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
            movingSpeed = speedOverrides[speedOverrides.Count - 1]();
        velocity.y = Input.GetAxis("Vertical") * movingSpeed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * movingSpeed * Time.deltaTime;
        currentSpeed = velocity.magnitude;

        transform.Translate(velocity.x, 0, velocity.y);

        */
    }
}