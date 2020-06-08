using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBackScript : MonoBehaviour
{
    public GameObject parentDoor; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        //Open the door if
        //Player collides while the door is closed and there are no enemies alive
        if (col.gameObject.tag == "Player" && !parentDoor.GetComponent<DoorScript>().GetHasBeenOpened() && GameMaster.gm_script.getNumEnemies() <= 0)
        {
            parentDoor.GetComponent<DoorScript>().DoorOpenUp();
        }
    }
}
