using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire2Script : MonoBehaviour
{
    private GameObject player; //player game object
    private float lifeTime, speed;
    private bool alive;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;
        lifeTime = 3f;
        speed = 20f;
        alive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
                gameObject.SetActive(false);
        }
        else
        {
            transform.position += transform.forward * speed * Time.deltaTime; // move forward
            if (Vector3.Distance(transform.position, startPos) > 10)
                alive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If gameObj is a fireball, deactivate on player and decrement player hp
        if (other.tag == "Player" && gameObject.tag == "Fire2" && !player.GetComponent<PlayerController>().getDashing())
        {
            player.GetComponent<PlayerController>().decHealthAndInvokeIFrames();
            gameObject.SetActive(false);
        }
        //If gameObj is a Arrow Destroy on room structures
        else if ((other.tag == "Wall" || other.tag == "door") && gameObject.tag == "Fire2")
        {
            gameObject.SetActive(false);
        }
    }
}
