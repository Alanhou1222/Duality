using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private float speed = 8f;

    private Transform player;
    private Vector2 target;
    private bool isEnemy = false;
    SpriteRenderer spriteRenderer;
    SpriteManager sm;
    PlayerControl controller;

    float allyAttack = 8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;
        sm = GameObject.Find("SpriteManager").GetComponent(typeof(SpriteManager)) as SpriteManager;   
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(controller.era == PlayerControl.PlayerType.Medieval) {
            spriteRenderer.sprite = sm.blueArrow;
        }
        else {
            spriteRenderer.sprite = sm.blueLaser;
        }
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
        Debug.Log(collision.gameObject.tag + "  ally");
        if (collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().getIsSameTypeAsPlayer()) {
            Debug.Log("Destroy ally projectile");
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
        LookAt2D(transform, target);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,225);
    }
    private void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
