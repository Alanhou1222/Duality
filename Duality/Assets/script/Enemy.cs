using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum EnemyType
    {
        Medieval,
        Cyberpunk
    }

    [Header("Ally Speed")]
    // The attributes for an ally
    [SerializeField] float allySpeed = 10f;
    [SerializeField] float allyStoppingDistance = 20f;
    [SerializeField] float allyRetreatDistance = 10f;

    [Header("Enemy Speed")]
    // The attributes for an enemy
    [SerializeField] float enemySpeed = 10f;
    [SerializeField] float enemyStoppingDistance = 20f;
    [SerializeField] float enemyRetreatDistance = 10f;

    [Header("Enemy Type")]
    [SerializeField] EnemyType enemyType = EnemyType.Medieval;
    [SerializeField] float aggro = 0.5f;
    private bool isSameTypeAsPlayer = true;
    int playerType = 1;

    [Header("Projectile")]
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] GameObject allyProjectile;

    private float timeBetweenShots;
    [SerializeField] float startTimeBetweenShots;

    [Header("Health")]
    [SerializeField] GameObject medievalHealthBar;
    [SerializeField] GameObject cyberHealthBar;
    [SerializeField] float enemyMaxHealth = 100f;
    [SerializeField] float enemyCurrentHealth = 100f;

    Transform player;

    // Get all enemies
    GameObject[] allEnemies;

    // Target enemy
    GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {

        // Get player's type
        playerType = 1;

        // Set enemyType
        if (enemyType == EnemyType.Medieval)
        {

        }
        else
        {

        }

        // if playerType is the same as enemyType, they are allies
        // otherwise they are enemies
        if ((playerType == 0 && enemyType == EnemyType.Medieval) || (playerType == 1 && enemyType == EnemyType.Cyberpunk))
        {
            isSameTypeAsPlayer = true;

            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemy = getClosestEnemy(allEnemies);

            // No nearest enemy available
            if (enemy == null)
            {
                Debug.Log("no enemy");
                // don't do anything 
            }

            else
            {
                //Enemy enemy = e.GetComponent<Enemy>();
                if (Vector2.Distance(transform.position, enemy.transform.position) > allyStoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, allySpeed * Time.deltaTime);
                }
                else if (Vector2.Distance(transform.position, enemy.transform.position) < allyStoppingDistance &&
                            Vector2.Distance(transform.position, enemy.transform.position) > allyRetreatDistance)
                {
                    transform.position = this.transform.position;
                }
                else if (Vector2.Distance(transform.position, enemy.transform.position) < allyRetreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, -allySpeed * Time.deltaTime);
                }
            }
            
        }
        else
        {
            isSameTypeAsPlayer = false;

            if (Vector2.Distance(transform.position, player.position) > enemyStoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < enemyStoppingDistance &&
                        Vector2.Distance(transform.position, player.position) > enemyRetreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < enemyRetreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
            }

        }

        if (timeBetweenShots <= 0)
        {

            if (isSameTypeAsPlayer)
            {
                GameObject projectile = Instantiate(allyProjectile, transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().setTarget(enemy.transform);
            }
            else
            {
                Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            }

            timeBetweenShots = startTimeBetweenShots;

            // if enemy is null

        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

    }

    void setEnemyType(EnemyType type)
    {
        enemyType = type;
    }

    void switchEnemyType(EnemyType type)
    {
        if (type == EnemyType.Medieval)
        {
            enemyType = EnemyType.Cyberpunk;
        }
        else
        {
            enemyType = EnemyType.Medieval;
        }
    }

    EnemyType getEnemyType()
    {
        return enemyType;
    }

    GameObject getClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            // Check if it is enemy type
            Enemy enemy = potentialTarget.GetComponent<Enemy>();
            if ((enemy.getEnemyType() == EnemyType.Medieval && playerType != 0) ||
                (enemy.getEnemyType() == EnemyType.Cyberpunk && playerType != 1)) {

                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }

            }

            
        }

        return bestTarget;
    }

    public bool getIsSameTypeAsPlayer()
    {
        return isSameTypeAsPlayer;
    }

    public void dealDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            enemyDead();
        }
    }

    private void enemyDead()
    {
        // add vfx, sfx
        Destroy(gameObject);
    }
}

