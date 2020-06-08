using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaster : MonoBehaviour
{
    public int spawnAmount;
    public bool hasEntered;

    // Start is called before the first frame update
    void Start()
    {
        spawnAmount = 6;
        EnemySpawn(spawnAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemySpawn(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameMaster.gm_script.SpawnObject("ChargingEnemy", new Vector3(Random.Range(-40f, 40f), 0, Random.Range(0f, 50f)), gameObject);
        }
    }
}
