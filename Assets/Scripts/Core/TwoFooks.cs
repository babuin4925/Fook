using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class TwoFooks : MonoBehaviour
{
    public EventHandler OnAnimationEnding;
    public GameObject fook1;
    public GameObject corpse;
    public GameObject puddle;
    
    public AudioClip bite;
    public AudioClip mouth;
    public AudioClip chew;
    
    new public AudioSource audio;

    public void EatingAnimEnded()
    {
        OnAnimationEnding?.Invoke(this, EventArgs.Empty);
        fook1.SetActive(true);
    }
    public void PlayMouthSound()
    {
        audio.clip = mouth;
        audio.Play();
    }
    public void PlayBiteSound()
    {
        audio.clip = bite;
        audio.Play();
    }
    public void PlayChewSound()
    {
        audio.clip = chew;
        audio.Play();
    }
}
