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
    private int clickPower;

    public Text awrnssTxt;

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
}
