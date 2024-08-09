using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    NavMeshAgent agent;

    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isAlerted)
        {
            anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        }
        else
        {
            Pursue();
        }
        
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Pursue()
    {
        Vector3 targetDirection = player.transform.position - this.transform.position;

        float lookAhead = targetDirection.magnitude / (agent.speed + 50);

        Seek(player.transform.position + player.transform.forward * lookAhead);
    }
}
