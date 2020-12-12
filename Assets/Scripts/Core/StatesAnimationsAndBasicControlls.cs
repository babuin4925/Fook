using System;
using UnityEngine;
using UnityEngine.UI;
//using SystemNumerics or whatever was here

public delegate void twoIntParamDelegate(int a, int b);
public class StatesAnimationsAndBasicControlls : MonoBehaviour
{
    bool blocked = false;
    bool buttonActive = false;

    public Sprite idle;
    public Sprite sitting;
    public Sprite idle_clicked;

    public int hunger = 20;
    public int maxHunger = 20;
    public int anger = 0;
    public int bonusHunger = 0;

    private Vector3 buttonOffset = new Vector3(0,0,0); 

    public ScoreManagement Score;
    public ShopPanel shopPanel;
    [HideInInspector] public OnHatBought hatPurchaseManager;
    public Buttons buyButtons;

    private Animator FookAnim;

    public Text awrnssTxt;

    public event twoIntParamDelegate OnHungerLowered;
    public event EventHandler OnFookStanding;
    public event EventHandler OnFookReleased;
    public event EventHandler OnHatGot;

    public GameObject hatsChanger;
    public GameObject hatResetButton;
    [HideInInspector]public GameObject eyes;
    private GameObject eyebrows;

    private int[] listPrev = new int[4];

    public ParticleSystem poopParticles;
    private ParticleSystem.EmissionModule emission;

    private void Start()
    {
        FookAnim = GetComponent<Animator>();
        eyes = transform.Find("Eyes").gameObject;
        eyebrows = transform.Find("Eyebrows").gameObject;

        shopPanel.SomePanelIsBlocking += BlockDetector;

        emission = poopParticles.emission;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buttonActive)
        {
            Invoke("HideButton", 0.1f);
        }
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.GetRayIntersection(mouseRay);
        if (hitInfo.collider != null && !blocked)
        {
            Stand();

            if (Input.GetMouseButton(0)||Input.GetKey(KeyCode.Space))
            {
                hatsChanger.transform.position = new Vector3(-0.04f, -3.93f , 0);
                eyes.transform.localPosition = new Vector3(0,-0.01f,0);
                eyebrows.transform.localPosition = new Vector3(0.0015f, 0, 0);

                SpriteChange(idle_clicked);
                OnFookStanding?.Invoke(this, EventArgs.Empty);
            }
            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space))
            {
                Click();

                hatsChanger.transform.position = new Vector3(-0.04f, -3.765f, 0);
                eyes.transform.localPosition = new Vector3(0,0,0);
                eyebrows.transform.localPosition = new Vector3(0.0015f, 0.0084f, 0);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hatsChanger.GetComponent<SpriteRenderer>().sprite != null)
                {
                    hatResetButton.transform.Find("ResetText").GetComponent<Text>().text = "Reset";
                    hatResetButton.SetActive(true);
                    buttonActive = true;
                }
                else
                {
                    if (anger<100) 
                    {
                        hatResetButton.transform.Find("ResetText").GetComponent<Text>().text = "Let go";
                        hatResetButton.SetActive(true);
                        buttonActive = true;
                    }
                }
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
    }
    public void Stand()
    {
        FookAnim.enabled = false;
        SpriteChange(idle);
        OnFookStanding?.Invoke(this, EventArgs.Empty);
    }
    public void Click()
    {
        if (hunger -1 >= 0)
        {
            Score.scoreIncrease();
            hunger -= 1;
            OnHungerLowered?.Invoke(hunger, maxHunger + bonusHunger);

            emission.SetBurst(0, new ParticleSystem.Burst(0,Score.clickPow));
            poopParticles.Play();
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
        bonusHunger = bonusList[1];
        Score.clickPow += bonusList[2];

        buyButtons.FoodPrice += listPrev[0];
        Score.clickPow -= listPrev[2];

        Score.Score -= cost;

        listPrev = bonusList;

        hatsChanger.GetComponent<SpriteRenderer>().sprite = sprite;
        
        if (hunger > maxHunger + bonusHunger) 
        {
            hunger = maxHunger + bonusHunger;
        }
        OnHungerLowered(hunger, maxHunger + bonusHunger);
        OnHatGot?.Invoke(this, EventArgs.Empty);

        anger+=100;
        EyebrowCheck();
    }
    private void HatManagerEquip(int[] bonusList ,Sprite sprite)
    {
        //0 - foodPrice, 1 - maxHunger, 2 - clickPower, 3- anger 
        buyButtons.FoodPrice -= bonusList[0];
        bonusHunger = bonusList[1];
        Score.clickPow += bonusList[2];

        buyButtons.FoodPrice += listPrev[0];
        Score.clickPow -= listPrev[2];

        listPrev = bonusList;

        hatsChanger.GetComponent<SpriteRenderer>().sprite = sprite;

        if (hunger > maxHunger + bonusHunger)
        {
            hunger = maxHunger + bonusHunger;
        }
        OnHungerLowered(hunger, maxHunger + bonusHunger);

    }

    public void ResetHat()
    {
        if (hatsChanger.GetComponent<SpriteRenderer>().sprite != null)
        {
            buyButtons.FoodPrice += listPrev[0];
            bonusHunger = 0;
            Score.clickPow -= listPrev[2];

            hatsChanger.GetComponent<SpriteRenderer>().sprite = null;


            if (hunger > maxHunger + bonusHunger)
            {
                hunger = maxHunger + bonusHunger;
            }
            OnHungerLowered(hunger, maxHunger + bonusHunger);

        }
        else
        {
            LetGo();
        }
    }
    private void LetGo()
    {
        OnFookReleased?.Invoke(this, EventArgs.Empty);
    }

    private void HideButton()
    {
        hatResetButton.SetActive(false);
        buttonActive = false;
    }

    private void EyebrowCheck()
    {
        //ay yo, eyebrow check -__-
        if (anger >= 300)
        {
            eyebrows.SetActive(true);
        }
    }
}
