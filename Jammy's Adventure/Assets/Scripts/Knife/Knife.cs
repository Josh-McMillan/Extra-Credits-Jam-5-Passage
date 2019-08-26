using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knife : MonoBehaviour
{
    [SerializeField] private float walkRange = 5.0f;

    [SerializeField] private Vector2 walkArea;

    [SerializeField] private float attackRange = 2.5f;

    [SerializeField] private float attackRotationSpeed = 5.0f;

    private Vector3 startPosition;

    private Animator animator;

    private Transform player;

    private NavMeshAgent agent;

    private bool canMove = true;

    private void Start()
    {
        startPosition = transform.position;

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void WanderToRandomPoint()
    {
        agent.SetDestination(GenerateNewWanderPoint(transform.position.y));
    }

    public bool PlayerInRadius()
    {
        return player.position.x > startPosition.x - walkArea.x &&
               player.position.x < startPosition.x + walkArea.x &&
               player.position.z > startPosition.z - walkArea.y &&
               player.position.z < startPosition.z + walkArea.y;
    }

    public bool PlayerInAttackRadius()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    public void PursuePlayer()
    {
        agent.SetDestination(player.position);
    }

    public bool PlayerHasStopped()
    {
        return !agent.hasPath;
    }

    public void StopKnife()
    {
        agent.destination = transform.position;
    }

    public bool CanAttackMove()
    {
        return canMove;
    }

    public void StopAttackMovement()
    {
        canMove = false;
    }

    public void RestartAttackMovement()
    {
        canMove = true;
    }

    public void RotateToPlayer()
    {
        Vector3 lookPos = (player.position + player.forward) - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, attackRotationSpeed * Time.deltaTime);
    }

    private Vector3 GenerateNewWanderPoint(float y)
    {
        float x = startPosition.x + Random.Range(-walkArea.x, walkArea.x);
        float z = startPosition.z + Random.Range(-walkArea.y, walkArea.y);

        return new Vector3(x, y, z);
    }
}