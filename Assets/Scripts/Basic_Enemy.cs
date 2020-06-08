using UnityEngine;

public class Basic_Enemy : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    private int state; //0 patrol, 1 follow, 2 attack
    private Vector3 targetPos;
    private float attackTimer, baseTimer;
    [SerializeField]
    private Rigidbody rb;
    public bool charge;
    
    // Start is called before the first frame update
    void Start() {
        state = 0;
        gameObject.GetComponent<Rigidbody>();
        baseTimer = .12f;
        attackTimer = baseTimer;
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy)
        {
            switch (state)
            {
                case 0:
                    Debug.Log("Do Nothing, State ");
                    if (Vector3.Distance(transform.position, player.transform.position) < 25)
                    {
                        state = 1;
                    }
                    break;
                case 1:
                    Debug.Log("In State 1, follow player");
                    if (Vector3.Distance(transform.position, player.transform.position) > 15 && Vector3.Distance(transform.position, player.transform.position) < 25)
                    {
                        //follows player slowly until radius reached
                        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.25f * Time.deltaTime);

                    }
                    else if (Vector3.Distance(transform.position, player.transform.position) >= 25)
                        state = 0;

                    else
                    {
                        targetPos = player.transform.position;
                        charge = true;
                        state = 2;
                        //do dash attack;
                    }
                    break;
                case 2:
                    Debug.Log("In State 2, attack player");
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 25f * Time.deltaTime);
                    attackTimer -= Time.deltaTime;
                    if (attackTimer <= 0)
                    {
                        attackTimer = baseTimer;
                        state = 0;
                    }
                    break;

            }
        }

    }

    private void EnemyStuff()
    {
        if (!charge)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 15)
            {
                //follows player slowly until radius reached
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.25f * Time.deltaTime);

            }
            else
            {
                targetPos = player.transform.position;
                charge = true;
                state = 2;
                //do dash attack;
            }
        }
        if (charge && attackTimer > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 55f * Time.deltaTime);
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer <= 0)
        {
            charge = false;
        }
        /*attackTimer -= Time.deltaTime;

        if (attackTimer <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.5f * Time.deltaTime);
            //attackTimer = baseTimer;
        }*/
    }
    public void SpawnEnemy()
    {
        gameObject.SetActive(true);

    }

}