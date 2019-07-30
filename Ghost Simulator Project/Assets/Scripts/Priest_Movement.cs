using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Priest_Movement : MonoBehaviour
{
#region PUBLIC
    public float m_DelayAtWaypoint;
    //public float m_DelayDuringScare;
    public Transform[] waypoints;
    public Transform[] safeZones;
    [HideInInspector]
    public bool hasReachedDestination;
#endregion

#region PRIVATE
    NavMeshAgent navMeshAgent;
    int m_CurrentWaypointIndex;
    NPC_Priest_Controller npc_Controller;
#endregion
    void Start()
    {
        npc_Controller = GetComponent<NPC_Priest_Controller>();
        InitializeNPC();
        //npc_Controller.AddToReliefTime(m_DelayAtWaypoint);
    }
    void Update()
    {
        if(!npc_Controller.isScared){
            MoveNPC();
            navMeshAgent.speed = 1f;
        }  
        ReachedDestination();
    }

#region PUBLIC_METHODS
    ///<summary>Move NPC to Safe Zone after scare</summary>
    public void MoveToSafeZone(){
        Debug.Log("NPC Moving to safe zone");
        //StartCoroutine(DelayAtWaypoint(m_DelayDuringScare));
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % safeZones.Length;
        navMeshAgent.SetDestination(safeZones[m_CurrentWaypointIndex].position);
        navMeshAgent.speed = 1.5f;
    }
#endregion

#region PRIVATE_METHODS
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
    IEnumerator DelayAtWaypoint(float duration){
        navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(duration);
        navMeshAgent.isStopped = false;
    }

    void ReachedDestination(){
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    hasReachedDestination = true;
                }
            }
        } 
        else{
            hasReachedDestination = false;
        } 
    }
#endregion
}