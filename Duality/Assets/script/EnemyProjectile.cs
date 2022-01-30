using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public enum EnemyType
    {
        Medieval,
        Cyberpunk
    }
    private float speed = 4f;

    private Transform player;
    private Vector2 target;
    private Vector3 normalizedDirection;
    private bool isEnemy = false;
    private PlayerControl controller;

    float enemyAttack = 8f;
    SpriteManager sm;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sm = GameObject.Find("SpriteManager").GetComponent(typeof(SpriteManager)) as SpriteManager;   
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(2,2,2);
        target = new Vector2(player.position.x, player.position.y);
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;
        if(controller.era == PlayerControl.PlayerType.Medieval) {
            spriteRenderer.sprite = sm.redLaser;
        }
        else {
            spriteRenderer.sprite = sm.redArrow;
        }
        LookAt2D(transform, target);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,225);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizedDirection * speed * Time.deltaTime;
        // transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    private void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        var direction = target - current;
        normalizedDirection = direction.normalized;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().TakeDamage((int)UnityEngine.Random.Range(5f, 10f));
        }

        if ((collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().getIsSameTypeAsPlayer()) ||
            (collision.gameObject.tag == "Projectile"))
        {
            
        }
        else
        {
            Debug.Log("Destroy enemy projectile");
            DestroyProjectile();
        }
        
    }

    public void SetSpeed(float actualSpeed)
    {
        speed = actualSpeed;
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
