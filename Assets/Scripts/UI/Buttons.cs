using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour
{
    public StatesAnimationsAndBasicControlls controller;
    public ScoreManagement scoreManagement;

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
    public int IPPPrice = 50;
    public int MHPrice = 100;
    private int clickPower;

    public float clickPricemultiplier = 2f;

    public Text BuyFoodTxt;
    public Text IPPBTxt;
    public Text MHBTxt;

    public event EventHandler OnFoodBuyed;
      
    private bool eventTrigger = false;
    public bool EventTrigger
    {
        get
        {
            return eventTrigger;
        }
        set
        {
            OnFoodBuyed?.Invoke(this, EventArgs.Empty);
            //eventTrigger = false;
            Debug.Log("Event =Triggered");
        }
    }

    private void Update()
    {
        
    }
    public void BuyFood()
    {
        if (controller.hunger != controller.maxHunger)
        {
            if (scoreManagement.score >= foodPrice)
            {
                scoreManagement.score -= foodPrice;
                controller.hunger = controller.maxHunger;
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
            IPPPrice = (int)(IPPPrice* clickPricemultiplier);
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
            MHPrice += 100;
            foodPrice += 10;


            MHBTxt.text = "-" + MHPrice + "g";
            BuyFoodTxt.text = "-" + foodPrice + "g";
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
