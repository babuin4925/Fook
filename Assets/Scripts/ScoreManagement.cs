
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    public int clickPow = 1;
    public int score;
    public Text scoreTxt;
    private GameObject fook;
    public Sprite idle;
    private void Start()
    {
        fook = GameObject.Find("Fook");
    }
    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(mouseRay);
        if (hitInfo.collider!= null)
        {
            Debug.Log("The ray hit " + hitInfo.collider.name);
            fook.GetComponent<SpriteRenderer>().sprite = idle;
        }

    }
    public void scoreViewer()
    {
        score = score + clickPow;
        scoreTxt.text = score.ToString();
    }

}
