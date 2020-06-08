using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject player; //player game object
    public float speed; //speed bullet flies
    // Start is called before the first frame update
    void Start()
    {
        //player = GameMaster.gm_script.getPlayerGameObj();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    /// <Move>
    /// Move the object forward by its speed;
    /// </summary>
    private void Move() {
        transform.position += transform.forward * speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other) {
        //If gameObj is an arrow, deactivate on player and decrement player hp
        if (other.tag == "Player" && gameObject.tag == "Arrow" && !player.GetComponent<PlayerController>().getDashing()) {
            player.GetComponent<PlayerController>().decHealthAndInvokeIFrames();
            gameObject.SetActive(false);
        }
        //If gameObj is a Arrow Destroy on room structures
        
        else if((other.tag == "Wall" || other.tag == "door") && gameObject.tag == "Arrow") {
            gameObject.SetActive(false);
        }
        else if (other.tag == "Idol" && gameObject.tag == "Scar_Bullet") //bullets don't work against idols
        {
            gameObject.SetActive(false);
        }
        /*
        else if ((other.tag != "Player" || other.tag != "Enemy") && gameObject.tag == "Arrow") {
            gameObject.SetActive(false);
        }*/
        //If the gameObj is a bullet, deactivate on appropritate objects
        else if (other.tag != "Wall" && other.tag != "door" && other.tag != "Trap" && other.tag != "Player_Weapon" && other.tag !="Arrow" && other.tag != "Player" && gameObject.tag == "Scar_Bullet") {
            gameObject.SetActive(false);
            if(other.tag == "Enemy")
                PlayHitSound(other);
            //GameMaster.gm_script.decNumEnemies();
            //other.gameObject.SetActive(false);
            other.gameObject.GetComponent<HealthScript>().decHP();
        }
        //If the gameObj is a bullet and it hits a door, deactivate
        else if(other.tag == "Wall" || other.tag == "door" && gameObject.tag == "Scar_Bullet") {
            gameObject.SetActive(false);
        }
        else if(other.tag == "Fire2" && gameObject.tag == "Scar_Bullet")
        {
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }

    private void PlayHitSound(Collider other) {
        if (other.gameObject.name.Contains("CubeDude"))
            FindObjectOfType<AudioManager>().Play("CubeDudeHit");
        else
            FindObjectOfType<AudioManager>().Play("EnemyHit");
    }
}
