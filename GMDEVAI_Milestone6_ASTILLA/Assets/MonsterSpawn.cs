using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject celebrity;

    public GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(obstacle, hit.point, obstacle.transform.rotation);

                foreach(GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectMonster(hit.point);
                }
            }
        }

        if(Input.GetMouseButtonDown(1)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(celebrity, hit.point, obstacle.transform.rotation);

                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectWaypoint(hit.point);
                }
            }
        }

    }
}
