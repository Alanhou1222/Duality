using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public TMP_Text timerText;
    private float timer = 0.00f;
    private bool timerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        startTimer();
        resetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn) {
            timer += Time.deltaTime;
            updateTime();
        }
    }

    void updateTime() {
        int minutes = Mathf.FloorToInt(timer / 60.00f);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void startTimer() {
        timerOn = true;
    }
    public void stopTimer() {
        timerOn = false;
    }
    public void resetTimer() {
        timer = 0.00f;
    }
}
