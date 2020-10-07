using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public RectTransform panelTransform;

    private bool buttonPressed = false, onScreen = false, IsSliding = false;

    private float defaultOffScreenPos = 10.80f, defaultOnScreenPos = 7f, offset;

    private Vector3 offsetVector;

    [SerializeField]
    private float panelSpeed = 5; 

    
    //offscreen: x =7  onscreen x= 10.75
    void Update()
    {
        if (buttonPressed)
        {
            if (panelTransform.position.x > defaultOnScreenPos)
            {
                panelTransform.position += transform.up * Time.deltaTime * panelSpeed;
            }
            else
            {
                buttonPressed = false;
                onScreen = true;
                offset = defaultOnScreenPos - panelTransform.position.x;
                offsetVector = new Vector3(offset, 0f, 0f);
                panelTransform.position += offsetVector;
            }
        }
        //Debug.Log(Input.mousePosition.x);
        if (onScreen && Input.mousePosition.x < 570f)
        {
            IsSliding = true; //function that checks if our cursor is far from the panel and if the panel is on screen sets IsSliding to true
        }
        SlideReturn();
    }
    public void Button()
    {
        if (!onScreen&&!buttonPressed)
        {
            buttonPressed = true;
        }
    }
    private void SlideReturn()
    {
        if (panelTransform.position.x < defaultOffScreenPos) //until the panel reaches its offscreen idle position
        {
            if (IsSliding) //and is sliding is true 
            {
                panelTransform.position += -transform.up * Time.deltaTime * panelSpeed;
                // moves the panell towards offscreen idle 
            }
        }
        else
        {
            onScreen = false;
            IsSliding = false;
            //the panel has reached its offscreen idle meaning its offscreen ang no longer sliding
            offset = defaultOffScreenPos - panelTransform.position.x;
            offsetVector = new Vector3(offset, 0f, 0f);
            panelTransform.position += offsetVector;
        }
    }
}
