using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;

    [SerializeField] private float jumpSpeed = 8.0f;

    [SerializeField] private float gravity = 20.0f;

    private CharacterController controller;

    private Vector3 movementDirection = Vector3.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            movementDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                movementDirection.y = jumpSpeed;
            }
        }

        movementDirection.y -= gravity * Time.deltaTime;

        controller.Move(movementDirection * Time.deltaTime);
    }
}
