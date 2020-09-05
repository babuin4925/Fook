using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int clickPow;
    public int score;
    public Text scoreTxt;
    private void Update()
    {
        scoreTxt.text = score.ToString();
    }
    public void ButtonClick()
    {
        score = score + clickPow; 
    }

}
