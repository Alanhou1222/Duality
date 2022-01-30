using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerProjectile : MonoBehaviour
{
    public enum EnemyType
    {
        Medieval,
        Cyberpunk
    }
    public enum PlayerTeam{
        Red,
        Blue
    }
    public PlayerControl controller;
    public SpriteRenderer spriteRenderer;
    public Sprite redArrow;
    public Sprite blueArrow;
    public Sprite redLaser;
    public Sprite blueLaser;

    private float attack = 10f;

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.tag == "Enemy" && !other.gameObject.GetComponent<Enemy>().getIsSameTypeAsPlayer())
        {
            if(other.gameObject.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Medieval){
                controller.changeEraProgress -= 2;
            }
            else {
                controller.changeEraProgress += 2;
            }
            other.gameObject.GetComponent<Enemy>().dealDamage(attack);
        }
        else if (other.gameObject.tag == "Enemy") {
            if(other.gameObject.GetComponent<Enemy>().enemyType == Enemy.EnemyType.Medieval){
                controller.changeEraProgress += 5;
            }
            else {
                controller.changeEraProgress -= 5;
            }
            other.gameObject.GetComponent<Enemy>().dealDamage(attack);
            controller.changeEraProgress -= 1;
        } 
        Destroy(gameObject);
    }

    void Start() {
        transform.localScale = new Vector3(2,2,2);
        transform.eulerAngles = transform.eulerAngles + new Vector3(0,0,-45);
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;
        if(controller.era == PlayerControl.PlayerType.Medieval){
            if(controller.team == PlayerControl.PlayerTeam.Red){
                spriteRenderer.sprite = redArrow;
            }
            else {
                spriteRenderer.sprite = blueArrow;
            }
            Destroy(gameObject,2f);
        }
        else{
            if(controller.team == PlayerControl.PlayerTeam.Red){
                spriteRenderer.sprite = redLaser;
            }
            else {
                spriteRenderer.sprite = blueLaser;
            }
            Destroy(gameObject,0.5f);
        }
    }
}
