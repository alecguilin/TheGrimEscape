using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBro : MonoBehaviour
{
    [SerializeField]
    private GameObject fireball;
    [SerializeField]
    private GameObject firewall;

    private GameObject player;
    private Rigidbody rb;
    private int state;
    private float dist; //distance from player
    private float moveSpeed;
    private Vector3 targetMovePos;
    private float fireballTimer, fireballBase;
    private float firewallTimer, firewallBase;
    private int count; //number of fireballs launched before moving to next state
    private bool moveToPlayer;
    private bool moveFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameMaster.gm_script.getPlayerGameObj();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        state = 0;
        dist = 0f;
        moveSpeed = 40f;
        targetMovePos = new Vector3(0, 0, 0);
        firewallTimer = 0f;
        firewallBase = 2.5f;
        fireballTimer = 0f;
        fireballBase = .8f;
        moveToPlayer = false;
        moveFromPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.transform.position);

        switch (state) // FSM
        {
            case 0: //determine state
                MovementChecker();
                break;

            case 1: //fireball
                Fireball();
                break;

            case 2: //firewall
                Firewall();
                break;

            case 3: //move toward player
                MoveTowardPlayer();
                break;

            case 4: //move away from player
                MoveAwayFromPlayer();
                break;
        }

    }

    private void FixedUpdate() {
        if (moveToPlayer) {
            Vector3 dir1 = (player.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + dir1 * moveSpeed * Time.deltaTime);
        }
        if (moveFromPlayer) {
            Vector3 dir2 = (player.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position - dir2 * moveSpeed * Time.deltaTime);
        }

    }

    //MovementChecker checks if FireBro needs to move to or away from player. Starts attack otherwise
    private void MovementChecker()
    {
        if (dist > 35)
            state = 3;
        else if (dist < 25)
            state = 4;
        else
            state = 2;
    }

    //Moves toward player
    private void MoveTowardPlayer()
    {
        if (dist <= 35) {
            state = 2;
            moveToPlayer = false;
        }
        else {/*
            Vector3 dir1 = (player.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + dir1 * moveSpeed * Time.deltaTime);*/
            moveToPlayer = true;
        }
    }

    //Moves away from player
    private void MoveAwayFromPlayer()
    {
        if (dist >= 25) {
            state = 2;
            moveFromPlayer = false;
        }
        else {/*
            Vector3 dir2 = (player.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position - dir2 * moveSpeed * Time.deltaTime);*/
            moveFromPlayer = true;
        }
    }

    //Shoots three Fireballs
    private void Fireball()
    {
        fireballTimer -= Time.deltaTime;

        if (fireballTimer < 0)
        {
            Instantiate(fireball, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
            FindObjectOfType<AudioManager>().Play("Fireball");
            //GameMaster.gm_script.SpawnObject("Fireball", transform.position, Quaternion.LookRotation(player.transform.position - transform.position), this.gameObject);
            fireballTimer = fireballBase;
            count++;
        }

        if (count >= 3)
        {
            fireballTimer = fireballBase;
            count = 0;
            state = 0;
        } 
    }

    private void Firewall()
    {
        firewallTimer -= Time.deltaTime;

        if (firewallTimer < 0)
        {
            FindObjectOfType<AudioManager>().Play("Firewall");

            Instantiate(firewall, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
            var a = Instantiate(firewall, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
            var b = Instantiate(firewall, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
            var c = Instantiate(firewall, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));
            var d = Instantiate(firewall, transform.position, Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up));


            //GameMaster.gm_script.SpawnObject("Firewall", transform.position, Quaternion.LookRotation(player.transform.position - transform.position), this.gameObject);
            //var a = GameMaster.gm_script.SpawnObject("Firewall", transform.position, Quaternion.LookRotation(player.transform.position - transform.position), this.gameObject);
            //var b = GameMaster.gm_script.SpawnObject("Firewall", transform.position, Quaternion.LookRotation(player.transform.position - transform.position), this.gameObject);
            //var c = GameMaster.gm_script.SpawnObject("Firewall", transform.position, Quaternion.LookRotation(player.transform.position - transform.position), this.gameObject);
            //var d = GameMaster.gm_script.SpawnObject("Firewall", transform.position, Quaternion.LookRotation(player.transform.position - transform.position), this.gameObject);

            a.transform.Rotate(0, 20, 0);
            b.transform.Rotate(0, 40, 0);
            c.transform.Rotate(0, -20, 0);
            d.transform.Rotate(0, -40, 0);

            firewallTimer = firewallBase;
            state = 1;
        }
    }

    
}
