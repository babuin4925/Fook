using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    public Animator anim;
    public ShopPanel panel;
    private GameObject button;

    public bool BodyInside {
        get 
        {
            return isDripping;
        }
    }

    private bool isDripping = false;

    public EventHandler OnClosetClicked;
    private void Start()
    {
        button = this.transform.Find("Button").gameObject;
        panel.SomePanelIsBlocking += DisableButton;
    }
    public void Drip()
    {
        isDripping = true;
        anim.SetBool("dripping", isDripping);
    }
    public void DropCorpse()
    {
        if (isDripping)
        {
            OnClosetClicked?.Invoke(this, EventArgs.Empty);
        }
    }
    
    private void DisableButton(bool a)
    {
        button.SetActive(!a);   
    }
}
