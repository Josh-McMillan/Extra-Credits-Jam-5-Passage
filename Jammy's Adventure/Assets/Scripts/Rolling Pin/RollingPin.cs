using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingPin : MonoBehaviour
{
    [SerializeField] private float attackSpeed;

    [SerializeField] private float returnSpeed;

    private Animator animator;

    private Transform player;

    private Vector3 startPosition;

    private EnemyFace face;

    private void Start()
    {
        startPosition = transform.position;

        animator = GetComponent<Animator>();
        face = GetComponent<EnemyFace>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public bool PlayerInLineOfSight()
    {
        if (player.position.x > transform.position.x - 2.5f &&
            player.position.x < transform.position.x + 2.5 &&
            player.position.z < transform.position.z)
        {
            return true;
        }

        return false;
    }

    public void RollPin()
    {
        transform.Translate(0.0f, 0.0f, -attackSpeed * Time.deltaTime);
    }

    public void ReturnPin()
    {
        transform.Translate(0.0f, 0.0f, returnSpeed * Time.deltaTime);

        if (transform.position.z > startPosition.z)
        {
            transform.position = startPosition;
            animator.SetTrigger("Idle");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            animator.SetTrigger("Hit");
        }
    }

    public void SetFace(EnemyFaceType type)
    {
        face.ChangeFace(type);
    }

    public void ResetTriggers()
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Roll");
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Return");
    }
}
