using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool invincible = false;
    public int maxHealth = 100;
    public int currentHealth;
    public SpriteRenderer spriteRenderer;
    public Sprite redMed;
    public Sprite blueMed;
    public Sprite redCybe;
    public Sprite blueCybe;
    public HealthBar healthBar;
    public SpriteManager spriteManager;
    public int coinCount = 0;
    public int changeEraProgress = 0;
    public enum PlayerType{
        Medieval,
        Cyberpunk
    };
    public enum PlayerTeam{
        Red,
        Blue
    };
    public PlayerType era;
    public PlayerTeam team;
    public CoinManager cm;
    void Start(){
        currentHealth = maxHealth;
        spriteManager = GameObject.Find("SpriteManager").GetComponent(typeof(SpriteManager)) as SpriteManager;
        cm = GameObject.Find("CoinManager").GetComponent(typeof(CoinManager)) as CoinManager;
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetHealth(maxHealth);
        if(era == PlayerType.Medieval){
            changeEraProgress = 100;
        }
        else {
            changeEraProgress = 0;
        }
    }

    void Update() {
        if(coinCount == 3) {
            // SwitchEra();
            coinCount = 0;
            cm.GenerateCoins(3);
        }
        if (changeEraProgress < 40) {
            SwitchEra(PlayerType.Cyberpunk);
        }
        else if(changeEraProgress > 60){
            SwitchEra(PlayerType.Medieval);
        }
    }
    

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().getIsSameTypeAsPlayer()&&!invincible)
        {
            TakeDamage(20);
        }
    }

    public void SwitchEraInt(int type) {
        if(type == 0){
            SwitchEra(PlayerType.Medieval);
        }
        else{
            SwitchEra(PlayerType.Cyberpunk);
        }
    }
    public void SwitchEra(PlayerType type){ 
        era = type;
        healthBar.SwitchSide(era);
    }
}
