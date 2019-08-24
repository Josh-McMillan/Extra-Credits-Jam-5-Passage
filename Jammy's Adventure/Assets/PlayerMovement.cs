using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;

    [SerializeField] private float jumpSpeed = 8.0f;

    [SerializeField] private float gravity = 20.0f;

    [SerializeField] private float maxAngularSpeed = 15.0f;

    [SerializeField] private Transform characterModel;

    private CharacterController controller;

    private Vector3 movementDirection = Vector3.zero;

    private Vector3 rotationDirection = Vector3.zero;

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
        else
        {
            movementDirection.y -= gravity * Time.deltaTime;
        }

        rotationDirection = Vector3.RotateTowards(characterModel.forward, new Vector3(movementDirection.x, 0.0f, movementDirection.z), maxAngularSpeed * Time.deltaTime, 0.0f);
        characterModel.rotation = Quaternion.LookRotation(rotationDirection);

        controller.Move(movementDirection * Time.deltaTime);
    }
}
