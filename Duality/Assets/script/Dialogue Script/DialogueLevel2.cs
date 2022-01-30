using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueLevel2 : MonoBehaviour
{

    // Image for medieval and cyberpunk
    [SerializeField] Image medieval;
    [SerializeField] Image cyberpunk;

    [SerializeField] TMP_Text dialogue;

    [SerializeField] GameObject dialogueObject;

    int dialogueProgress = 0;

    bool finishDialogue = false;

    string[] allDialogues =
    {
        "Our family ... our home",
        "Our family ... OUR HOME",
        "You have no idea of us."
    };

    bool[,] whoTalks =
    {
        { true, false },
        { false, true },
        { false, true },
    };

    // Start is called before the first frame update
    void Start()
    {
        dialogueObject.SetActive(true);
        Time.timeScale = 0;

        medieval.enabled = whoTalks[dialogueProgress, 0];
        cyberpunk.enabled = whoTalks[dialogueProgress, 1];

        //medieval.SetActive(whoTalks[dialogueProgress, 0]);
        //cyberpunk.SetActive(whoTalks[dialogueProgress, 1]);

        dialogue.text = allDialogues[dialogueProgress];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !finishDialogue)
        {
            dialogueProgress++;
            if (dialogueProgress >= 3)
            {
                finishDialogue = true;

                dialogueObject.SetActive(false);

                dialogue.enabled = false;

                medieval.enabled = true;
                cyberpunk.enabled = true;

                Time.timeScale = 1;
            }
            else
            {
                medieval.enabled = whoTalks[dialogueProgress, 0];
                cyberpunk.enabled = whoTalks[dialogueProgress, 1];
                dialogue.text = allDialogues[dialogueProgress];
                dialogue.text = allDialogues[dialogueProgress];
            }
        }
    }
}
