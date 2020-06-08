using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Light light;
    private float intensity, baseIntensity;
    private bool switchL;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        switchL = false;
        baseIntensity = 1;
        intensity = baseIntensity;
        speed = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        radiate();
    }

    private void radiate() {
        light.intensity = intensity;
        if (intensity < 2 && !switchL) {
            intensity += Time.deltaTime * speed;
        }
        else {
            switchL = true;
        }
        if (switchL && intensity > 0) {
            intensity -= Time.deltaTime * speed;
        }
        else
            switchL = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            GameMaster.gm_script.IncrementScore();
            Destroy(gameObject);
        }
    }
}
