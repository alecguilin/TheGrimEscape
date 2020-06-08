using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDetector : MonoBehaviour
{
    public bool activated;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isActivated() {
        return activated;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
            activated = true;
    }
}
