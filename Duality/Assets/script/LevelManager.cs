using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { get; private set; } // static singleton

    public int playerHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (Instance == null) { Instance = this; }
    }

    public void LoadToLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadToLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadToLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LoadToLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void LoadToLevel5()
    {
        SceneManager.LoadScene("Level 5");
    }

    public void LoadToCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeToNextLevel()
    {
        LoadToLevel2();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
