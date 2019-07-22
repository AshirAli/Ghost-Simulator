using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public int m_DelayAtWaypoint;
    public Transform[] waypoints;
    NavMeshAgent navMeshAgent;
    int m_CurrentWaypointIndex;
    void Start()
    {
        InitializeNPC();
    }
    void Update()
    {
        MoveNPC();
    }
    void InitializeNPC(){
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[0].position);
    }
    void MoveNPC(){
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            StartCoroutine(DelayAtWaypoint(m_DelayAtWaypoint));
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
            //Debug.Log("Current waypoint " + m_CurrentWaypointIndex);
        }
    }
    IEnumerator DelayAtWaypoint(int duration){
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(duration);
        navMeshAgent.isStopped = false;
    }
}
