using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
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

    void Start()
    {
        sentencesQueue = new Queue<string>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
    public void StartDialogue(Dialogue dialogue)
    {
        sentencesQueue.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentencesQueue.Enqueue(sentence);
        }

        nameText.text = dialogue.name;
        anim.Play("DialogueStart");
        Fade(true);

        Invoke("DisplayNextSentence", 0.1f);
    }
    public void DisplayNextSentence()
    {
        if (sentencesQueue.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            string sentence = sentencesQueue.Dequeue();
            StartCoroutine(Writer(sentence));
        }
        

    }
    IEnumerator Writer(string sentence)
    {
        textField.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            textField.text += letter;
            //play Audio;
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void EndDialogue()
    {
        Fade(false);
        anim.Play("DialogueEnd");
    }
    public void Fade(bool inTrueOutFalse)
    {
        fadeAnim.gameObject.SetActive(inTrueOutFalse);
        panel.PublishEvent(inTrueOutFalse);

        if (inTrueOutFalse)
        {
            fadeAnim.Play("FadeIn");
        }
        else
        {
            fadeAnim.Play("FadeOut");
        }
    }
}
