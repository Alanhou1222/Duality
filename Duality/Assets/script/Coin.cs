using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    SpriteManager sm;
    SpriteRenderer spriteRenderer;
    PlayerControl controller;
    public bool settingIsMedieval;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SpriteManager").GetComponent(typeof(SpriteManager)) as SpriteManager; 
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;   
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (controller.era == PlayerControl.PlayerType.Cyberpunk) {
            spriteRenderer.sprite = sm.medCoin;
        }
        else {
            spriteRenderer.sprite = sm.cybeCoin;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        controller.coinCount += 1;
        Destroy(gameObject);
    }
}
