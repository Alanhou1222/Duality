using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class boxCloseButton : MonoBehaviour
{    
    public GameObject BoxUI;
    public GameObject strength;
    public GameObject speed;
    public GameObject shield;
    public void closeBox(){
        Close();
    }

    void Close() {
        BoxUI.SetActive(false);
        strength.SetActive(false);
        speed.SetActive(false);
        shield.SetActive(false);
        Time.timeScale = 1f;
    }
}
