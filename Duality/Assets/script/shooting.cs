using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    //The interval you want your player to be able to fire.
    float fireRate = 0.5f;
 
    //The actual time the player will be able to fire.
    private float nextFire;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public playerControl controller;
  

    public float bulletForce = 20f;
    // Update is called once per frame
    void Start() 
    {
        controller = GameObject.Find("Player").GetComponent(typeof(playerControl)) as playerControl;    
    }
    void Update()
    {
        if(controller.era == playerControl.playerType.medieval) {
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
        GameObject bullet;
        if(controller.era == playerControl.playerType.medieval) {
            firePoint.localPosition = new Vector3(0.05f,0.15f,0);
            bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
        else {
            firePoint.localPosition = new Vector3(0.05f,0.2f,0);
            bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up*bulletForce,ForceMode2D.Impulse);
    }
}
