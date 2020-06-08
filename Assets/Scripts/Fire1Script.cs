using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire1Script : MonoBehaviour
{
    private GameObject player; //player game object
    private float speed; //speed fireball flies
    private bool straight;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameMaster.gm_script.getPlayerGameObj();
        player = GameObject.FindGameObjectWithTag("Player");
        speed = 12f;
        straight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 10)
            straight = true;

        Move();
    }

    private void Move()
    {

        //transform.position += transform.forward * speed * Time.deltaTime; 
        if (straight)
            transform.position += transform.forward * speed * Time.deltaTime; // move forward
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.position, player.transform.position, 5f, 360f));
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, speed*Time.deltaTime);
            transform.LookAt(player.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If gameObj is a fireball, deactivate on player and decrement player hp
        if (other.tag == "Player" && gameObject.tag == "Fire1" && !player.GetComponent<PlayerController>().getDashing())
        {
            player.GetComponent<PlayerController>().decHealthAndInvokeIFrames();
            gameObject.SetActive(false);
        }
        //If gameObj is a fireball Destroy on room structures
        else if ((other.tag == "Wall" || other.tag == "door") && gameObject.tag == "Fire1")
        {
            gameObject.SetActive(false);
        }
    }
}
