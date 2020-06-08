using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject healthbar;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private Camera camera;
    public float health;
    public SpriteRenderer sr;
    public Animator animator;
    public GameObject bullet;
    public float xPos;
    public float zPos;
    public float speed;
    public float baseDodgeTimer;
    public float dodgeDist;
    private float dodgeTimer;
    public Rigidbody rb;
    private Vector3 movement;
    private bool doorOpen = false;
    public int doorSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        dodgeTimer = baseDodgeTimer;
        dodgeDist = 5;
        rb.GetComponent<Rigidbody>();
        health = healthbar.transform.localScale.x;
        transform.rotation = Quaternion.Euler(GameMaster.gm_script.GetXRot(), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
   
        Fire();
        Blink();
        FlipX();
    }

    

    private void FixedUpdate() {

        MovePlayer();
    }
    
    private void MovePlayer() {
        xPos = Input.GetAxisRaw("Horizontal");
        zPos = Input.GetAxisRaw("Vertical");
        movement = new Vector3(xPos, 0.0f, zPos) * speed;
        rb.velocity = movement;
    }

    private void FlipX() {
        if (weapon.GetComponent<WeaponScript>().angle > 90 || weapon.GetComponent<WeaponScript>().angle <-90) { //left
            sr.flipX = true;
        }
        else if (weapon.GetComponent<WeaponScript>().angle <= 90 || weapon.GetComponent<WeaponScript>().angle >= -90)//right
            sr.flipX = false;
    }
    private void Blink() {
        if (dodgeTimer >= 0)
            dodgeTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(1) && dodgeTimer <= 0) {
            StartCoroutine(Dodge());
            /*if (Input.mousePosition.x < Screen.width / 2 )
                transform.position = new Vector3(transform.position.x - dodgeDist, transform.position.y, transform.position.z);
            else if (Input.mousePosition.x >= Screen.width / 2)
                transform.position = new Vector3(transform.position.x + dodgeDist, transform.position.y, transform.position.z);
            */
            Vector3 target = new Vector3(transform.position.x + dodgeDist, transform.position.y, transform.position.z);
            if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") == 0) //right blink
                transform.position = new Vector3(transform.position.x + dodgeDist, transform.position.y, transform.position.z);
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") == 0) //left blink
                transform.position = new Vector3(transform.position.x - dodgeDist, transform.position.y, transform.position.z);
            else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0) //down blink
                transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z - dodgeDist);
            else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0) // up blink
                transform.position = new Vector3(transform.position.x , transform.position.y , transform.position.z + dodgeDist);
            else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0) //top right blink
                transform.position = new Vector3(transform.position.x + dodgeDist, transform.position.y , transform.position.z + dodgeDist);
            else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0) //bottom right blink
                transform.position = new Vector3(transform.position.x + dodgeDist, transform.position.y , transform.position.z - dodgeDist);
            else if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0) //top left blink
                transform.position = new Vector3(transform.position.x - dodgeDist, transform.position.y , transform.position.z + dodgeDist);
            else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0) //bottom left
                transform.position = new Vector3(transform.position.x - dodgeDist, transform.position.y , transform.position.z - dodgeDist);


            dodgeTimer = baseDodgeTimer;    
        }
    }
    private void Fire() {
        if (Input.GetKeyDown("space")) {
            Instantiate(bullet, transform.position, bullet.transform.rotation);
        }
    }

    IEnumerator Dodge() {
        animator.SetTrigger("Dodge");
        yield return new WaitForSeconds(.1f);
    }
    /*
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "door" && doorOpen == false)
        {
            Debug.Log("DOOR OPEN");
            col.gameObject.transform.Translate(Vector3.up * 5, Space.World);
            doorOpen = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "door")
        {
            Debug.Log("DOOR CLOSED");
            //col.gameObject.transform.Translate(Vector3.down * 5, Space.World);
        }
    }
    */
    private void OnTriggerEnter(Collider other) {

    }

    
}
