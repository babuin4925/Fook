
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int clickPow = 1;
    public int score;
    public Text scoreTxt;

    public void scoreIncrease()
    {
        score = score + clickPow;
        scoreTxt.text = score.ToString();
    }

}
