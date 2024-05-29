using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToDirection1 : MonoBehaviour
{
    [SerializeField] Vector3 direction = new Vector3(8f, 0f, -4f);
    [SerializeField] float movementSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
    }
}
