using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentencesQueue;
    public Text nameText;
    public TextMeshProUGUI textField;
    private Animator anim;
    public Animator fadeAnim;
    public ShopPanel panel;
    public Image face;
    private Animator faceAnim;
    private bool doneTyping = true;
    private string sentence;

    public EventHandler OnDialogEnded;
    private AudioSource audio;
    public AudioClip dialogLetterSound;
    private float volume = 0.01f;
    void Awake()
    {
        sentencesQueue = new Queue<string>();
        anim = GetComponent<Animator>();
        faceAnim = face.GetComponent<Animator>();

        audio = gameObject.GetComponent<AudioSource>();
        audio.clip = dialogLetterSound;
        audio.volume = volume;
    }
    public void StartDialogue(Dialogue dialogue)
    {
        textField.text = "";
        sentencesQueue.Clear();
        InformAboutDialog(true);

        foreach (string sentence in dialogue.sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }

        nameText.text = dialogue.name;
        face.sprite = dialogue.face;
        anim.Play("DialogueStart");
        Fade(true);

        Invoke("DisplayNextSentence", 1f);
        Invoke("ShowFace", 0.1f);
    }
    public void DisplayNextSentence()
    {
        if (!doneTyping)
        {
            StopAllCoroutines();
            textField.text = sentence;
            doneTyping = true;
        }
        else
        {
            faceAnim.Play("Face_Idle");
            if (sentencesQueue.Count == 0)
            {
                EndDialogue();
            }
            else
            {
                sentence = sentencesQueue.Dequeue();
                StartCoroutine(Writer(sentence));
            }
        }

    }
    IEnumerator Writer(string sentence)
    {
        doneTyping = false;
        textField.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textField.text += letter;
            audio.Play();
            yield return new WaitForSeconds(0.03f);
        }
        doneTyping = true;
    }
    public void EndDialogue()
    {
        InformAboutDialog(false);
        Fade(false);
        faceAnim.Play("Face_Hide");
        anim.Play("DialogueEnd");
    }
    public void Fade(bool inTrueOutFalse)
    {
        fadeAnim.gameObject.SetActive(inTrueOutFalse);
        InformAboutDialog(inTrueOutFalse);

        if (inTrueOutFalse)
        {
            fadeAnim.Play("FadeIn");
        }
        else
        {
            fadeAnim.Play("FadeOut");
        }
    }
    public void ShowFace()
    {
        faceAnim.Play("Face_Appear");
    }
    public void InformAboutDialog(bool openedOrClosed)
    {
        try
        {
            panel.PublishEvent(openedOrClosed);
        }
        catch(NullReferenceException e)
        {
        }
        if (!openedOrClosed)
        {
            OnDialogEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
