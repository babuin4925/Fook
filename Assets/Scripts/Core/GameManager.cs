using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Dialogue[] dialogues;
    //place dialogues in order that they need to be shown in
    public DialogueManager manager;
    public StatesAnimationsAndBasicControlls controller;
    public Closet closet;
    public ShopPanel shop;
    public BuyFookTheSecond buyFookButton;
    public Animator fadeAnim;
    
    public TwoFooks twoFooks;
    public PlayableDirector director;
    
    void Start()
    {
        BlackOutReverse();

        if (SceneManager.GetActiveScene().name == "Game")
        {
            shop.SomePanelIsBlocking += TriggerDripping;
            SubscribeAllDialogs();
            manager.StartDialogue(dialogues[0]);
        }
        if(SceneManager.GetActiveScene().name == "DefaultEnding")
        {
            twoFooks.OnAnimationEnding += SpareUsDialog;
        }
        if(SceneManager.GetActiveScene().name == "BadEnding")
        {
            director.stopped += CorpseDialogue;
        }
    }


    private void TriggerDripping(bool x)
    {
        if (x && controller.anger>=700)
        {
            closet.Drip();
            UnsubscribeAllDialogs();
        }
    }
    private void ReleaseCutscene(object sender, EventArgs e)
    {
        controller.FookAnim.enabled = true;
        director.Play();
        controller.transform.position = new Vector3(14, controller.transform.position.y, controller.transform.position.z);
        director.stopped += ReleaseDialog;
    }

    private void ReleaseDialog(PlayableDirector obj)
    {
        manager.StartDialogue(dialogues[2]);
        manager.OnDialogEnded += CreditsLauncher2;
    }

    private void First1000Dialog(object sender, EventArgs e)
    {
        manager.StartDialogue(dialogues[3]);
        controller.Score.scoreOver1000 -= First1000Dialog;
    }
    private void ManiacDialog(object sender, EventArgs e)
    {
        manager.StartDialogue(dialogues[4]);
        controller.Score.scoreOver3000 -= ManiacDialog;
    }
    private void AfterClickDialog(int a, int b)
    {
        manager.StartDialogue(dialogues[1]);
        controller.OnHungerLowered -= AfterClickDialog;
    }
    private void LoadEndScene(object o, EventArgs e)
    {
        shop.CloseShop();
        BlackOut();
        SceneManager.LoadScene("DefaultEnding");
    }
    private void HatDialog(object o, EventArgs e)
    {
        shop.CloseShop();
        manager.StartDialogue(dialogues[5]);
        controller.OnHatGot -= HatDialog;
        controller.OnHatGot += HatWarning;
    }

    private void HatWarning(object sender, EventArgs e)
    {
        if (controller.anger == 600)
        {
            shop.CloseShop();
            manager.StartDialogue(dialogues[6]);
        }
    }

    public void SubscribeAllDialogs()
    {
        controller.OnFookReleased += ReleaseCutscene;
        controller.OnHungerLowered += AfterClickDialog;
        controller.Score.scoreOver3000 += ManiacDialog;
        controller.Score.scoreOver1000 += First1000Dialog;
        buyFookButton.OnFookBought += LoadEndScene;
        controller.OnHatGot += HatDialog;
        closet.OnClosetClicked += BadEndingTransition;
    }


    public void UnsubscribeAllDialogs()
    {
        controller.OnFookReleased -= ReleaseCutscene;
        controller.OnHungerLowered -= AfterClickDialog;
        controller.Score.scoreOver3000 -= ManiacDialog;
        controller.Score.scoreOver1000 -= First1000Dialog;
    }
    
    private void BlackOut()
    {
        fadeAnim.Play("BlackOut");
    }
    private void BlackOutReverse()
    {
        fadeAnim.Play("BlackOutReverse");
    }
    private void BlackOutSlowed()
    {
        fadeAnim.Play("BlackOutSlowed");
    }


    private void SpareUsDialog(object o, EventArgs e)
    {
        manager.StartDialogue(dialogues[6]);
        manager.OnDialogEnded += EndCutscene;
    }

    private void EndCutscene(object sender, EventArgs e)
    {
        twoFooks.corpse.SetActive(true);
        twoFooks.puddle.SetActive(true);
        Destroy(twoFooks.gameObject);
        director.Play();
        director.stopped += CreditsLauncher;
    }
    private void CreditsLauncher(PlayableDirector obj)
    {
        manager.StartDialogue(dialogues[7]);
        manager.OnDialogEnded -= EndCutscene;
        manager.OnDialogEnded += RollCredits;
    }
    
    private void RollCredits(object sender, EventArgs e)
    {
        StartCoroutine("RollCreditsCoroutineSlowed");
    }
    private IEnumerator RollCreditsCoroutine()
    {
        BlackOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Credits");
    }
    private IEnumerator RollCreditsCoroutineSlowed()
    {
        yield return new WaitForSeconds(1f);
        BlackOutSlowed();
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("Credits");
    }

    private void BadEndingTransition(object sender, EventArgs e)
    {
        StartCoroutine("BadEndingLaunch");
    }
    private IEnumerator BadEndingLaunch()
    {
        BlackOut();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("BadEnding");
    }

    private void CorpseDialogue(PlayableDirector obj)
    {
        manager.StartDialogue(dialogues[0]);
        manager.OnDialogEnded += CreditsLauncher2;
    }

    private void CreditsLauncher2(object sender, EventArgs e)
    {
        StartCoroutine("RollCreditsCoroutineSlowed");
    }
}
