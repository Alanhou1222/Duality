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
        localScale = transform.localScale;
    }
    public void SetHealth(float health){
       localScale.x = health/100*1.67f;
       transform.localScale = localScale;
    } 

    void Update() {
        SetHealth(100);
        SwitchSide(Enemy.EnemyType.Cyberpunk);
    }

    void SwitchSide(Enemy.EnemyType enemyType){
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
