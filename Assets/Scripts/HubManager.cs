using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubManager : MonoBehaviour
{
    public Text scrapTotal;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHubStats();
    }

    void UpdateHubStats()
    {
        scrapTotal.text = "Scrap: " + PlayerPrefs.GetInt("PlayerScrapTotal");

    }
}
