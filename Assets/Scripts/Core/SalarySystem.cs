﻿using System.Collections;
using UnityEngine;

public class SalarySystem : MonoBehaviour
{
    public ScoreManagement score;
    private int[] goldValues = {100,50,20};
    private string[] messages = { "You have recieved salary from your work! ", "Your mom sent you some cash! ", "You have found a couple coins on the floor! "};
    System.Random rnd = new System.Random();
    void Start()
    {

        StartCoroutine(SalaryTimer());
    }
    IEnumerator SalaryTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(rnd.Next(50,61));
            int randomElement = rnd.Next(0,3);
            score.Score += goldValues[randomElement];
            score.AwarenessTextShow(messages[randomElement]+ "+" + goldValues[randomElement] + "g", true,3);
        }
    }
}
