using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

// Rigidbody of the player
    private Rigidbody rb;

// Reference to the NavMeshAgent component for pathfinding	
    private NavMeshAgent navMeshAgent;

    // Reference to the player's transform
    public Transform player;
	
	
// Start is called before the first frame update
    void Start()
    {
        // Get and store the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();

        // Get and store the NavMeshAgene component attached to this object
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (navMeshAgent.isOnNavMesh && player != null)
        {
           
            navMeshAgent.SetDestination(player.position);
        }
    }
}
