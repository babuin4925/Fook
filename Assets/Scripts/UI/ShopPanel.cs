using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public delegate void boolPasser(bool b);
public class ShopPanel : MonoBehaviour
{
    public event boolPasser SomePanelIsBlocking; 

    private Animator panelAnim;
    private Animator fadeAnim;

    [SerializeField]private GameObject hatsPanel;
    [SerializeField]private GameObject otherPanel;

    public Text scoreInShop;
    public ScoreManagement manager;
    private void Start()
    {
        panelAnim = GetComponent<Animator>();
        GameObject canvas = GameObject.FindGameObjectWithTag("Main Canvas");
        fadeAnim = canvas.transform.Find("FadeScreen").GetComponent<Animator>();

    }
    public void ShopButton()
    {
        panelAnim.Play("ShopPanelSlide");
        fadeAnim.Play("FadeIn");
        scoreInShop.text = manager.score + "g";

        hatsPanel.SetActive(false);
        otherPanel.SetActive(false);

        SomePanelIsBlocking?.Invoke(true);
    }
    public void CloseShop()
    {
        panelAnim.Play("ShopPanelClose");
        fadeAnim.Play("FadeOut");
        SomePanelIsBlocking?.Invoke(false);
    }
    public void HatsPanel()
    {
        hatsPanel.SetActive(true);
        hatsPanel.GetComponent<Animator>().Play("HatsPanel");
        
    }
    public void OtherPanel()
    {
        otherPanel.SetActive(true);
        otherPanel.GetComponent<Animator>().Play("OtherPanel");
        
    }
}