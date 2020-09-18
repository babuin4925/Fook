﻿
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int clickPow = 1;
    public int score;
    public Text scoreTxt;
    public Countdown timer;
    public Text awrnssTxt;
    public void scoreIncrease()
    {
        score = score + clickPow;
        scoreTxt.text = score.ToString();
    }
    public void AwarenessTextShow(string message)
    {
        awrnssTxt.enabled = true;
        awrnssTxt.text = message;
        Invoke("hideTxt",1f);
    }
    private void hideTxt()
    {
        awrnssTxt.enabled = false;
    }
}
