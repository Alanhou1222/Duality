using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    private float speed = 8f;

    private Transform player;
    private Vector2 target;
    private bool isEnemy = false;

    float enemyAttack = 8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.localScale = new Vector3(2,2,2);
        target = new Vector2(player.position.x, player.position.y);
        LookAt2D(transform, target);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,225);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // player lose health

        }

        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.tag != "Enemy")
        {
            Debug.Log("Destroy enemy projectile");
            DestroyProjectile();
        }
        
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
