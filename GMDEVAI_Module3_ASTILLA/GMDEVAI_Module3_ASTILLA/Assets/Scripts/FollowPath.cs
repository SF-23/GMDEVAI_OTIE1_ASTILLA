using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float accuracy = 1.0f;
    [SerializeField] float rotSpeed = 2.0f;

    public GameObject wpManager;

    GameObject[] wps;

    GameObject currentNode;

    int currentWaypointIndex = 0;

    Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WayPointManager>().waypoints;
        graph = wpManager.GetComponent<WayPointManager>().graph;
        
        currentNode = wps[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }

        currentNode = graph.getPathPoint(currentWaypointIndex);

        if (Vector3.Distance(graph.getPathPoint(currentWaypointIndex).transform.position,
                            transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, goal.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.root.rotation = Quaternion.Slerp(this.transform.rotation,
                                                             Quaternion.LookRotation(direction),
                                                             Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void GoToHelipad()
    {
        graph.AStar(currentNode, wps[0]);//
        currentWaypointIndex = 0;
    }

    public void GoToRuins()
    {
        graph.AStar(currentNode, wps[6]);//
        currentWaypointIndex = 0;
    }

    public void GoToFactory()
    {
        graph.AStar(currentNode, wps[7]);//
        currentWaypointIndex = 0;
    }

    public void GoToBarracks()
    {
        graph.AStar(currentNode, wps[3]);//
        currentWaypointIndex = 0;
    }

    public void GoToCommandCenter()
    {
        graph.AStar(currentNode, wps[20]);//
        currentWaypointIndex = 0;
    }

    public void GoToTwinMountains()
    {
        graph.AStar(currentNode, wps[2]);//
        currentWaypointIndex = 0;
    }

    public void GoToOilRefinery()
    {
        graph.AStar(currentNode, wps[15]);//
        currentWaypointIndex = 0;
    }

    public void GoToTankers()
    {
        graph.AStar(currentNode, wps[16]);//
        currentWaypointIndex = 0;
    }

    public void GoToRadar()
    {
        graph.AStar(currentNode, wps[17]);//
        currentWaypointIndex = 0;
    }

    public void GoToCP()
    {
        graph.AStar(currentNode, wps[18]);//
        currentWaypointIndex = 0;
    }

    public void GoToCenter()
    {
        graph.AStar(currentNode, wps[21]);//
        currentWaypointIndex = 0;
    }
}
