using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = SpriteManager.levelLogo;
        gameObject.GetComponent<SpriteRenderer>().color = SpriteManager.logoColour;
    }

}
