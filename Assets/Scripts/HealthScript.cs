using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && gameObject.tag != "Enemy")
            gameObject.SetActive(false);
        if (hp <= 0 && gameObject.tag == "Enemy") {
            GameMaster.gm_script.decNumEnemies();
            gameObject.SetActive(false);
        }
    }

    public void decHP() { hp--; }
    public int getHP() { return hp; }
    public void setHP(int x) { hp = x; }
    public void incHP() { hp++; }
}
