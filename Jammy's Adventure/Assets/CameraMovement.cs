using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    [SerializeField] private Vector3 farOffsetPosition;

    [SerializeField] private Vector3 closeOffsetPosition;

    private Transform player;

    private Vector3 targetPosition;

    private Vector3 currentVelocity;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        // Switch camera positions based on if player is checking UI
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetPosition = player.position + closeOffsetPosition;
        }
        else
        {
            targetPosition = player.position + farOffsetPosition;
        }

        // Move towards target position with set speed!

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, maxSpeed);
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
