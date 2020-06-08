using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDude : MonoBehaviour {
    public GameObject Player;
    public LayerMask Mask;
    public SpriteRenderer sr;
    public Rigidbody rb;
    public List<Sprite> sprites;
    public int state;
    private enum Direction { UP, DOWN, LEFT, RIGHT };
    private Vector3 chargeDir;
    private float chargeWaitTimer;
    private float baseChargeWaitTimer;
    private float hitboxWidth;
    private float chargeSpeed;
    private float chargePower;
    private bool playOnce;
    // Start is called before the first frame update
    void Start() {
        state = 0;
        rb = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponentInChildren<SpriteRenderer>();
        hitboxWidth = 2.25f;
        baseChargeWaitTimer = 1.5f;
        chargeWaitTimer = baseChargeWaitTimer;
        chargeSpeed = 100;
        chargePower = 2500;
        playOnce = false;
    }

    // Update is called once per frame
    void Update() {
        switch (state) {
            case 0: //Wait until player crosses the hitboxes
                    //changeColor();
                playOnce = false;
                sr.sprite = sprites[0];
                if (FindChargeDir())
                    state = 1;
                break;
            case 1: //Charging
                //changeColor();
                sr.sprite = sprites[2];
                Charge();
                chargeWait();
                break;
            case 2: //Charge wait-time
                sr.sprite = sprites[1];
                chargeWait();
                break;

        }
    }

    private void chargeWait() {
        if(chargeWaitTimer > 0) {
            chargeWaitTimer -= Time.deltaTime;
        }
        else {
            chargeWaitTimer = baseChargeWaitTimer;
            state = 0;
        }
    }

    private void changeColor() {
        if(state == 0) {
            sr.sprite = sprites[0];
        }
        else if(state == 1) {
            sr.sprite = sprites[2];
        }
    }
    private bool FindChargeDir() {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, hitboxWidth, transform.forward, out hit, 100, Mask)) { //UP
            chargeDir = Vector3.forward;
            return true;
        }
        else if (Physics.SphereCast(transform.position, hitboxWidth, -transform.forward, out hit, 100, Mask)) { //DOWN
            chargeDir = -Vector3.forward;
            return true;
        }
        else if (Physics.SphereCast(transform.position, hitboxWidth, -transform.right, out hit, 100, Mask)) { //LEFT
            chargeDir = -Vector3.right;
            return true;
        }
        else if (Physics.SphereCast(transform.position, hitboxWidth, transform.right, out hit, 100, Mask)) { //RIGHT
            chargeDir = Vector3.right;
            return true;
        }
        return false;
    }

    private void Charge() {
        if (!playOnce) {
            FindObjectOfType<AudioManager>().Play("CubeDudeCharge");
            playOnce = true;
        }
        rb.MovePosition(transform.position +chargeDir * Time.deltaTime * chargeSpeed);
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Player") {
            collision.rigidbody.AddForce(chargeDir * chargePower);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            state = 2;
        }
        else {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.rigidbody.velocity = Vector3.zero;
            state = 2;
        }
    }


}
