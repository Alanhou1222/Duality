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

    // The attributes for an ally
    [SerializeField] float allySpeed = 10f;
    [SerializeField] float allyStoppingDistance = 20f;
    [SerializeField] float allyRetreatDistance = 10f;

    // The attributes for an enemy
    [SerializeField] float enemySpeed = 10f;
    [SerializeField] float enemyStoppingDistance = 20f;
    [SerializeField] float enemyRetreatDistance = 10f;

    [SerializeField] EnemyType enemyType = EnemyType.Medieval;
    int playerType = 1;

    private float timeBetweenShots;
    [SerializeField] float startTimeBetweenShots;

    [SerializeField] GameObject projectile;

    [SerializeField] Transform player;

    // Get all enemies
    GameObject[] allEnemies;

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

        // if playerType is the same as enemyType, they are allies
        // otherwise they are enemies
        if (playerType == 0 && enemyType == EnemyType.Medieval || playerType == 1 && enemyType == EnemyType.Cyberpunk)
        {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject enemy = getClosestEnemy(allEnemies);

            // No nearest enemy available
            if (e == null)
            {
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
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
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
                (enemy.getEnemyType() == EnemyType.Cyberpunk && playerType != 1))
            {

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
}

