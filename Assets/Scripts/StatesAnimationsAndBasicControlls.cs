using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class StatesAnimationsAndBasicControlls : MonoBehaviour
{
    bool isSitting = true;
    bool isSittingIdle = true;
    bool isPlaying = false;
    bool isClicked = false;

    public Sprite idle;
    public Sprite sitting;
    public Sprite idle_clicked;

    public float seconds;

    public Countdown timer;
    public ScoreManagement Score;

    public Animator FookAnim;

    private void Start()
    {
        timer.SetTimer(5f, () => EarShake());
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
        FookAnim.SetBool("EarShaking", isPlaying);
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
        timer.SetTimer(5f, ()=> EarShake());
    }
    public void EarShake()
    {
        isPlaying = true;
        Invoke("EarShakeContinous",1f);
    }
    public void EarShakeContinous()
    {
        isPlaying = false;
        Invoke("EarShake",1f);
    }
    public void Click()
    {
        Score.scoreIncrease();
    }


    public void SpriteChange(Sprite neededSprite)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = neededSprite;
    }
    public void TestAnim()
    {
        FookAnim.enabled = true;
    }
}
