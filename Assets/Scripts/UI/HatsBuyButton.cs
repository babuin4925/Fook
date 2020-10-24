using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatsBuyButton : MonoBehaviour
{
    private GameObject currentHatButton;
    public GameObject CurrentHatButton
    {
        get
        {
            return currentHatButton;
        }
        set
        {
            currentHatButton = value;
            purchaser = currentHatButton.GetComponent<OnHatBought>();
            viewer = currentHatButton.GetComponent<HatButtonViewer>();

            if (viewer.Bought)
            {
                buyButtonTxt.GetComponent<Text>().text = "Equip";
            }
            else
            {
                buyButtonTxt.GetComponent<Text>().text = "Buy";
            }
        }
    }
    private OnHatBought purchaser;
    private HatButtonViewer viewer;
    [SerializeField] private GameObject buyButtonTxt;
    public void Buy()
    {
        purchaser.BuyOrEquip();

        if (viewer.Bought)
        {
            buyButtonTxt.GetComponent<Text>().text = "Equip";
        }
    }

}
