using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dialogue[] dialogues;
    //place dialogues in order that they need to be shown in
    public DialogueManager manager;

    private void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            manager.StartDialogue(dialogues[0]);
        }
        if (Input.GetKeyDown("n"))
        {
            manager.DisplayNextSentence();
        }
    }
}
