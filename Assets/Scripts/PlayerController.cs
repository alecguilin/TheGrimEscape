using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Animator animator;
    private Vector3 lastMoveDir;
    private Rigidbody rb;
    [SerializeField]
    private float dashDistance;
    [SerializeField]
    private float baseDashDuration;
    private float dashDurationTimer;
    private float dashWaitTimer;
    private float baseDashWaitTimer;
    private bool facingLeft;
    private bool dashing;
    private bool canDash;
    public bool invincible;
    public int health;
    public float speed;
    float moveX = 0;
    float moveZ = 0;
    
    public Texture hp;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<HealthScript>().getHP();
        dashDurationTimer = baseDashDuration;
        dashing = false;
        invincible = false;
        canDash = true;
       // dashDistance = 7500;
        baseDashDuration = .0575f;
        speed = 30f;
        Physics.IgnoreLayerCollision(11, 11);
        baseDashWaitTimer = .33f;
        dashWaitTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleDash();
        FlipX();
    }

    void OnGUI() {
        for (int i = 0; i < GetComponent<HealthScript>().getHP(); i++)
            GUI.DrawTexture(new Rect(i * 64 + 10, 10, 64, 64), hp);
    }

    private void HandleMovement() {
        moveX = 0;
        moveZ = 0;
        if (Input.GetAxisRaw("Horizontal") > 0) { //move right
            moveX = 1f;
        }
        if (Input.GetAxisRaw("Horizontal") < 0) { //move left
            moveX = -1f;
        }
        if (Input.GetAxisRaw("Vertical") > 0) { //move up
            moveZ = 1f;
        }
        if (Input.GetAxisRaw("Vertical") < 0) { //move down
            moveZ = -1f;
        }

        bool isIdle = moveX == 0 && moveZ == 0;
        if (isIdle) {
            //***Play idle animation here: playerAnimatorScript.PlayIdleAnim(lastMoveDir);
        }
        else {

            Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized; //normalize to ensure speed is constant. 

            Vector3 targetMovePos = transform.position + moveDir * speed * Time.deltaTime;
            lastMoveDir = moveDir; //for future use for animators
            rb.position = targetMovePos;

        }
    }

    private void HandleDash() {	
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown("left shift")) && !dashing && canDash) {
            dashing = true;
            canDash = false;
            //StartCoroutine(DodgeAnim());
            //rb.position += lastMoveDir * dashDistance;\
            FindObjectOfType<AudioManager>().Play("dash");
            rb.AddForce(lastMoveDir.normalized * dashDistance, ForceMode.Force);

        }
        else if(dashing) {
            dashWaitTimer = baseDashWaitTimer;
            if (dashDurationTimer < 0) {
                dashing = false;
                rb.velocity = Vector3.zero;
                dashDurationTimer = baseDashDuration;
                //GetComponent<BoxCollider>().enabled = true;
            }
            else {
                dashDurationTimer -= Time.deltaTime;
                //GetComponent<BoxCollider>().enabled = false;
            }
        }
        if (!canDash) {
            if(dashWaitTimer <= 0) {
                canDash = true;
                dashWaitTimer = baseDashWaitTimer;

            }
            else
                dashWaitTimer -= Time.deltaTime;

        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Arrow" || other.tag == "Enemy" ) {
            //health--;
        }
        if( other.tag == "Trap" && !getDashing()) {
            decHealthAndInvokeIFrames();
        }
    }

    private void FlipX() {
        if (weapon.GetComponent<WeaponScript>().angle > 90 || weapon.GetComponent<WeaponScript>().angle < -90) { //left
            sr.flipX = true;
            facingLeft = true;
        }
        else if (weapon.GetComponent<WeaponScript>().angle <= 90 || weapon.GetComponent<WeaponScript>().angle >= -90) {//right
            sr.flipX = false;
            facingLeft = false;
        }
        
    }

    void ResetInvulnerability() {
        invincible = false;
    }

    IEnumerator DodgeAnim() {
        animator.SetTrigger("Dodge");
        yield return new WaitForSeconds(.1f);
    }

    public void decHealthAndInvokeIFrames() {
        if (!invincible) {
            FindObjectOfType<AudioManager>().Play("PlayerHit");
            //health--;
            GetComponent<HealthScript>().decHP();
        StartCoroutine(IFramesAnim());
        invincible = true;
        Invoke("ResetInvulnerability", 1.1f);
        }

    }

    IEnumerator IFramesAnim() {
        animator.SetTrigger("IFrames");
        yield return new WaitForSeconds(.01f);
    }
    public bool getDashing() { return dashing; }
    public bool getFacingLeft() { return facingLeft; }
}
