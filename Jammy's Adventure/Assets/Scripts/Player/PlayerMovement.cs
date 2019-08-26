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

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private float checkDistance;

    [SerializeField] private float slideSpeed = 3.0f;

    [SerializeField] private float wallLimit = 75.0f;

    private CharacterController controller;

    private Animator animator;

    private Vector3 movementDirection = Vector3.zero;

    private Vector3 rotationDirection = Vector3.zero;

    private bool isGrounded;
    private bool isSlopeGrounded;

    private Vector3 hitNormal;
    private float angleComparison;

    private bool canMove = true;
    private Vector3 respawnPoint;

    private void OnEnable()
    {
        PlayerHealth.Died += OnPlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.Died -= OnPlayerDeath;
    }

    private void Start()
    {
        respawnPoint = transform.position;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            isSlopeGrounded = angleComparison < controller.slopeLimit || angleComparison > wallLimit;
            isGrounded = Physics.Raycast(transform.position, -transform.up, checkDistance, groundMask);

            if (controller.isGrounded && isSlopeGrounded)
            {
                movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                animator.SetFloat("Speed", Mathf.Abs(movementDirection.magnitude));

                movementDirection *= speed;

                if (Input.GetButton("Jump"))
                {
                    movementDirection.y = jumpSpeed;
                }
            }

            movementDirection.y -= gravity * Time.deltaTime;

            if (!isSlopeGrounded)
            {
                movementDirection.x = ((1f - hitNormal.y) * hitNormal.x) * slideSpeed;
                movementDirection.z = ((1f - hitNormal.y) * hitNormal.z) * slideSpeed;
            }

            rotationDirection = Vector3.RotateTowards(characterModel.forward, new Vector3(movementDirection.x, 0.0f, movementDirection.z), maxAngularSpeed * Time.deltaTime, 0.0f);
            characterModel.rotation = Quaternion.LookRotation(rotationDirection);

            controller.Move(movementDirection * Time.deltaTime);

            angleComparison = Vector3.Angle(Vector3.up, hitNormal);

            animator.SetBool("Grounded", isGrounded);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    private void OnPlayerDeath()
    {
        canMove = false;

        animator.SetTrigger("Death");
    }
}
