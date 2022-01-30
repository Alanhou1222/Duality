using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    public static bool boxIsOpened = false;
    public Transform player;
    public Transform box;
    public GameObject pressEGuide;
    public float minDistance = 3f;
    public GameObject BoxUI;
    // Update is called once per frame
    void Update() {
        if ((player.position - box.position).magnitude <= minDistance) {
            pressEGuide.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                if (boxIsOpened) {
                    Close();
                } else {
                    Open();
                }
            }
        } else {
          pressEGuide.SetActive(false);
        }
    }

    void Close() {
        BoxUI.SetActive(false);
        Time.timeScale = 1f;
        boxIsOpened = false;
    }
    
    void Open() {
        BoxUI.SetActive(true);
        Time.timeScale = 0f;
        boxIsOpened = true;
    }
}
