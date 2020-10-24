
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int clickPow = 1;
    public int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreTxt.text = score.ToString();
        }
    }
    public Text scoreTxt;
    public Countdown timer;
    public Text awrnssTxt;

    private float secondsSince;
    private bool called = false;

    private void Update()
    {
        scoreTxt.text = score.ToString();

        if (called) //timer
        {
            secondsSince += Time.deltaTime;
            if (secondsSince >= 1f)
            {
                awrnssTxt.enabled = false;
                called = false;
            }
        }
    }
    public void scoreIncrease()
    {
        score = score + clickPow; 
    }
    public void AwarenessTextShow(string message)
    {
        awrnssTxt.enabled = true;
        awrnssTxt.text = message;

        called = true;
        secondsSince = 0;
    }
    private void hideTxt()
    {
        awrnssTxt.enabled = false;
    }
}
