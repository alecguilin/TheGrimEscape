using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinheadScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pin;

    private GameObject player;
    private Rigidbody rb;
    private int state;
    private float dist;
    private float moveSpeed;
    private float spikeTimer, spikeBase;
    private float teleTimer, teleBase;
    private float atkTimer, atkBase;
    private float dispelTimer;
    private Vector3 targetPos;

    private int idolCount;
    private int numPins;
    private bool damageable;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        state = 0;
        moveSpeed = 8f;
        spikeTimer = spikeBase = 2.5f;
        teleTimer = teleBase = 1.5f;
        atkTimer = atkBase = .65f;
        idolCount = 4;
        numPins = 10;
        damageable = false;
        health = 13;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameMaster.gm_script.getNumPinIdols() < idolCount)
        {
            idolCount = GameMaster.gm_script.getNumPinIdols();
            Strengthen();
        }

        dist = Vector3.Distance(transform.position, player.transform.position);
        switch (state) //FSM
        {
            case 0:
                DistCheck();
                break;
            case 1:
                SpikeRing();
                break;
            case 2:
                HuntPlayer();
                break;
            case 3:
                Teleport();
                break;
            case 4:
                AtkPause();
                break;
        }
    }

    private void DistCheck()
    {
        if (dist <= 30)
            state = 4;
        else
        {
            state = 3;
            targetPos = player.transform.position;
        }
    }

    private void SpikeRing()
    {
        for(int i=0; i<numPins; i++)
        {
            //Debug.Log("Spawn Pin"+i);
            var a = Instantiate(pin, transform.position, Quaternion.identity);
            a.transform.Rotate(0, i * (360/numPins), 0);
        }

        state = 2;
    }

    private void HuntPlayer()
    {
        spikeTimer -= Time.deltaTime;
        if (spikeTimer <= 0)
        {
            state = 0;
            spikeTimer = spikeBase;
        }
        else
        {
            Vector3 dir1 = (player.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + dir1 * moveSpeed * Time.deltaTime);
        }
    }

    private void Teleport()
    {
        teleTimer -= Time.deltaTime;
        if(teleTimer <= 0)
        {
            rb.MovePosition(targetPos);
            teleTimer = teleBase;
            state = 1;
        }
    }

    private void AtkPause()
    {
        atkTimer -= Time.deltaTime;
        if(atkTimer <= 0)
        {
            atkTimer = atkBase;
            state = 1;
        }
    }

    private void Strengthen()
    {
        spikeTimer -= .2f;
        switch (idolCount)
        {
            case 4:
                numPins = 10;
                moveSpeed = 8f;
                break;
            case 3:
                numPins = 12;
                moveSpeed = 10f;
                break;
            case 2:
                numPins = 15;
                moveSpeed = 13f;
                break;
            case 1:
                numPins = 18;
                moveSpeed = 18f;
                break;
            case 0:
                numPins = 24;
                moveSpeed = 28f;
                damageable = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if(damageable && (c.tag == "Player_Weapon" || c.tag == "Scar_Bullet"))
        {
            health--;
            if (health <= 0)
            {
                GameMaster.gm_script.decNumEnemies();
                gameObject.SetActive(false);
            }
        }
    }
}
