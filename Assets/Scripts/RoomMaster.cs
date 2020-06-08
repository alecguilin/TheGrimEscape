using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundaries {
    public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class EnemySpawnController {
    public string EnemyToSpawn;
    public int numToSpawn;
}
public class RoomMaster : MonoBehaviour
{
    public Boundaries bounds;
    public List<EnemySpawnController> e_spawn_controller;
    public bool roomCompleted;
    public int spawnAmount;
    public bool hasEntered;
    public List<GameObject> Spawn_Detectors;
    public bool hasSpawned;
    public List<GameObject> Doors;
    public bool doorsCanOpen;
    private bool didOnce;
    // Start is called before the first frame update
    void Start()
    {
        doorsCanOpen = false;
        roomCompleted = false;
        didOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasSpawned){ //spawn enemies{
            spawnRoomEnemies();
        }
        if (GameMaster.gm_script.getNumEnemies() <= 0 && hasSpawned) { //Enemies killed, room completed
            doorsCanOpen = true;
            roomCompleted = true;
            if (!didOnce) {
                GameMaster.gm_script.IncRoomsCleared();
                int playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>().getHP();
                if (playerHP<5 && playerHP > 0)
                    GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScript>().incHP();
                didOnce = true;
            }
	    }
        if(doorsCanOpen){
            for (int i = 0; i < Doors.Count; i++) {
                Doors[i].GetComponent<DoorScript>().canOpenNow();
            }
        }
    }

    private void spawnRoomEnemies() {
        if (isSpawnDetectorHit()) {
            CloseDoors();
            hasSpawned = true;
            EnemySpawn(spawnAmount);

        }
    }

    private void CloseDoors() {
        for (int i = 0; i < Doors.Count; i++) {
            Doors[i].GetComponent<DoorScript>().DoorCloseDown();
        }
    }

    private void EnemySpawn(int num)
    {
	    for(int numDiffEnemies = 0; numDiffEnemies < e_spawn_controller.Count; numDiffEnemies++){
            for (int i = 0; i < e_spawn_controller[numDiffEnemies].numToSpawn; i++)
            {
		        string e = e_spawn_controller[numDiffEnemies].EnemyToSpawn;
		        //Debug.Log(e);
                GameMaster.gm_script.SpawnObject(e, new Vector3(Random.Range(bounds.xMin, bounds.xMax), 2, Random.Range(bounds.zMin, bounds.zMax)), this.gameObject);
            }
	    }
    }

    private bool isSpawnDetectorHit() {
        for (int i = 0; i < Spawn_Detectors.Count; i++) {
            if (Spawn_Detectors[i].GetComponent<SpawnDetector>().isActivated()) {
                return true;
            }
        }
        return false;
    }
    public bool GetRoomCompleted(){ return roomCompleted;}
}
