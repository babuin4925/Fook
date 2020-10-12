using System.Collections;
using System.Collections.Generic;
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
            HidePrice();
            Debug.Log("bought is now set to" + value);
        }
    }
    
    private Image icon;
    private Text productName;
    private Text description;
    private Text costTxt;
 
    void Start()
    {
        shopPanel = this.transform.parent.transform.parent.GetComponent<ShopPanel>();
        Debug.Log(shopPanel.manager.score);
        icon = transform.Find("ProductIcon").GetComponent<Image>();
        productName = transform.Find("ProductName").GetComponent<Text>();
        description = transform.Find("ProductDescription").GetComponent<Text>();
        costTxt = transform.Find("ProductPrice").GetComponent<Text>();
        
        costTxt.text = hat.cost + "g";
        description.text = hat.description;
        icon.sprite = hat.sprite;
        productName.text = hat.name;

        shopPanel.SomePanelIsBlocking += SetTextColor;
    }
    private void HidePrice()
    {
        if (bought == true)
        {
            costTxt.text = "Equip";
        }
    }
    private void SetTextColor(bool a)
    {
        if (a)
        {
            if (shopPanel.manager.score >= hat.cost)
            {
                productName.color = Color.black;
                costTxt.color = Color.black;
            }
            else
            {
                productName.color = new Color32((byte)231, (byte)93, (byte)93, (byte)255);
                costTxt.color = new Color32((byte)231, (byte)93, (byte)93, (byte)255);
            }
        }
    }

}
