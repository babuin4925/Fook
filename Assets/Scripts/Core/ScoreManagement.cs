
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
    public Text awrnssTxt;
    private Color32 defaultColor = new Color32((byte)231, (byte)110, (byte)110, (byte)255);
    private Color32 goodColor = new Color32((byte)110, (byte)231, (byte)110, (byte)255);

    private float secondsSince;
    private float seconds;
    private bool called = false;

    private void Update()
    {
        scoreTxt.text = score.ToString();

        if (called) //timer
        {
            secondsSince += Time.deltaTime;
            if (secondsSince >= seconds)
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
        awrnssTxt.color = defaultColor;
        awrnssTxt.enabled = true;
        awrnssTxt.text = message;

        called = true;
        secondsSince = 0;
        seconds = 1f;
    }
    public void AwarenessTextShow(string message, bool good, float seconds)
    {
        if (good) { awrnssTxt.color = goodColor; }
        else { awrnssTxt.color = defaultColor; }
        
        awrnssTxt.enabled = true;
        awrnssTxt.text = message;

        called = true;
        secondsSince = 0;

        this.seconds = seconds;
    }
}
