using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBallScript : MonoBehaviour
{
    public GameObject laser; //laser gameObj
    private int state = 0; //State for FSM
    public float rotateSpeed; //Speed of rotation
    private float laserWait; //Wait time till shoot for the laser
    private float baseLaserWait;
    private float laserRotationTimer; //Time for eye to rotate
    private float baseLaserRotationTimer;
    private bool shooting; //Is the laser currently shooting
    // Start is called before the first frame update
    void Start()
    {
        shooting = false;
        baseLaserWait = 1.0f;
        laserWait = baseLaserWait;
        laserRotationTimer = 5f;
        baseLaserRotationTimer = laserRotationTimer;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case 0: //Wait until laser ready to shoot
                WaitUntilLaserShoots();
                break;
            case 1: //Laser shooting while rotating
                RotateAndShoot();
                break;

        }
    }

    private void WaitUntilLaserShoots() {
        laser.SetActive(false);
        if (laserWait >= 0) //Decrease timer
            laserWait -= Time.deltaTime;
        else //Change to next state
            state = 1;
    }

    private void RotateAndShoot() {
        laser.SetActive(true);
        //StartCoroutine(Turn360(1f));
        //Debug.Log(laserRotationTimer);
        if (laserRotationTimer > 0) {
            laserRotationTimer -= Time.deltaTime;
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else {
            state = 0;
            laserRotationTimer = baseLaserRotationTimer;
            laserWait = baseLaserWait;
        }
    }

    IEnumerator Turn360(float duration) {
        Quaternion startRot = transform.rotation;
        float t = 0f;
        while( t < duration) {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 360f, Vector3.up);
            yield return null;
        }
        transform.rotation = startRot;
    }


}
