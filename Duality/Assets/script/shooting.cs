using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class shooting : MonoBehaviour
{
    //The interval you want your player to be able to fire.
    float fireRate = 0.5f;
 
    //The actual time the player will be able to fire.
    private float nextFire;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public PlayerControl controller;
    public AudioSource shootingSound;
    Vector2 mousePos;
    public Camera cam;
  

    public float bulletForce = 20f;
    // Update is called once per frame
    void Start() 
    {
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;    
    }
    void Update()
    {
        if(controller.era == PlayerControl.PlayerType.Medieval) {
            bulletForce = 12f;
        }
        else {
            bulletForce = 30f;
        }
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }
    
    void Shoot(){
        shootingSound.Play();
        GameObject bullet;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - new Vector2(transform.position[0], transform.position[1]);
        float angle = Mathf.Atan2(lookDir.y,lookDir.x) * Mathf.Rad2Deg - 90f;
        Quaternion direction = Quaternion.Euler(0, 0, angle);
        if(controller.era == PlayerControl.PlayerType.Medieval) {
            firePoint.localPosition = new Vector3(0.05f,0.15f,0);
            bullet = Instantiate(bulletPrefab, firePoint.position, direction);
        }
        else {
            firePoint.localPosition = new Vector3(0.05f,0.2f,0);
            bullet = Instantiate(bulletPrefab, firePoint.position, direction);
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector3 bulletDir = Vector3.Normalize(new Vector3(lookDir[0], lookDir[1],0));
        rb.AddForce(bulletDir*bulletForce,ForceMode2D.Impulse);
    }
}
