using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{

    public Sprite[] asteroids;
    public Sprite[] logos;

    public static Sprite levelLogo;
    public static Color logoColour;
    // Start is called before the first frame update
    void Awake()
    {
        levelLogo = logos[Random.Range(0, logos.Length)];
        logoColour = new Color(Random.Range(0, 2), Random.Range(0, 2), Random.Range(0, 2), 0.6f);
    }
}
