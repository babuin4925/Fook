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

    public int foodPriceBonus = 0;
    public int maxHungerBonus = 0;
    public int clickPowerBonus = 0;
    public int angerIncrease = 0;
    public Sprite sprite;

    public new string name;
    public string description;

    public int[] bonusList = new int[4];

    private bool bought = false;

    public int[] FormArray()
    {
        bonusList[0] = foodPriceBonus;
        bonusList[1] = maxHungerBonus;
        bonusList[2] = clickPowerBonus;
        bonusList[3] = angerIncrease;
        return bonusList;
    }
}
