using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;
    public float chaseDistance = 5f; // Distance threshold for chasing the player
    private bool isChasing = false; // Flag to track if the agent is chasing
    private GameObject player; // Reference to the player GameObject
    private bool isAttacking = false; // Flag to track if the agent is attacking
    private Rigidbody rb;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player dynamically
        rb = GetComponent<Rigidbody>();

        // Ensure the agent starts on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position); // Ensure the agent is on the NavMesh
        }
    }

    void Update()
    {
        if (player != null && !isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (isChasing)
            {
                if (distanceToPlayer > chaseDistance)
                {
                    isChasing = false; // Stop chasing if the player is too far
                }
                else
                {
                    agent.SetDestination(player.transform.position); // Chase the player
                }
            }
            else
            {
                if (distanceToPlayer <= chaseDistance)
                {
                    isChasing = true; // Start chasing the player
                }
                else if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        agent.SetDestination(point);
                    }
                }
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas)) // Increase sample radius
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true; // Start chasing the player
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        agent.isStopped = true; // Stop the NavMeshAgent

        // Assume you have an attack animation to play here
        // animator.SetTrigger("Attack");

        // Set Rigidbody to kinematic to prevent physics interactions
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(1.0f); // Adjust this duration as needed

        // Re-enable the NavMeshAgent and Rigidbody
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        agent.isStopped = false;
        isAttacking = false;
    }
}