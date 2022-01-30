using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBarImage;
    public Image progressBarImageBackground;
    public PlayerControl controller;
    private float initialPercentage = 0;
    // Start is called before the first frame update
    void Start()
    {
        changeColorOfProgressBar(new Color32(131,2,123,255));
        changeColorOfProgressBarBackground(new Color32(242,242,26,255));
        controller = GameObject.Find("Player").GetComponent(typeof(PlayerControl)) as PlayerControl;
        if(controller.era == PlayerControl.PlayerType.Medieval) {
            initialPercentage = 100;
        }
        else {
            initialPercentage = 0;
        }
    }

    void Update() {
        if(controller.era == PlayerControl.PlayerType.Medieval) {
            updateProgressBar(controller.changeEraProgress / 100.00f);
        }
        else {
            updateProgressBar(controller.changeEraProgress / 100.00f);
        }
    }

    public void updateProgressBar(float percentage) {
        progressBarImage.fillAmount = percentage;
    }
    public void changeColorOfProgressBar(Color32 color) {
        progressBarImage.color = color;
    }
    public void changeColorOfProgressBarBackground(Color32 color) {
        progressBarImageBackground.color = color;
    }
}
