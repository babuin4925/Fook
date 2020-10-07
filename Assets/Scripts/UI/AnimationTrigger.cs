using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public StatesAnimationsAndBasicControlls controller;
    
    public Animator fookAnim;
    public Animator eyesAnim;

    public bool animating;

    public float seconds = 5f; 
    void Start()
    {
        controller.OnFookStanding += EarShake;
    }

    IEnumerator StartAnimating()
    {
        yield return new WaitForSeconds(seconds);
        animating = true;
        fookAnim.SetBool("EarShaking", true);
    }
    public void EarShake(object o, EventArgs e)
    {
        StartCoroutine(StartAnimating());
    }
}
