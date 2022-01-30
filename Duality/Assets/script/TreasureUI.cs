using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureUI : MonoBehaviour
{
    public static bool boxIsOpened = false;
    public Transform player;
    public Transform box;
    public float minDistance = 2f;
    public GameObject BoxUI;
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && (player.position - box.position).magnitude <= minDistance) {
            if (boxIsOpened) {
                Close();
            } else {
                Open();
            }
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
