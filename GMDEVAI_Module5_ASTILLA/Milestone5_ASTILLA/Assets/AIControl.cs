using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum AgentBehavior
{
    AgentBehavior1,
    AgentBehavior2,
    AgentBehavior3
}

public class AIControl : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    public WASDMovement playerMovement;

    public float range;

    public AgentBehavior agentBehavior;


    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location) 
    {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection);
    }

    void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;

        float lookAhead =  targetDirection.magnitude/(agent.speed + playerMovement.currentSpeed);

        Seek(target.transform.position + target.transform.forward * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;

        float lookAhead = targetDir.magnitude / (agent.speed + playerMovement.currentSpeed);

        Flee(target.transform.position + target.transform.forward*lookAhead);
    }

    Vector3 wanderTarget;

    void Wander()
    {
        float wanderRadius = 20;
        float wanderDist = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter, 0, Random.Range(-1.0f, 1.0f) * wanderJitter);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDist);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotCount = World.Instance.GetHidingSpots().Length;

        for(int i = 0; i < hidingSpotCount; i++) 
        { 
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);

            if(spotDistance < distance) 
            { 
                chosenSpot = hidePosition;
                distance = spotDistance;  
            }
        }

        Seek(chosenSpot);
    }

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDirec = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotCount = World.Instance.GetHidingSpots().Length;

        for (int i = 0; i < hidingSpotCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);

            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDirec = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDirec.normalized);
        RaycastHit info;
        float rayDist = 100.0f;
        hideCol.Raycast(back, out info, rayDist);

        Seek(info.point + chosenDirec.normalized * 5);
    }

    bool canSeeTarget() //within line of sight of Target
    {
        RaycastHit rayCastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;

        if (Physics.Raycast(this.transform.position, rayToTarget, out rayCastInfo))
        {
            Debug.DrawLine(this.transform.position, rayToTarget);
            return rayCastInfo.transform.gameObject.tag == "Player";
        }
        Debug.Log("HELLO");
        return false;
    }

    bool withinRange()
    {
        if(Vector3.Distance(this.transform.position, target.transform.position) < range)
        {
            return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update()
    {
        //Seek(target.transform.position);
        //Flee(target.transform.position);
        //Pursue();
        //Evade();
        //Wander();
        //Hide();
        /*
        if (canSeeTarget()) 
        {
            Debug.Log("True");
            CleverHide();
        }
        else
        {
            Debug.Log("FALSE");
        }
        */

        switch (agentBehavior)
        {
            case AgentBehavior.AgentBehavior1:
                BehaviorOne();
                break;
            case AgentBehavior.AgentBehavior2: 
                BehaviorTwo();
                break;
            case AgentBehavior.AgentBehavior3:
                BehaviorThree();
                break;
            default:
                Debug.Log("Check This variable");
                break;
        }
        
    }

    void BehaviorOne()
    {
        if(withinRange())
        {
            Pursue();
        }
        else
        {
            Wander();
        }
    }

    void BehaviorTwo()
    {
        if(withinRange() && canSeeTarget()) 
        {
            CleverHide();
        }
        else if(!withinRange())
        {
            Wander();
        }
    }

    void BehaviorThree()
    {
        if (withinRange()) 
        {
            Evade();
        }
        else
        {
            Wander();
        }
    }

}
