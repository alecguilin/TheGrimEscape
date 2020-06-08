using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public List<GameObject> attractedTo;
    public float strengthOfAttraction = 5.0f;
    public float dist = 10.0f;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        for (int i = 0; i < attractedTo.Count; i++)
        {
            var distance = Vector3.Distance(attractedTo[i].transform.position, gameObject.transform.position);
            if (distance < dist)
            {
                Vector3 direction = attractedTo[i].transform.position - transform.position;
                gameObject.GetComponent<Rigidbody>().AddForce(strengthOfAttraction * direction);
            }
        }
    }

}
