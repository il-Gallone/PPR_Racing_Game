using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
    public float fadeSpeed = 1;
    float fadeTime = 0;
    bool WPressed = false;
    bool APressed = false;
    bool SPressed = false;
    bool DPressed = false;
    bool Clicked = false;

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.stats.firstTime)
        {
            fadeTime = 60;
            GameManager.instance.stats.firstTime = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeTime > 0)
        {
            gameObject.GetComponent<CanvasGroup>().alpha += fadeSpeed * Time.deltaTime;
            fadeTime -= fadeSpeed * Time.deltaTime;
            if(Input.GetAxis("Horizontal") < -0.1f && !DPressed)
            {
                DPressed = true;
                fadeTime -= 10;
            }
            if (Input.GetAxis("Horizontal") > 0.1f && !APressed)
            {
                APressed = true;
                fadeTime -= 10;
            }
            if (Input.GetAxis("Vertical") > 0.1f && !WPressed)
            {
                WPressed = true;
                fadeTime -= 10;
            }
            if (Input.GetAxis("Vertical") < -0.1f && !SPressed)
            {
                SPressed = true;
                fadeTime -= 10;
            }
            if(Input.GetButtonDown("Fire1") && !Clicked)
            {
                Clicked = true;
                fadeTime -= 10;
            }
        }
        if(fadeTime<=0)
        {
            gameObject.GetComponent<CanvasGroup>().alpha -= fadeSpeed * Time.deltaTime;
        }
    }
}
