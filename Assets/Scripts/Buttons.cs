using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Runtime.InteropServices;

public class Buttons : MonoBehaviour
{
    public StatesAnimationsAndBasicControlls controller;
    public ScoreManagement scoreManagement;

    private int fookHunger;
    private int foodPrice = 10;
    private int IPPPrice = 50;
    private int MHPrice = 100;
    private int clickPower;

    public Text BuyFoodTxt;
    public Text IPPBTxt;
    public Text MHBTxt;

    private void Update()
    {
        fookHunger = controller.hunger;
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

                BuyFoodTxt.text = "-" + foodPrice + " gold";
            }
            else
            {
                scoreManagement.AwarenessTextShow("You don't have enough money!");
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
            IPPPrice = (int)(IPPPrice* 1.5);
            scoreManagement.scoreTxt.text = scoreManagement.score.ToString();

            IPPBTxt.text = "-" + IPPPrice + " gold";
        }
        else
        {
            scoreManagement.AwarenessTextShow("You don't have enough money!");
        }
    }
    public void IncreaseMaxHunger()
    {
        if(scoreManagement.score >= MHPrice)
        {
            scoreManagement.score -= MHPrice;
            controller.maxHunger += 10;
            MHPrice += 100;
            foodPrice += 5;

            MHBTxt.text = "-" + MHPrice + " gold";
            BuyFoodTxt.text = "-" + foodPrice + " gold";
        }
        else
        {
            scoreManagement.AwarenessTextShow("You don't have enough money!");
        }
    }
}
