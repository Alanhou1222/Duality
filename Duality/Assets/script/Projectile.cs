using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed;

    private Transform player;
    private Vector2 target;
    private bool isEnemy = false;

    [SerializeField] float allyAttack = 5f;

    // Start is called before the first frame update
    void Start()
    {

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !collision.gameObject.GetComponent<Enemy>().getIsSameTypeAsPlayer()) {
            collision.gameObject.GetComponent<Enemy>().dealDamage(allyAttack);
        }
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    public void setTarget(Transform obj)
    {
        target = new Vector2(obj.position.x, obj.position.y);
    }
}
