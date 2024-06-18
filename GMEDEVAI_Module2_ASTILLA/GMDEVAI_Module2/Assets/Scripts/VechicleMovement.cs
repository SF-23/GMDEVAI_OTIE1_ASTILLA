using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VechicleMovement : MonoBehaviour
{
    [SerializeField] Transform goal;
    [SerializeField] float speed;
    [SerializeField] float rotSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] float breakAngle;

    // Update is called once per frame
    void Update()
    {
        Vector3 lookatGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
        Vector3 direction = lookatGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);

        //speed = Mathf.Clamp(speed+(acceleration*Time.deltaTime), minSpeed, maxSpeed);
        if(Vector3.Angle(goal.forward, this.transform.forward) > breakAngle && speed > 2)
        {
            speed = Mathf.Clamp(speed - (deceleration * Time.deltaTime), minSpeed, maxSpeed);
            Debug.Log("SLOW");
        }
        else
        {
            speed = Mathf.Clamp(speed + (acceleration * Time.deltaTime), minSpeed, maxSpeed);
            Debug.Log("FAST");
        }
        
        this.transform.Translate(0, 0, speed);
    }
}
