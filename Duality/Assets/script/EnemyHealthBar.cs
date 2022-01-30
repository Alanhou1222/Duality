using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public SpriteRenderer fillSprite;
    public SpriteRenderer iconSprite;
    public SpriteRenderer boarderSprite;
    public Sprite battery;
    public Sprite bloodDrop;
    public Sprite medivalBoarder;
    public Sprite cyberpunkBoarder;
    Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        localScale = new Vector3(0.15f,0.15f,1);
        transform.localScale = localScale;
        transform.rotation = Quaternion.AngleAxis(270, Vector3.forward);
    }
    public void SetHealth(float health){
       localScale.x = health/100*0.15f;
       transform.localScale = localScale;
    } 

    void Update() {
        SetHealth(100);
        transform.localPosition = new Vector2(0.2f,0.1f);
        SwitchSide(Enemy.EnemyType.Cyberpunk);
    }

    public void SwitchSide(Enemy.EnemyType enemyType){
        if(enemyType == Enemy.EnemyType.Medieval){
            fillSprite.color = new Color(0.745f, 0.251f, 0.251f,1f);
            iconSprite.sprite = bloodDrop;
            boarderSprite.sprite = medivalBoarder;

        }
        else{
            fillSprite.color = new Color(59/255f, 170/255f, 118/255f, 1);
            iconSprite.sprite = battery;
            boarderSprite.sprite = cyberpunkBoarder;
        }
    }
}
