using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HatButtonViewer : MonoBehaviour
{
    public ShopItem hat;
    private ShopPanel shopPanel;

    private bool bought;
    public bool Bought
    {
        get
        {
            return bought;
        }
        set
        {
            bought = value;
            costTxt.SetActive(false);
        }
    }
    
    private Image icon;
    private Text productName;

    private GameObject thisButton;
    private GameObject scrollerPanel;
    private GameObject scroller;
    private GameObject hatsPanel;

    private GameObject descriptionTxt;
    private GameObject costTxt;
    private GameObject buyButton;
    private HatsBuyButton buyButtonScript;

    void Start()
    {

        thisButton = transform.parent.gameObject;
        scrollerPanel = thisButton.transform.parent.gameObject;
        scroller = thisButton.transform.parent.gameObject;
        hatsPanel = scroller.transform.parent.gameObject;
        shopPanel = hatsPanel.transform.parent.GetComponent<ShopPanel>();

        icon = transform.Find("ProductIcon").GetComponent<Image>();
        productName = transform.Find("ProductName").GetComponent<Text>();

        costTxt = hatsPanel.transform.Find("CostText").gameObject;
        descriptionTxt = hatsPanel.transform.Find("DescriptionText").gameObject;
        buyButton = hatsPanel.transform.Find("BuyButton").gameObject;
        buyButton.SetActive(false);
        buyButtonScript = buyButton.GetComponent<HatsBuyButton>();

        icon.sprite = hat.sprite;
        productName.text = hat.name;

        shopPanel.SomePanelIsBlocking += SetTextColor;
        shopPanel.SomePanelIsBlocking += HideText;

    }
    private void SetTextColor(bool a)
    {
        if (a)
        {
            if (shopPanel.manager.score >= hat.cost)
            {
                productName.color = Color.black;
                costTxt.GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            else
            {
                if (!bought)
                {
                    productName.color = new Color32((byte)231, (byte)93, (byte)93, (byte)255);
                    costTxt.GetComponent<TextMeshProUGUI>().color = new Color32((byte)231, (byte)93, (byte)93, (byte)255);
                }
            }
        }
    }
    
    public void ShowDescription()
    {
        if (!bought)
        {
            costTxt.SetActive(true);
            costTxt.GetComponent<TextMeshProUGUI>().text = "Cost: " + hat.cost + "g";
            
            SetTextColor(true);
        }
        else
        {
            costTxt.SetActive(false);
        }

        descriptionTxt.SetActive(true);
        buyButton.SetActive(true);

        descriptionTxt.GetComponent<TextMeshProUGUI>().text = hat.description;

        buyButtonScript.CurrentHatButton = transform.gameObject;

    }

    private void HideText(bool b)
    {
        if (!b)
        {
            costTxt.SetActive(false);
            descriptionTxt.SetActive(false);
            buyButton.SetActive(false);
        }
    }
}
