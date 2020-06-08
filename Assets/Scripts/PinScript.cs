using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScript : MonoBehaviour
{
    public GameObject player; //player game object
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 14f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If gameObj is an pin, deactivate on player and decrement player hp
        if (other.tag == "Player" && gameObject.tag == "Arrow" && !player.GetComponent<PlayerController>().getDashing())
        {
            player.GetComponent<PlayerController>().decHealthAndInvokeIFrames();
            gameObject.SetActive(false);
        }
        //If gameObj is a pin, Destroy on room structures
        else if ((other.tag == "Wall" || other.tag == "door") && gameObject.tag == "Arrow")
        {
            gameObject.SetActive(false);
        }
    }
}
