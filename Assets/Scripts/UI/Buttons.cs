using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    public StatesAnimationsAndBasicControlls controller;
    public ScoreManagement scoreManagement;
    public HungerBar hungerBar;

    private int fookHunger;
    private int foodPrice = 10;
    public int FoodPrice
    {
        get
        {
            return foodPrice;
        }
        set
        {
            foodPrice = value;
            BuyFoodTxt.text = "-" + foodPrice + "g";
        }
    }
    public int IPPPrice = 100;
    public int MHPrice = 100;

    private double clickPricePower = 1.5f;
    private double MHPricePower = 6; 

    public Text BuyFoodTxt;
    public Text IPPBTxt;
    public Text MHBTxt;

    public event EventHandler OnFoodBuyed;
     
    public void BuyFood()
    {
        if (controller.hunger != controller.maxHunger + controller.bonusHunger)
        {
            if (scoreManagement.score >= foodPrice)
            {
                scoreManagement.score -= foodPrice;
                controller.hunger = controller.maxHunger + controller.bonusHunger;
                scoreManagement.scoreTxt.text = scoreManagement.score.ToString();
                OnFoodBuyed?.Invoke(this, EventArgs.Empty);
                
                BuyFoodTxt.text = "-" + foodPrice + "g";
            }
            else
            {
                YourePoor();
            }
        }
        else
        {
            scoreManagement.AwarenessTextShow("Fook's hunger is full!!");
        }
    }
    public void IncreasePoopPower()
    {
        if (scoreManagement.score >= IPPPrice)
        {
            scoreManagement.score -= IPPPrice;
            scoreManagement.clickPow++;
            IPPPrice = (int)(Math.Pow(2, clickPricePower)*50);
            clickPricePower += 0.5;
            scoreManagement.scoreTxt.text = scoreManagement.score.ToString();

            IPPBTxt.text = "-" + IPPPrice + "g";
        }
        else
        {
            YourePoor();
        }
    }
    public void IncreaseMaxHunger()
    {
        if(scoreManagement.score >= MHPrice)
        {
            scoreManagement.score -= MHPrice;
            controller.maxHunger += 10;
            MHPrice = (int)Math.Pow(2.5, MHPricePower);
            foodPrice += 10;
            MHPricePower += 0.5;


            MHBTxt.text = "-" + MHPrice + "g";
            BuyFoodTxt.text = "-" + foodPrice + "g";

            hungerBar.HungerBarController(controller.hunger, controller.maxHunger + controller.bonusHunger);
        }
        else
        {
            YourePoor();
        }
    }
    public void YourePoor()
    {
        scoreManagement.AwarenessTextShow("Not enough gold!");
    }
    public bool YourePoor(int price)
    {
        if (price > scoreManagement.score)
        {
            return true;
        }
        return false;
    }
}
