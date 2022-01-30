using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
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
        healthBar.SetHealth(maxHealth);
    }

    void Update() {
        if(coinCount == 3) {
            if(era == PlayerType.Medieval) {
                era = PlayerType.Cyberpunk;
            }
            else {
                era = PlayerType.Medieval;
            }
            coinCount = 0;
            cm.GenerateCoins(3);
        }
        if(era == PlayerType.Medieval){
            spriteRenderer.sprite = spriteManager.redMed;
        }
        else{
            spriteRenderer.sprite = spriteManager.cybeMainCharacter;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
        }
    }
    

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !collision.gameObject.GetComponent<Enemy>().getIsSameTypeAsPlayer())
        {
            TakeDamage(20);
        }
    }
}
