using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Item")]
public class ShopItem : ScriptableObject
{
    public int cost;

    public Image image;
    private Image buttonImage;

    public new string name;
    public string itemType;

    private bool bought = false;

    /*void tryBuy(int money)
    {
        if (!bought)
        {
            if (money > price)
            {
                bought = true;

            }
        }
        else
        {
            Equip();
        }
    }
    void Equip()
    {

    }*/
}
