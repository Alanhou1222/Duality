using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TreasureUI : MonoBehaviour
{
    public Transform player;
    public Transform box;
    public GameObject pressEGuide;
    public GameObject strength;
    public GameObject speed;
    public GameObject shield;
    public string powerUpString = "";
    public TextMeshProUGUI powerUpMessage;
    public float minDistance = 3f;
    public GameObject BoxUI;
    // Update is called once per frame
    void Update() {
        if ((player.position - box.position).magnitude <= minDistance) {
            pressEGuide.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                Open();
                Powerup();
            }
        } else {
          pressEGuide.SetActive(false);
        }
    }
    
    void Open() {
        BoxUI.SetActive(true);
        Time.timeScale = 0f;
    }

    void Powerup() {
        switch(Random.Range(0, 3)) {
          case 0:
              strength.SetActive(true);
              powerUpString = "Your strength is upgraded!";
              break;
          case 1:
              speed.SetActive(true);
              powerUpString = "Your speed is upgraded!";
              break;
          case 2:
              shield.SetActive(true);
              powerUpString = "Your shield is upgraded!";
              break;
        }
        ShowMessage(powerUpString);
    }

    void ShowMessage(string message) {
        powerUpMessage.text = message;
        // powerUpMessage.SetActive(true);
    }
}
