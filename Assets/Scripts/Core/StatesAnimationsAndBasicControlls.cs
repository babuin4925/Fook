using System;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public delegate void twoIntParamDelegate(int a, int b);
public class StatesAnimationsAndBasicControlls : MonoBehaviour
{
    bool isSitting = true;
    bool isPlaying = false;

    public Sprite idle;
    public Sprite sitting;
    public Sprite idle_clicked;

    public int hunger = 20;
    public int maxHunger = 20;

    public ScoreManagement Score;

    public Animator FookAnim;

    public Text awrnssTxt;

    public event twoIntParamDelegate OnHungerLowered;
    public event EventHandler OnFookStanding;

    private void Start()
    {
        FookAnim = GetComponent<Animator>();
    }
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(mouseRay);
        if (hitInfo.collider != null)
        {
            Stand();

            if (Input.GetMouseButton(0))
            {
                SpriteChange(idle_clicked);
                OnFookStanding?.Invoke(this, EventArgs.Empty);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Click();
            }
        }
        else
        {
            Sit();
        }

        if (Input.GetKeyDown("i")) //Debug key
        {
            Score.score += 100;
        }
    }

    public void Sit()
    {
        SpriteChange(sitting);
        FookAnim.enabled = true;
        isSitting = true;
    }
    public void Stand()
    {
        FookAnim.enabled = false;
        isSitting = false;
        isPlaying = false;
        SpriteChange(idle);
        OnFookStanding?.Invoke(this, EventArgs.Empty);
    }
    public void Click()
    {
        if (hunger -1 >= 0)
        {
            Score.scoreIncrease();
            hunger -= 1;
            OnHungerLowered?.Invoke(hunger, maxHunger);
        }
        else
        {
            Score.AwarenessTextShow("Fook can't poop while hungry!");
        }
    }


    public void SpriteChange(Sprite neededSprite)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = neededSprite;
    }

}
