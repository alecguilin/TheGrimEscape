using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorClosed = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && doorClosed)// && GameMaster.gm_script.getRoomStatus() == true)
        {
            Debug.Log("DOOR OPEN");
            transform.Translate(Vector3.up * 10, Space.World);
            doorClosed = false;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("DOOR CLOSED");
            //transform.Translate(Vector3.down * 10, Space.World);
        }
    }
}
