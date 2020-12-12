
using UnityEngine;

public delegate void boolPasser(bool b);
public class ShopPanel : MonoBehaviour
{
    public event boolPasser SomePanelIsBlocking; 

    private Animator panelAnim;
    private Animator fadeAnim;

    [SerializeField]private GameObject hatsPanel;
    [SerializeField]private GameObject otherPanel;

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

    public void PublishEvent(bool blocking)
    {
        SomePanelIsBlocking(blocking);
    }
}