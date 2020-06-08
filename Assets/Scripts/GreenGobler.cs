using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGobler : MonoBehaviour
{
    private GameObject player;
    public int health;
    private Rigidbody rb;
    private float shootTimer;
    private float baseShootTimer;
    private bool knockback;
    private Vector3 knockbackDir;
    public SpriteRenderer sr;
    public List<Sprite> sprites;
    [SerializeField]
    private GameObject poisonpuddle;
    private float poisontrapSpawnTimer;
    private float poisontrapBaseSpawnTimer;
    private float moveSpeed;
    private float stopTimer;
    private float baseStopCountDown;
    private float stopCountdown;
    private float baseStopTime;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        baseShootTimer = 3.50f;
        shootTimer = Random.Range(baseShootTimer - 2, baseShootTimer);
        moveSpeed = 3.5f;
        baseStopCountDown = 5f;
        stopCountdown = Random.Range(baseStopCountDown - 2f, baseStopCountDown);
        baseStopTime = 1.5f;
        stopTimer = baseStopTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(shootTimer <= 0.65f)
            sr.sprite = sprites[1];
        if(shootTimer >= 0.5f)
            sr.sprite = sprites[0];

        if (shootTimer <= 0) {
            sr.sprite = sprites[1];
            shootTimer = Random.Range(baseShootTimer-2, baseShootTimer);
            FindObjectOfType<AudioManager>().Play("GreenGoblerShoot");
            var a = GameMaster.gm_script.SpawnObject("GreenGoblerProj", transform.position, new Quaternion(0,0,0,0), this.gameObject);
            var b = GameMaster.gm_script.SpawnObject("GreenGoblerProj", transform.position, new Quaternion(0, 0, 0, 0), this.gameObject);
            var c = GameMaster.gm_script.SpawnObject("GreenGoblerProj", transform.position, new Quaternion(0, 0, 0, 0), this.gameObject);
            var d = GameMaster.gm_script.SpawnObject("GreenGoblerProj", transform.position, new Quaternion(0, 0, 0, 0), this.gameObject);
            var e = GameMaster.gm_script.SpawnObject("GreenGoblerProj", transform.position, new Quaternion(0, 0, 0, 0), this.gameObject);
            var f = GameMaster.gm_script.SpawnObject("GreenGoblerProj", transform.position, new Quaternion(0, 0, 0, 0), this.gameObject);

            a.transform.Rotate(0, 0, 0);
            b.transform.Rotate(0, 60, 0);
            c.transform.Rotate(0, 120, 0);
            d.transform.Rotate(0, 180, 0);
            e.transform.Rotate(0, 240, 0);
            f.transform.Rotate(0, 300, 0);
        }
        if(stopCountdown <= 0) {
            if (stopTimer > 0)
                stopTimer -= Time.deltaTime;
            else {
                stopCountdown = Random.Range(baseStopCountDown - 2f, baseStopCountDown);
                stopTimer = baseStopTime;
            }
        }
        shootTimer -= Time.deltaTime;
        stopCountdown -= Time.deltaTime;
    }

    private void FixedUpdate() {
        if (stopCountdown > 0) {
            Vector3 moveDir = Vector3.Normalize(transform.position - player.transform.position);
            rb.MovePosition(transform.position - moveDir * Time.deltaTime * moveSpeed);
            //rb.velocity = (transform.position - moveDir * Time.deltaTime * moveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other) {

    }

}
