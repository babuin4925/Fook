using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        isSitting = true;
        SpriteChange(sitting);
        
    }
    public void Stand()
    {
        isSitting = false;
        isPlaying = false;
        SpriteChange(idle);
        timer.SetTimer(5f, ()=> EarShake());
    }
    public void EarShake()
    {
        isPlaying = true;
        
    }
    public void Play()
    {
        isPlaying = true;
        //Starts playing animation
    }
    public void Click()
    {
        Score.scoreIncrease();
    }


    public void SpriteChange(Sprite neededSprite)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = neededSprite;
    }
}
