using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToGoal : MonoBehaviour
{
    [SerializeField] Transform goal;
    [SerializeField] float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = goal.position - this.transform.position;

        transform.LookAt(goal);

        if (direction.magnitude > 1 )
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        
    }
}
