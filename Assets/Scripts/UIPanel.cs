using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public RectTransform panelTransform;

    private bool isSlidingLeft = false, isSlidingRight = false, onScreen= false;

    public float defaultOffScreenPos = 10.75f, defaultOnScreenPos = 7f;
    //offscreen: x =7  onscreen x= 10.75
    void Update()
    {
        if (isSlidingLeft)
        {
            if (panelTransform.position.x > defaultOnScreenPos)
            {
                panelTransform.position += transform.up * Time.deltaTime * 5;
            }
            else
            {
                isSlidingLeft = false;
                onScreen = true;
            }
        }
        Debug.Log(Input.mousePosition.x);
        if (onScreen && Input.mousePosition.x < 600f)
        {
            isSlidingRight = true;
            if (panelTransform.position.x < defaultOffScreenPos&& isSlidingRight)
            {
                panelTransform.position += -transform.up * Time.deltaTime * 5;
            }
            else
            {
                onScreen = false;
                isSlidingRight = false;
            }
        }
    }
    public void Slide()
    {
        if (!onScreen&&!isSlidingLeft)
        {
            isSlidingLeft = true;
        }
    }
}
