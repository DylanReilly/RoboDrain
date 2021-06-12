using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravityValue = -9.81f;

    private Vector3 fallVelocity = new Vector3(0f,0f,0f);
    private bool groundedPlayer;
    private float turnSmoothVelocity;
    private float targetAngle;
    private float angle;
    private Vector3 moveDirection;


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

        if (direction.magnitude>=0.1)
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
        fallVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(((moveDirection.normalized * speed) + fallVelocity )* Time.deltaTime);
    }
}
