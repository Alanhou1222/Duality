using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMusic : MonoBehaviour
{

    AudioSource audioSource;

    GameObject player;
    PlayerControl playercontrol;

    [SerializeField] AudioClip medievalMusic;
    [SerializeField] AudioClip cyberpunkMusic;
    private bool isMedieval = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        playercontrol = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        playercontrol = player.GetComponent<PlayerControl>();
        if (playercontrol.era == PlayerControl.PlayerType.Medieval && !isMedieval)
        {
            isMedieval = true;
            audioSource.clip = medievalMusic;
        }
        else if (playercontrol.era == PlayerControl.PlayerType.Cyberpunk && isMedieval)
        {
            isMedieval = false;
            audioSource.clip = cyberpunkMusic;
        }
    }
}
