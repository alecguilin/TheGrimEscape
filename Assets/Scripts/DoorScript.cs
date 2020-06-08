using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GameMaster.cs;

public class DoorScript : MonoBehaviour
{
    public GameObject door;
    private bool doorClosed;
    public List<GameObject> doorConditions;
    private bool HasBeenOpened;
    public bool canOpen;
    // Start is called before the first frame update
    void Start()
    {
        doorClosed = true;
        canOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && doorClosed && checkDoorConditions() && canOpen	)// && GameMaster.gm_script.getRoomStatus() == true)
        {
	        DoorOpenUp();
            //door.transform.Translate(Vector3.up * 10, Space.World);

            //GetComponentInChildren<AudioSource>().Play();
            //transform.parent.GetComponentInChildren<AudioSource>().Play();
        }
    }
    public void DoorOpenUp(){
        doorClosed = false;
        FindObjectOfType<AudioManager>().Play("DoorOpen");
        HasBeenOpened = true;
        door.transform.Translate(Vector3.up * 10, Space.World);
    }
    public void DoorCloseDown() {
        if (!doorClosed) {
            doorClosed = true;
            FindObjectOfType<AudioManager>().Play("DoorClose");
            door.transform.Translate(Vector3.up * -10, Space.World);
        }
    }
    private bool checkDoorConditions() {
	if(doorConditions.Count == 0)
		return true;
	else{
            for(int i= 0; i < doorConditions.Count; i++) {
                if (!doorConditions[i].GetComponent<RoomMaster>().GetRoomCompleted())
                    return false;
            }
	}
        return true;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //transform.Translate(Vector3.down * 10, Space.World);
        }
    }


    public bool GetHasBeenOpened() { return HasBeenOpened; }
    public void canOpenNow() { canOpen = true; }
}
