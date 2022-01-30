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
    void Start(){
        currentHealth = maxHealth;
        spriteManager = GameObject.Find("SpriteManager").GetComponent(typeof(SpriteManager)) as SpriteManager;
        healthBar.SetHealth(maxHealth);
    }

    void Update() {
        if(era == PlayerType.Medieval){
            if(team == PlayerTeam.Red){
                spriteRenderer.sprite = spriteManager.redMed;
            }
            else {
                spriteRenderer.sprite = spriteManager.blueMed;
            }
        }
        else{
            if(team == PlayerTeam.Red){
                spriteRenderer.sprite = spriteManager.redCybe;
            }
            else {
                spriteRenderer.sprite = spriteManager.blueCybe;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
        }
    }
    

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
