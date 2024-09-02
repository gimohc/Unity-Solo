using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float walkRange;
    [SerializeField] static float range = 10;

    GameObject player;
    NavMeshAgent agent;
    Vector3 destination;
    bool walkpointSet = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {

        if (!walkpointSet) SearchForDestination();
        else
        {

            agent.SetDestination(destination);
            if (Vector3.Distance(transform.position, destination) < 5)
                walkpointSet = false;
        }
    }
    void SearchForDestination()
    {
        float z = UnityEngine.Random.Range(-range, range);
        float x = UnityEngine.Random.Range(-range, range);
        destination = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

//        if (Physics.Raycast(destination, Vector3.down, groundLayer))
            walkpointSet = true;
    }
}
