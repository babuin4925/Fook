using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HatBuyer(int[] bonusList, int cost, Sprite sprite);
public delegate void HatEquiper(int[] bonusList, Sprite sprite);
public class OnHatBought : MonoBehaviour
{
    private HatButtonViewer viewer;
    private ShopItem hat;
    public StatesAnimationsAndBasicControlls controlls;

    public event HatBuyer OnHatBoughtEv;
    public event HatEquiper OnHatEquipedEv;


    void Start()
    {
        viewer = GetComponent<HatButtonViewer>();
        hat = viewer.hat;
    }
    public void BuyOrEquip()
    {
        if (!viewer.Bought)
        {
            if (controlls.Score.Score >= hat.cost)
            {
                controlls.hatPurchaseManager = this;
                controlls.Subscribe();
                OnHatBoughtEv?.Invoke(hat.FormArray(), hat.cost, hat.sprite);
                viewer.Bought = true;

                controlls.shopPanel.PublishEvent();
            }
        }
        else
        {
            controlls.hatPurchaseManager = this;
            controlls.Subscribe();
            OnHatEquipedEv?.Invoke(hat.FormArray(), hat.sprite);
        }
    }
}
