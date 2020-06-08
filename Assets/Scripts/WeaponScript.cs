using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WeaponScript : MonoBehaviour {
    public GameObject bullet;
    public Transform w_Rotator;
    public float radius;
    public float angle { get; set; }
    public SpriteRenderer weaponSprite;
    public List<SpriteRenderer> sprites;
    public Transform bulletSpawnPos;
    public float swingSpeed;
    private bool attack;
    private float swingTimer;
    public float baseSwingTimer;
    public TextMeshProUGUI ammoText;
    private int ammo;
    private int magazine;
    private int magazineMax;
    private float reloadTimer;
    private float baseReloadTimer;
    private Transform pivot;
    private bool scythePlayed;
    private bool enemyHit;
    private float shootWaitTimer;
    private float baseShootWaitTimer;
    private bool canShoot;
    void Start() {
        swingTimer = baseSwingTimer;
        pivot = w_Rotator.transform;
        //transform.parent = pivot;
        transform.position *= radius;
        magazineMax = 5;
        magazine = magazineMax;
        ammo = magazine;
        baseReloadTimer = 6.0f;
        reloadTimer = baseReloadTimer;
        ammoText.text = "Ammo: " + ammo + "/" + magazine;
        weaponSprite = sprites[0];
        scythePlayed = false;
        enemyHit = false;
        canShoot = true;
        shootWaitTimer = 0;
        baseShootWaitTimer = 0.5f;
    }

    void Update() {
        WeaponSpriteHandler();
        Reload();
        ShootWaitTimer();
        if (Input.GetMouseButtonDown(0))
	        attack = !attack;
        Vector3 wr_Vector = Camera.main.WorldToScreenPoint(w_Rotator.position);
        wr_Vector = Input.mousePosition - wr_Vector;
	    if(!attack){
		    //gameObject.GetComponent<TrailRenderer>().Clear();
            setColliders(false);
            angle = Mathf.Atan2(wr_Vector.y, wr_Vector.x) * Mathf.Rad2Deg;
	        pivot.position = w_Rotator.position;
            pivot.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        
	    }
	    else{
                if (ammo > 0) {     
                    radius = 1.5f;
                    Shoot();
                }    
                else {
                    radius = 5;
                    if (!scythePlayed) {
                        FindObjectOfType<AudioManager>().Play("ScytheSlash");
                        scythePlayed = true;
                    }
                    setColliders(true);
                    swingTimer -= Time.deltaTime;
                    if (swingTimer > 0) {
                        if (angle >= 90 || angle <= -90)
                            pivot.RotateAround(pivot.position, Vector3.down, swingSpeed * Time.deltaTime);
                        else
                            pivot.RotateAround(pivot.position, Vector3.down, -swingSpeed * Time.deltaTime);

                    }
                    else {
                        attack = false;
                        scythePlayed = false;
                        swingTimer = baseSwingTimer;
                        gameObject.GetComponent<TrailRenderer>().Clear();

                    }
                }
	    }



    }

    private void ShootWaitTimer() {
        if (!canShoot) {
            if (shootWaitTimer <= 0) {
                canShoot = true;
                shootWaitTimer = baseShootWaitTimer;

            }
            else
                shootWaitTimer -= Time.deltaTime;
        }
    }
    private void WeaponSpriteHandler() {
        if (ammo > 0) {
            sprites[0].enabled = false;
            sprites[1].enabled = true;
        }
        else {
            sprites[0].enabled = true;
            sprites[1].enabled = false;
        }
        if (angle >= 90 || angle <= -90) {
            //weaponSprite.flipY = true;
            sprites[0].flipY = true;
            sprites[1].flipY = true;
        }
        else {
            //weaponSprite.flipY = false;
            sprites[0].flipY = false;
            sprites[1].flipY = false;
        }
        
    }
    private void Shoot() {
        if (ammo > 0 && canShoot) { //If we have bullets in the magazine
            canShoot = false;
            shootWaitTimer = baseShootWaitTimer;
            FindObjectOfType<AudioManager>().Play("Bullet");
            ammo--;
            GameMaster.gm_script.SpawnObject("Scar_Bullet", bulletSpawnPos.position, Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y + 90, pivot.eulerAngles.z), this.gameObject);
            attack = false;
            ammoText.text = "Ammo: " + ammo + "/" + magazine;
        }
    }

    private void Reload() {
        if (ammo <= 0) {
            if (reloadTimer > 0) {
                ammoText.text = "Reloading...   " + Mathf.Round(reloadTimer * 100f) / 100f;
                reloadTimer -= Time.deltaTime;
            }
            else {
                ammo = magazineMax;
                reloadTimer = baseReloadTimer;
                ammoText.text = "Ammo: " + ammo + "/" + magazine;
            }
        }
    }

    private void setColliders(bool b) {
        foreach(Collider c in GetComponents<BoxCollider>()) {
            c.enabled = b;
        }

    }
    private void attackClicked(){
	
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Boss" || other.tag == "Idol" || other.tag == "Enemy" && !enemyHit) {
            enemyHit = true;
            PlayHitSound(other);
            other.gameObject.GetComponent<HealthScript>().decHP();
        }

    }

    private void PlayHitSound(Collider other) {
        if (other.gameObject.name.Contains("CubeDude"))
            FindObjectOfType<AudioManager>().Play("CubeDudeHit");
        else
            FindObjectOfType<AudioManager>().Play("EnemyHit");
    }

    private void OnTriggerExit(Collider other) {
        enemyHit = false;
    }
    public void setAttack(bool b){ attack = b; }
}