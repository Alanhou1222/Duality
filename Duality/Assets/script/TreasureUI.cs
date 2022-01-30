using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    public Transform player;
    public Transform box;
    public GameObject pressEGuide;
    public float minDistance = 3f;
    public GameObject BoxUI;
    public string powerup;
    // Update is called once per frame
    void Update() {
        if ((player.position - box.position).magnitude <= minDistance) {
            pressEGuide.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                Open();
                Powerup();
                Debug.Log(powerup);
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
        switch(Random.Range(0, 2)) {
          case 0:
              powerup = "shield";
              break;
          case 1:
              powerup = "speed";
              break;
          case 2:
              powerup = "strength";
              break;
        }
    }
}
