using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public delegate void twoIntParamDelegate(int a, int b);
public class StatesAnimationsAndBasicControlls : MonoBehaviour
{
    bool isSitting = true;
    bool isPlaying = false;
    bool blocked = false;
    bool hatMoved = false;

    public Sprite idle;
    public Sprite sitting;
    public Sprite idle_clicked;

    public int hunger = 20;
    public int maxHunger = 20;
    private int anger;

    public ScoreManagement Score;
    public ShopPanel shopPanel;
    public OnHatBought hatPurchaseManager;
    public Buttons buyButtons;

    public Animator FookAnim;

    public Text awrnssTxt;

    public event twoIntParamDelegate OnHungerLowered;
    public event EventHandler OnFookStanding;

    public GameObject hatsChanger;

    private int[] listPrev = new int[4];

    private void Start()
    {
        FookAnim = GetComponent<Animator>();

        shopPanel.SomePanelIsBlocking += BlockDetector;
    }
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(mouseRay);
        if (hitInfo.collider != null && !blocked)
        {
            Stand();

            if (Input.GetMouseButton(0))
            {
                hatsChanger.transform.position = new Vector3(-0.04f, -3.93f , 0);

                SpriteChange(idle_clicked);
                OnFookStanding?.Invoke(this, EventArgs.Empty);
            }
            if (Input.GetMouseButtonUp(0))
            {
                Click();
                hatsChanger.transform.position = new Vector3(-0.04f, -3.765f, 0);
            }
        }
        else
        {
            Sit();

            hatsChanger.transform.position = new Vector3(-0.04f, -3.765f, 0);
        }

        if (Input.GetKeyDown("i")) //Debug key
        {
            Score.score += 100;
        }
        if (Input.GetKeyDown("k")) //Debug key
        {
            buyButtons.FoodPrice = 30;
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

    public void BlockDetector(bool blocked)
    {
        this.blocked = blocked;
    }

    public void Subscribe()
    {
        hatPurchaseManager.OnHatBoughtEv += HatManagerBuy;
        hatPurchaseManager.OnHatEquipedEv += HatManagerEquip;
    }
    private void HatManagerBuy(int[] bonusList, int cost, Sprite sprite)
    {
        //0 - foodPrice, 1 - maxHunger, 2 - clickPower, 3- anger 
        buyButtons.FoodPrice -= bonusList[0];
        maxHunger += bonusList[1];
        Score.clickPow += bonusList[2];
        anger += bonusList[3];

        buyButtons.FoodPrice += listPrev[0];
        maxHunger -= listPrev[1];
        Score.clickPow -= listPrev[2];

        Score.Score -= cost;

        listPrev = bonusList;

        hatsChanger.GetComponent<SpriteRenderer>().sprite = sprite;
        
        hunger = maxHunger;
        buyButtons.EventTrigger = true;

    }
    private void HatManagerEquip(int[] bonusList ,Sprite sprite)
    {
        //0 - foodPrice, 1 - maxHunger, 2 - clickPower, 3- anger 
        buyButtons.FoodPrice -= bonusList[0];
        maxHunger += bonusList[1];
        Score.clickPow += bonusList[2];

        buyButtons.FoodPrice += listPrev[0];
        maxHunger -= listPrev[1];
        Score.clickPow -= listPrev[2];

        listPrev = bonusList;

        hatsChanger.GetComponent<SpriteRenderer>().sprite = sprite;

        hunger = maxHunger;
        buyButtons.EventTrigger = true;
    }
}
