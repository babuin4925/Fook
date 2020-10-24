using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Buttons buttons;
    private StatesAnimationsAndBasicControlls controller;
    
    [SerializeField]private Image scale1, scale2, scale3, scale4,scale5;
    
    private bool s1, s2, s3, s4, s5;

    private int scaleSize;

    private Image[] hungerBarUIArr = new Image[5];
    private bool[] hungerBarArr = new bool[5];
    private void Start()
    {
        hungerBarUIArr = new Image[] {scale1, scale2, scale3, scale4, scale5};
        SetScaleAll(hungerBarArr, true);

        controller = GetComponent<StatesAnimationsAndBasicControlls>();
        controller.OnHungerLowered += HungerBarController;

        buttons.OnFoodBuyed += HungerFiller;
    }
    public void HungerBarController(int hunger, int MaxHunger)
    {
        if (hunger == 0)
        {
            SetScaleAll(hungerBarArr, false);
        }
        else
        {
            if (hunger == MaxHunger)
            {
                SetScaleAll(hungerBarArr, true);
            }
            else
            {
                scaleSize = MaxHunger / 5;
                SetScale((int)(hunger / scaleSize) + 1, hungerBarArr);
            }
        }
    }
    private void SetScale(int number , bool[] boolArray)
    {
        SetScaleAll(hungerBarArr, false);
        for (int i = 0; i<number; i++)
        {
            boolArray[i] = true;
        }
        HungerDisplayer(hungerBarArr, hungerBarUIArr);
    }
    private void SetScaleAll(bool[] boolArray, bool value)
    {
        for (int i = 0; i < boolArray.Length; i++)
        {
            boolArray[i] = value;
        }
        HungerDisplayer(hungerBarArr, hungerBarUIArr);
    }
    private void HungerDisplayer(bool[] boolArray, Image[] imageArray)
    {
        for (int i = 0; i < imageArray.Length; i++)
        {
            imageArray[i].enabled = boolArray[i];
        }
    }

    private void HungerFiller(object o, EventArgs e)
    {
        SetScaleAll(hungerBarArr, true);
    }
}
