using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_ArcherEnemy : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private GameObject arrow;
    private Rigidbody rb;
    [SerializeField]
    private GameObject sprite;
    private int state;
    private float distance;
    private float moveSpeed;
    private float shootFreq, elapsedTime;
    private Vector3 playerPos, offset;
    private float runTimer, baseRun;
    private float randRot;
    private Quaternion fixedRotate;

    private string arrow_str = "Arrow";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //player = GameMaster.gm_script.getPlayerGameObj();
        player = GameObject.FindGameObjectWithTag("Player");
        state = 0;
        moveSpeed = 22f;
        shootFreq = 2f;
        baseRun = .5f;
        fixedRotate = Quaternion.Euler(75f,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        switch (state)
        {
            case 0: //run
                if (distance < 20)
                {
                    runTimer += Time.deltaTime;
                    if (runTimer > baseRun)
                    {
                        randRot = Random.Range(0f,360f);
                        transform.rotation = Quaternion.Euler(0, randRot, 0);
                        runTimer = 0;
                    }
                    else
                    {
                        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);    
                        
                    }
                }
                else
                {
                    runTimer = 0; 
                    playerPos = player.transform.position;
                    state = 1;
                }
                break;
            case 1: //attack
                if(distance < 20)
                {
                    state = 0;
                    elapsedTime = 0;
                }
                elapsedTime += Time.deltaTime;
                if (elapsedTime > shootFreq)
                {
                    ArrowAttack();
                    elapsedTime = Random.Range(0f, shootFreq * .80f);
                    state = 0;
                }
                break;
        }
    }

    private void LateUpdate()
    {
        sprite.transform.rotation = fixedRotate;
    }

    private void ArrowAttack()
    {
        //Instantiate(arrow, transform.position, Quaternion.LookRotation(playerPos - transform.position, Vector3.up));
        FindObjectOfType<AudioManager>().Play("ArrowShoot");
        GameMaster.gm_script.SpawnObject(arrow_str, transform.position, Quaternion.LookRotation(playerPos - transform.position), this.gameObject);
        var a = GameMaster.gm_script.SpawnObject(arrow_str, transform.position, Quaternion.LookRotation(playerPos - transform.position), this.gameObject);
        var b = GameMaster.gm_script.SpawnObject(arrow_str, transform.position, Quaternion.LookRotation(playerPos - transform.position), this.gameObject);
        //var a = Instantiate(arrow, transform.position, Quaternion.LookRotation(playerPos - transform.position, Vector3.up));
        //var b = Instantiate(arrow, transform.position, Quaternion.LookRotation(playerPos - transform.position, Vector3.up));
        a.transform.Rotate(0, 15, 0);
        b.transform.Rotate(0, -15, 0);
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player_Weapon") {
            //GameMaster.gm_script.decNumEnemies();
            //Destroy(gameObject);
        }
    }


}
