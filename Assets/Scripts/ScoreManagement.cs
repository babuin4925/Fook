
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int clickPow;
    public int score;
    public Text scoreTxt;

    public LayerMask LayerMask;
    private Vector2 camPos;

    private void Start()
    {
        Vector2 camPos = new Vector2(transform.position.x, transform.position.y);
}
    private void Update()
    {
        Vector2 direct = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(camPos, direct);
        if (hit)
        {
            Debug.Log("The ray hit " + hit.collider.name);
            scoreViewer();
        }

    }
    public void scoreViewer()
    {
        score = score + clickPow;
        scoreTxt.text = score.ToString();
    }

}
