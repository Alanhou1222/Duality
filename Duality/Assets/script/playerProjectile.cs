using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    public playerControl controller;
    public SpriteRenderer spriteRenderer;
    public Sprite redArrow;
    public Sprite blueArrow;
    public Sprite redLaser;
    public Sprite blueLaser;

    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

    void Start() {
        transform.localScale = new Vector3(4,4,4);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,-45);
        controller = GameObject.Find("Player").GetComponent(typeof(playerControl)) as playerControl;
        if(controller.era == playerControl.playerType.medieval){
            if(controller.team == playerControl.playerTeam.red){
                spriteRenderer.sprite = redArrow;
            }
            else {
                spriteRenderer.sprite = blueArrow;
            }
            Destroy(gameObject,2f);
        }
        else{
            if(controller.team == playerControl.playerTeam.red){
                spriteRenderer.sprite = redLaser;
            }
            else {
                spriteRenderer.sprite = blueLaser;
            }
            Destroy(gameObject,0.5f);
        }
    }
}
