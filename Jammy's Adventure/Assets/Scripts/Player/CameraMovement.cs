using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    [SerializeField] private Vector3 farOffsetPosition;

    [SerializeField] private Vector3 closeOffsetPosition;

    [SerializeField] private float playerMotionCompensation = 2.0f;

    private Transform player;

    private Vector3 targetPosition;

    private Vector3 currentVelocity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("CamTarget").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        // Switch camera positions based on if player is checking UI

        targetPosition = player.position;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetPosition += closeOffsetPosition;
        }
        else
        {
            targetPosition += farOffsetPosition;
        }

        // Move towards target position with set speed!

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, maxSpeed);
    }
}
