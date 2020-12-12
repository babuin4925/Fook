using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    public Animator anim;
    public Sprite idle;
    public Sprite corpse;
    public bool BodyInside {
        get 
        {
            return isDripping;
        }
    }

    private bool isDripping = false;
    public void Drip()
    {
        isDripping = true;
        anim.SetBool("dripping", isDripping);
    }
    public void DropCorpse()
    {
        if (isDripping)
        {
            anim.SetBool("clicked", true);
        }
    }
    
}
