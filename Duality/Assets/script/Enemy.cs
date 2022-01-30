using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioSource shootingSound;

    public enum EnemyType
    {
        Medieval,
        Cyberpunk
    }
    public enum PlayerTeam{
        Red,
        Blue
    }
    [Header("Ally Speed")]
    // The attributes for an ally
    float allySpeed = 10f;
    float allyStoppingDistance = 20f;
    float allyRetreatDistance = 10f;

    [Header("Enemy Speed")]
    // The attributes for an enemy
    float enemySpeed = 10f;
    float enemyStoppingDistance = 20f;
    float enemyRetreatDistance = 10f;

    [Header("Enemy Type")]
    public EnemyType enemyType = EnemyType.Medieval;
    [Range(0f, 1f)]
    [SerializeField] float aggro = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] float distance = 0.5f;
    private bool isSameTypeAsPlayer = true;
    [SerializeField] bool isSpecialEnemy = false;
    int playerType = 1;

    [Header("Projectile")]
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] GameObject allyProjectile;
    [SerializeField] GameObject shootPoint;

    private float timeBetweenShots;
    [SerializeField] float startTimeBetweenShots;
    float projectileSpeed = 0f;

    [Header("Health")]
    EnemyHealthBar healthBar;
    [SerializeField] float enemyMaxHealth = 100f;
    [SerializeField] float enemyCurrentHealth = 100f;

    [SerializeField] SpriteRenderer spriteRenderer; 
    private SpriteManager spriteManager;

    private bool isStop = false;

    Transform player;

    // Get all enemies
    GameObject[] allEnemies;

    // Target enemy
    GameObject enemy;

    // Player Control
    PlayerControl controller;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteManager = GameObject.Find("SpriteManager").GetComponent(typeof(SpriteManager)) as SpriteManager;   
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;
        healthBar = GetComponentInChildren<EnemyHealthBar>();

        // Set aggro and distance variable
        //                  Aggro high <--------------> low
        // Ammo speed       20f     4f      10f     4f
        // Movement speed   4.5f    1f      3f      1f
        // Shooting speed   0.5f    0.25f   1f      1.5f

        //                  Distance close <----------> far
        // Stopping Distance    2f      4f      5f      4f (6f)
        // Retrieving Distance  1f      2f      3f      2f (4f)

        // Easiest the right most
        // Mostly set up on third or around third
        // Little on the second
        // And the hardest on the left most

        // Renew
        // Set 4f, 1f, 0.25f, 4f, 2f
        // Other linear

        // Aggro
        projectileSpeed = 4f + 16f * aggro;
        allySpeed = 1.5f + 3f * aggro;
        enemySpeed = 1f + 3.5f * aggro;
        startTimeBetweenShots = 1.5f - aggro;

        // Distance
        allyStoppingDistance = 2f + 4f * distance;
        enemyStoppingDistance = 2f + 4f * distance;
        allyRetreatDistance = 1f + 3f * distance;
        enemyRetreatDistance = 1f + 3f * distance;

        // Special Enemy
        if (isSpecialEnemy)
        {
            projectileSpeed = 4f;
            allySpeed = 1f;
            enemySpeed = 1f;
            startTimeBetweenShots = 0.25f;
            allyStoppingDistance = 4f;
            enemyStoppingDistance = 4f;
            allyRetreatDistance = 2f;
            enemyRetreatDistance = 2f;
        }

        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {

        isStop = false;

        // Get player's type
        if (controller.era == PlayerControl.PlayerType.Medieval) {
            playerType = 0;
        }
        else {
            playerType = 1;
        }
        // Set enemyType
        healthBar.SwitchSide(enemyType);
        healthBar.SetHealth(enemyCurrentHealth);


        // if playerType is the same as enemyType, they are allies
        // otherwise they are enemies
        if ((playerType == 0 && enemyType == EnemyType.Medieval) || (playerType == 1 && enemyType == EnemyType.Cyberpunk))
        {
            isSameTypeAsPlayer = true;
            if (enemyType == EnemyType.Medieval)
            {
                spriteRenderer.sprite = spriteManager.blueMed;
            }
            else
            {
                spriteRenderer.sprite = spriteManager.blueCybe;
            }
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemy = getClosestEnemy(allEnemies);

            // No nearest enemy available
            if (enemy == null)
            {
                isStop = true;
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
                LookAt2D(transform, enemy.transform.position);
            }
            
        }
        else
        {
            isSameTypeAsPlayer = false;
            if (enemyType == EnemyType.Medieval)
            {
                spriteRenderer.sprite = spriteManager.redMed;
            }
            else
            {
                spriteRenderer.sprite = spriteManager.redCybe;
            }
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
            LookAt2D(transform, player.transform.position);

        }

        if (timeBetweenShots <= 0)
        {

            if (isSameTypeAsPlayer && !isStop)
            {
                GameObject projectile = Instantiate(allyProjectile, shootPoint.transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().setTarget(enemy.transform);
                projectile.GetComponent<Projectile>().SetSpeed(projectileSpeed);
            }
            else if (!isSameTypeAsPlayer)
            {
                shootingSound.Play();
                GameObject proj = Instantiate(enemyProjectile, shootPoint.transform.position, Quaternion.identity);
                proj.GetComponent<EnemyProjectile>().SetSpeed(projectileSpeed);
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

    public void LookAt2D(Transform transform, Vector2 target)
    {
        Vector2 current = transform.position;
        if(current[0] > target[0]){
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            transform.localScale = new Vector3(6,6,1);
        }
        else {
            transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
            transform.localScale = new Vector3(-6,6,1);
        }
        
    }

    public void dealDamage(float damage)
    {
        enemyCurrentHealth -= damage;
        if (enemyCurrentHealth <= 0)
        {
            if(enemyType == EnemyType.Medieval && playerType == 0){
                controller.changeEraProgress -= 25;
            }
            else if(enemyType == EnemyType.Medieval && playerType == 1){
                controller.changeEraProgress += 10;
            }  
            else if(enemyType == EnemyType.Cyberpunk && playerType == 0){
                controller.changeEraProgress -= 10;
            }
            else if(enemyType == EnemyType.Cyberpunk && playerType == 1){
                controller.changeEraProgress += 25;
            }
            if(controller.changeEraProgress > 100){
                controller.changeEraProgress = 100;
            }
            if(controller.changeEraProgress < 0){
                controller.changeEraProgress = 0;
            }
            enemyDead();
        }
    }

    private void enemyDead()
    {
        // add vfx, sfx
        Destroy(gameObject);
    }
}

