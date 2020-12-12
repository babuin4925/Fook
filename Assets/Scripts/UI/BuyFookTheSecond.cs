using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BuyFookTheSecond : MonoBehaviour
{
    public GameObject buyButton;
    public GameObject descText;
    public GameObject costText;

    public ShopPanel shop;

    public EventHandler OnFookBought; 
    private void Start()
    {
        shop.SomePanelIsBlocking += HideAll;
    }

    private void HideAll(bool a)
    {
        if (a)
        {
            buyButton.SetActive(false);
            descText.SetActive(false);
            costText.SetActive(false);
        }
    }

    public void ShowAll()
    {
        buyButton.SetActive(true);
        descText.SetActive(true);
        costText.SetActive(true);
    }

    public void BuyFook()
    {
        OnFookBought?.Invoke(this, EventArgs.Empty);
    }

}
