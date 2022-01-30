using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public SpriteRenderer spriteRenderer;
    public Sprite redMed;
    public Sprite blueMed;
    public Sprite redCybe;
    public Sprite blueCybe;
    public HealthBar healthBar;
    public enum playerType{
        medieval,
        cyberpunk
    }
    public enum playerTeam{
        red,
        blue
    }
    public playerType era;
    public playerTeam team;
    void Start(){
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    void Update() {
        if(era == playerType.medieval){
            if(team == playerTeam.red){
                spriteRenderer.sprite = redMed;
            }
            else {
                spriteRenderer.sprite = blueMed;
            }
        }
        else{
            if(team == playerTeam.red){
                spriteRenderer.sprite = redCybe;
            }
            else {
                spriteRenderer.sprite = blueCybe;
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
