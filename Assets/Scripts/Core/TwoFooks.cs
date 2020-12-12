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

    public void EatingAnimEnded()
    {
        OnAnimationEnding?.Invoke(this, EventArgs.Empty);
        fook1.SetActive(true);
    }
}
