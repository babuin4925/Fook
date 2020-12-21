using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class UIPanel : MonoBehaviour
{
    private Animator anim;

    private bool extended = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.mousePosition.x / Camera.main.pixelWidth < 0.75 && extended)
        {
            SlideReturn();
        }
    }
    public void Slide()
    {
        if (!extended)
        {
            anim.Play("SlidingPanelAnimation");
            Invoke("SetExtendedTrue", 0.5f);
        }
        else
        {
            SlideReturn();
        }
    }
    public void SlideReturn()
    {
        anim.Play("SlidingPanelReturn");
        Invoke("SetExtendedFalse", 0.5f);
    }

    private void SetExtendedTrue()
    {
        extended = true;
    }
    private void SetExtendedFalse()
    {
        extended = false;
    }
}
