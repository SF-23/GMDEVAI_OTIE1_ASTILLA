using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaypointTagVariant
{
    waypoint1,
    waypoint2, 
    waypoint3
}

public class Patrol : NPCBaseFSM
{
    GameObject[] wayPoints;
    public WaypointTagVariant waypointTagVariant;
    int currentWaypoint;

    private void Awake()
    {
        switch(waypointTagVariant)
        {
            case WaypointTagVariant.waypoint1:
                wayPoints = GameObject.FindGameObjectsWithTag("waypoint");
                break;
            case WaypointTagVariant.waypoint2:
                wayPoints = GameObject.FindGameObjectsWithTag("waypointTwo");
                break;
            case WaypointTagVariant.waypoint3:
                wayPoints = GameObject.FindGameObjectsWithTag("waypointThree");
                break;
            default:
                Debug.Log("Check waypointTagVariant");
                break;
        }
        
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypoint = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (wayPoints.Length == 0) return;

        if (Vector3.Distance(wayPoints[currentWaypoint].transform.position, NPC.transform.transform.position) < accuracy)
        {
            currentWaypoint++;
            if(currentWaypoint >= wayPoints.Length) 
            { 
                currentWaypoint = 0;
            }
        }

        var direction = wayPoints[currentWaypoint].transform.position - NPC.transform.position;
        NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), rotSpeed*Time.deltaTime);

        NPC.transform.Translate(0, 0, Time.deltaTime * speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

 
}
