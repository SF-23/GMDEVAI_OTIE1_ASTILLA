using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;

    public Transform PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject ai in agents)
        {
            ai.GetComponent<AIController>().agent.SetDestination(PlayerTransform.position);
        }
        /*
        if(Input.GetMouseButton(0)) 
        { 
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000) )
            {
                foreach (GameObject ai in agents)
                {
                    ai.GetComponent<AIController>().agent.SetDestination(hit.point);
                }
            }
        }
        *///For RayCast
    }
}
