using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBarImage;
    public Image progressBarImageBackground;
    private float initialPercentage = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        updateProgressBar(initialPercentage);
        changeColorOfProgressBar(new Color32(0,0,0,255));
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
