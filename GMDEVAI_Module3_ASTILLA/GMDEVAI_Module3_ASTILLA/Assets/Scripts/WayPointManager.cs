using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public struct Link
{
    public enum direction {UNI, BI};

    public GameObject node1;
    public GameObject node2;

    public direction dir;
}

public class WayPointManager : MonoBehaviour
{
    public GameObject[] waypoints;

    public Link[] links;

    public Graph graph = new Graph();

    // Start is called before the first frame update
    void Start()
    {
        if(waypoints.Length > 0) 
        {
            foreach (GameObject wp in waypoints)
            {
                graph.AddNode(wp);
            }

            foreach (Link I in links)
            {
                graph.AddEdge(I.node1 , I.node2 );
                if(I.dir == Link.direction.BI)
                {
                    graph.AddEdge(I.node1, I.node2);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        graph.debugDraw();
    }
}
