using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{

    private float timeBtwSpawns;
    public float baseTimeBtwSpawns;

    public GameObject echo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerController>().getDashing()) {
            if (timeBtwSpawns <= 0) {
                GameObject temp = (GameObject)Instantiate(echo, new Vector3(0,-50, 0), echo.transform.rotation);
                if (GetComponent<PlayerController>().getFacingLeft())// facing left
                    temp.GetComponent<SpriteRenderer>().flipX = true;
                else
                    temp.GetComponent<SpriteRenderer>().flipX = false;

                //GameMaster.gm_script.SpawnObject("Player_Echo", transform.position, echo.transform.rotation, gameObject);
                //Instantiate(temp, transform.position, echo.transform.rotation);
                temp.transform.position = transform.position;
                Destroy(temp, .15f);
                timeBtwSpawns = baseTimeBtwSpawns;
            }
            else {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
