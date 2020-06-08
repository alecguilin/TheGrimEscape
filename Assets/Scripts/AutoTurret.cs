using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : MonoBehaviour
{
    public GameObject bullet;
    public Transform turret;
    public float baseFireTimer;
    private float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = baseFireTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireTimer > 0) {
            fireTimer -= Time.deltaTime;
        }
        if(fireTimer <= 0) {
            Instantiate(bullet, turret.position, turret.rotation);
            fireTimer = baseFireTimer;
        }
    }
}
