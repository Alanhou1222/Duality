using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class boxCloseButton : MonoBehaviour
{    
    public GameObject BoxUI;
    public void closeBox(){
        Close();
    }

    void Close() {
        BoxUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
