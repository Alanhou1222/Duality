using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{  
    public Slider slider;
    public GameObject fill;
    public GameObject icon;
    public GameObject boarder;
    public Sprite battery;
    public Sprite bloodDrop;
    public Sprite medivalBoarder;
    public Sprite cyberpunkBoarder;

    public void SetMaxHealth(int health){
       slider.maxValue = health;
   } 
    public void SetHealth(int health){
       slider.value = health;
    } 

    public void SwitchSide(PlayerControl.PlayerType playerType){
        if(playerType == PlayerControl.PlayerType.Medieval){
            fill.GetComponent<Image>().color = new Color(0.745f, 0.251f, 0.251f,1f);
            icon.GetComponent<Image>().sprite = bloodDrop;
            boarder.GetComponent<Image>().sprite = medivalBoarder;

        }
        else{
            fill.GetComponent<Image>().color = new Color(59/255f, 170/255f, 118/255f, 1);
            icon.GetComponent<Image>().sprite = battery;
            boarder.GetComponent<Image>().sprite = cyberpunkBoarder;
        }
    }
}
