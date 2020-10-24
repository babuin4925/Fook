using System;
using System.Collections;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public StatesAnimationsAndBasicControlls controller;
    
    public Animator fookAnim;

    private bool animating;

    private float seconds = 2f; 
    void Start()
    {
        StartCoroutine(Blink());
    }
    IEnumerator Blink()
    {
        while (true)
        {
            System.Random rnd = new System.Random();
            seconds = rnd.Next(1,10);
            yield return new WaitForSeconds(seconds);
            controller.eyes.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            controller.eyes.SetActive(false);
        }
    }
}
