using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankAIScriptNEW : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    public float range;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / (agent.speed + 50);

        Seek(target.transform.position + target.transform.forward * lookAhead);
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

    bool withinRange()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) < range)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!withinRange())
        {
            Wander();
        }
        else if(GameManager.instance.isAlerted)
        {
            Seek(target.transform.position);
        }
        else
        {
            GameManager.instance.isAlertON = true;
            Pursue();
        }

    }
}
