using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AI_Movement : MonoBehaviour
{
#region PUBLIC
    public float m_DelayAtWaypoint;
    public float m_DelayWhenScared;
    public Transform[] waypoints;
    public Transform[] safeZones;
    public bool isScared;
#endregion

#region PRIVATE
    NavMeshAgent navMeshAgent;
    int m_CurrentWaypointIndex;
#endregion
    void Start()
    {
        InitializeNPC();
    }
    void Update()
    {
        if(!isScared){
            MoveNPC();
        }  
    }

#region PUBLIC_METHODS
    ///<summary>Move NPC to Safe Zone after scare</summary>
    public void MoveToSafeZone(){
        isScared = true;
        Debug.Log("NPC Moving to safe zone");
        StartCoroutine(DelayAtWaypoint(m_DelayWhenScared));
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % safeZones.Length;
        navMeshAgent.SetDestination(safeZones[m_CurrentWaypointIndex].position);
    }
#endregion

#region PRIVATE_METHODS
    void InitializeNPC(){
        isScared = false;
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
    IEnumerator DelayAtWaypoint(float duration){
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(duration);
        navMeshAgent.isStopped = false;
    }
#endregion
}