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

    public float bulletForce = 20f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
            Shoot();
            nextFire = Time.time + fireRate;
        }
    }
    
    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up*bulletForce,ForceMode2D.Impulse);
    }
}
