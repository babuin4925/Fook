using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsWalk : MonoBehaviour
{
    public GameObject assetsText;
    new public AudioSource audio;

    private Vector3 defPosLeft = new Vector3(-10.25f,-3.08f,0);
    private float triggerX = 10.64f;

    private void Update()
    {
        if (!audio.isPlaying)
        {
            assetsText.SetActive(true);
        }

        this.transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);

        if(transform.position.x >= triggerX)
        {
            transform.position = defPosLeft;
        }
    }
}
