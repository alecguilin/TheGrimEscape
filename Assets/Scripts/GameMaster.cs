using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]

/// <ObjectPool>
/// Class which contains object pool properties
/// instantiates objects and deactivates them when program starts
/// reusable objects rather than using Instantiate/Destroy
/// </summary>
public class ObjectPool {
    public List<GameObject> object_pool; //List of object to be pooled
    public GameObject object_To_Spawn; //Game object of a specific pool
    public int amount_objects; //num to spawn into pool
    public string object_name; //name of object used for spawning
}

/// <summary>
/// Game Master is the main operator of the game. Controls various game states and properties
/// Useable by any object to get information
/// </summary>
public class GameMaster : MonoBehaviour
{
    public static GameMaster gm_script; //static link
    public GameObject Player; //Player game object
    private int xRotation; //rotation of the camera
    //////To be deleted
    public UnityEvent triggerss;
    public TextMeshProUGUI roomsText;
    public TextMeshProUGUI timeText;
    public int ScoreGoal;
    private int roomsCleared;
    private int numRoomsToClear;
    private int score;
    public int numEnemies;
    public int numPinIdols = 4; //number of idols remaining for Pinhead boss
    /////////

    // map
    private bool mapOpen = false;
    public GameObject map;

    public string LevelToLoad; //Name of next scene to load
    
    public List<GameObject>RoomMasterList; //List of all room masters in the scene

    /// Object Pools
    [SerializeField]
    public List<ObjectPool> gameobject_pools;
    /// 

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        gm_script = this;
        numEnemies = 0;
        xRotation = 75;
        score = 0;
        InitializeObjectPools();
        numRoomsToClear = RoomMasterList.Count;
        roomsCleared = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (mapOpen)
            {
                mapOpen = false;
                map.SetActive(false);
            }
            else
            {
                mapOpen = true;
                map.SetActive(true);
            }
        }

        timeText.text = "Time: " + Time.timeSinceLevelLoad; ;
        if (roomsCleared >= 8)
        {
            roomsText.text = "BOSS TIME!!";
        }
        else
        {
            roomsText.text = "Rooms Cleared: " + roomsCleared + " / " + numRoomsToClear;
        }
        if(ScoreGoal == score && LevelToLoad != "none") {
            SceneManager.LoadScene(LevelToLoad);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            triggerss.Invoke();
        }
        if (Input.GetKeyDown("l")) {
            SceneManager.LoadScene("Showcase");
        }
    }

    /// <InitializeObjectPools>
    /// Create all the objects from the object pool according to the corresponding amounts
    /// Loops through all objects from the objectpool list
    /// </summary>
    void InitializeObjectPools() {
        for (int i = 0; i < gameobject_pools.Count; i++) {
            for(int zi = 0; zi < gameobject_pools[i].amount_objects; zi++) {
                GameObject obj = (GameObject)Instantiate(gameobject_pools[i].object_To_Spawn);
                obj.SetActive(false);
                gameobject_pools[i].object_pool.Add(obj);
            }
        }
    }

    /// <SpawnObject>
    /// Spawn a game object from the pool
    /// </summary>
    /// <param name="obj_s">Name of the object</param>
    /// <param name="spawn_point">Where the object will spawn</param>
    /// <param name="caller">Gameobject calling SpawnObject</param>
    public GameObject SpawnObject(string obj_s, Vector3 spawn_point, GameObject caller) {
        GameObject obj = new GameObject();
        for (int i = 0; i < gameobject_pools.Count; i++) {
            if (obj_s == gameobject_pools[i].object_name) { // name matches
                for (int zi = 0; zi < gameobject_pools[i].amount_objects; zi++) {
                    if (!gameobject_pools[i].object_pool[zi].activeInHierarchy) {
                        obj = gameobject_pools[i].object_pool[zi].gameObject;
                        gameobject_pools[i].object_pool[zi].transform.position = spawn_point;
                        gameobject_pools[i].object_pool[zi].SetActive(true);
                        if (gameobject_pools[i].object_pool[zi].tag == "Enemy") {
                            numEnemies++;
                            gameobject_pools[i].object_pool[zi].GetComponent<HealthScript>().setHP(3);
                        }
                        zi = gameobject_pools[i].amount_objects;
                    }
                }
                i = gameobject_pools.Count;
            }
        }
        return obj;
    }
    /// <SpawnObject>
    /// Spawn a game object from the pool
    /// </summary>
    /// <param name="obj_s">Name of the object</param>
    /// <param name="spawn_point">Where the object will spawn</param>
    /// <param name="rotation">Rotation of the gameobject to be spawned
    /// <param name="caller">Gameobject calling SpawnObject</param>
    /// 
    public GameObject SpawnObject(string obj_s, Vector3 spawn_point, Quaternion rotation, GameObject caller) {
        GameObject obj = new GameObject();
        for (int i = 0; i < gameobject_pools.Count; i++) {
            if (obj_s == gameobject_pools[i].object_name) { // name matches
                for (int zi = 0; zi < gameobject_pools[i].amount_objects; zi++) {
                    if (!gameobject_pools[i].object_pool[zi].activeInHierarchy) {
                        obj = gameobject_pools[i].object_pool[zi].gameObject;
                        gameobject_pools[i].object_pool[zi].transform.rotation = rotation;
                        gameobject_pools[i].object_pool[zi].transform.position = spawn_point;
                        gameobject_pools[i].object_pool[zi].SetActive(true);
                        if (gameobject_pools[i].object_pool[zi].tag == "Enemy")
                            numEnemies++;
                        zi = gameobject_pools[i].amount_objects;
                    }
                }
                i = gameobject_pools.Count;
            }
        }
        return obj;
    }

    public void IncrementScore() { score++; } //increment the score
    public void IncRoomsCleared() { roomsCleared++; }
    public int GetXRot() { return xRotation; } //get x rotation of the camera
    public int getNumEnemies() { return numEnemies;  } //get the number of enemies currently active
    public void decNumEnemies() { numEnemies--; } //decrement the number of enemies currently active
    public int getNumPinIdols() { return numPinIdols; } //get number of pin idols remaining
    public void decNumPinIdols() { numPinIdols--; } //reduce number of pin idols remaining
    public GameObject getPlayerGameObj() { return Player; } //return the player object
}
