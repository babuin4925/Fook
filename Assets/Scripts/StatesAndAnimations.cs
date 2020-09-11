using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesAndAnimations : MonoBehaviour
{
    bool isSitting = true;
    bool isSittingIdle = true;
    bool isPlaying = false;
    void Start()
    {
        
    }


    void Update()
    {

    }
    public void Sit()
    {
        isSitting = true;
        //changes sprite to sitting
    }

    public void EarShake()
    {
        isSittingIdle = false;
        //starts animation
    }

    public void Play()
    {
        isPlaying = true;
        //Starts playing animation
    }


}
