using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField]
    private GameObject[] waypoints;

    private int waypointIndex = 0;
    private readonly float moveCooldown = 2;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[waypointIndex].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            UpdateWaypoint();
    }

    private void UpdateWaypoint()
    {
        new WaitForSeconds(moveCooldown);
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[waypointIndex].transform.position);
    }
}
