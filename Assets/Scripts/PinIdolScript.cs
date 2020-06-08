using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinIdolScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pin;
    [SerializeField]
    private GameObject trapSpawn;
        
    private GameObject player;
    private float dist;
    private float spikeTimer, spikeBase;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spikeTimer = spikeBase = 3f;
        health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= 12)
        {
            spikeTimer -= Time.deltaTime;
            if (spikeTimer <= 0)
            {
                Instantiate(pin, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
                spikeTimer = spikeBase;
            }
        }
        else
            spikeTimer = spikeBase;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player_Weapon")
        {
            health--;
            if (health <= 0)
            {
                Instantiate(trapSpawn, transform.position, Quaternion.identity);
                GameMaster.gm_script.decNumPinIdols();
                gameObject.SetActive(false);
            }
        }
    }
}
