using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public enum playerType{
        medieval,
        cyberpunk
    }
    void Start(){
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(20);
        }
    }
    

    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
