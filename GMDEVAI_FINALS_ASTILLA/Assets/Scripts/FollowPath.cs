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
            Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.root.rotation = Quaternion.Slerp(this.transform.rotation,
                                                             Quaternion.LookRotation(direction),
                                                             Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("WinArea"))
        {
            GameManager.instance.DoWin();
        }

        if (other.gameObject.name.Contains("Warden") || other.gameObject.name.Contains("PatrolTank"))
        {
            GameManager.instance.DoLose();
        }
    }

    public void GoToTanker()
    {
        graph.AStar(currentNode, wps[0]);
        currentWaypointIndex = 0;

    }

    public void GoToRockOne()
    {
        graph.AStar(currentNode, wps[1]);
        currentWaypointIndex = 0;
       
    }

    public void GoToRefinery()
    {
        graph.AStar(currentNode, wps[2]);
        currentWaypointIndex = 0;
      
    }

    public void GoToBesideFactory()
    {
        graph.AStar(currentNode, wps[3]);
        currentWaypointIndex = 0;
        
    }

    public void GoToOpenAreaOne()
    {
        graph.AStar(currentNode, wps[10]);
        currentWaypointIndex = 0;
       
    }

    public void GoToBackFactory()
    {
        graph.AStar(currentNode, wps[4]);
        currentWaypointIndex = 0;
      
    }

    public void GoToOasis()
    {
        graph.AStar(currentNode, wps[6]);
        currentWaypointIndex = 0;
    }

    public void GoToDroughtRock()
    {
        graph.AStar(currentNode, wps[5]);
        currentWaypointIndex = 0;
    }

    public void GoToNorthHelipad()
    {
        graph.AStar(currentNode, wps[13]);
        currentWaypointIndex = 0;
    }

    public void GoToWestHelipad()
    {
        graph.AStar(currentNode, wps[9]);
        currentWaypointIndex = 0;
    }

}


