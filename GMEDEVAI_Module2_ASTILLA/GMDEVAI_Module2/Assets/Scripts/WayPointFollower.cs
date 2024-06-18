using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour
{
    //public GameObject[] wayPoints;
    public UnityStandardAssets.Utility.WaypointCircuit circuit;

    [SerializeField] int currentWaypointIndex = 0;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float accuracy;

    // Start is called before the first frame update
    void Start()
    {
        //wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (circuit.Waypoints.Length == 0) return;

        GameObject currentWaypoint = circuit.Waypoints[currentWaypointIndex].gameObject;

        Vector3 lookatGoal = new Vector3(currentWaypoint.transform.position.x,
                                          this.transform.position.y,
                                          currentWaypoint.transform.position.z);

        Vector3 direction = lookatGoal - this.transform.position;

        if(direction.magnitude < 1.0f)
        {
            currentWaypointIndex++;

            if(currentWaypointIndex >= circuit.Waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
