using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllegiancesManager : MonoBehaviour
{
    //Alpha
    public Slider faction1Slider;
    public Text faction1Percentage;
    public GameObject faction1Button;
    //Beta
    public Slider faction2Slider;
    public Text faction2Percentage;
    public GameObject faction2Button;
    //Gamma
    public Slider faction3Slider;
    public Text faction3Percentage;
    public GameObject faction3Button;
    //Omega
    public Slider faction4Slider;
    public Text faction4Percentage;
    public GameObject faction4Button;
    //Epsilon
    public Slider faction5Slider;
    public Text faction5Percentage;
    public GameObject faction5Button;

    public GameObject allegiancesPopUp;
    public Text popUpTitle;
    public Text popUpDescription;
    public int activePopUp = 0;

    CanvasGroup canvasGroup;
    bool fadeIn = false;
    public float fadeSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = allegiancesPopUp.GetComponent<CanvasGroup>();

        if (GameManager.instance.stats.faction1Favour > 100)
        {
            GameManager.instance.stats.faction1Favour = 100;
        }
        if (GameManager.instance.stats.faction1Favour < 0)
        {
            GameManager.instance.stats.faction1Favour = 0;
        }
        if (GameManager.instance.stats.faction2Favour > 100)
        {
            GameManager.instance.stats.faction2Favour = 100;
        }
        if (GameManager.instance.stats.faction2Favour < 0)
        {
            GameManager.instance.stats.faction2Favour = 0;
        }
        if (GameManager.instance.stats.faction3Favour > 100)
        {
            GameManager.instance.stats.faction3Favour = 100;
        }
        if (GameManager.instance.stats.faction3Favour < 0)
        {
            GameManager.instance.stats.faction3Favour = 0;
        }
        if (GameManager.instance.stats.faction4Favour > 100)
        {
            GameManager.instance.stats.faction4Favour = 100;
        }
        if (GameManager.instance.stats.faction4Favour < 0)
        {
            GameManager.instance.stats.faction4Favour = 0;
        }
        if (GameManager.instance.stats.faction5Favour > 100)
        {
            GameManager.instance.stats.faction5Favour = 100;
        }
        if (GameManager.instance.stats.faction5Favour < 0)
        {
            GameManager.instance.stats.faction5Favour = 0;
        }
        faction1Slider.value = GameManager.instance.stats.faction1Favour;
        faction1Percentage.text = GameManager.instance.stats.faction1Favour.ToString() + "%";
        faction2Slider.value = GameManager.instance.stats.faction2Favour;
        faction2Percentage.text = GameManager.instance.stats.faction2Favour.ToString() + "%";
        faction3Slider.value = GameManager.instance.stats.faction3Favour;
        faction3Percentage.text = GameManager.instance.stats.faction3Favour.ToString() + "%";
        faction4Slider.value = GameManager.instance.stats.faction4Favour;
        faction4Percentage.text = GameManager.instance.stats.faction4Favour.ToString() + "%";
        faction5Slider.value = GameManager.instance.stats.faction5Favour;
        faction5Percentage.text = GameManager.instance.stats.faction5Favour.ToString() + "%";
    }

    private void Update()
    {
        if (fadeIn)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }
        else if (!fadeIn)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
        }
    }

    public void GeneratePopUp(int factionID)
    {
        if (!fadeIn || activePopUp != factionID)
        {
            fadeIn = true;
            allegiancesPopUp.SetActive(true);
            activePopUp = factionID;
            allegiancesPopUp.transform.position = GetPopUpPosition(factionID);
            popUpTitle.text = GetPopUpTitle(factionID);
            popUpDescription.text = GetPopUpDescription(factionID);
        }
        else if (allegiancesPopUp.activeInHierarchy && activePopUp == factionID)
        {
            fadeIn = false;
            //allegiancesPopUp.SetActive(false);
        }
    }

    public Vector3 GetPopUpPosition(int factionID)
    {
        switch (factionID)
        {
            case 1:
                {
                    return faction1Button.transform.position;
                }
            case 2:
                {
                    return faction2Button.transform.position;
                }
            case 3:
                {
                    return faction3Button.transform.position;
                }
            case 4:
                {
                    return faction4Button.transform.position;
                }
            case 5:
                {
                    return faction5Button.transform.position;
                }
        }
        return allegiancesPopUp.transform.position;
    }

    public string GetPopUpTitle(int factionID)
    {
        string title = "Error Title not Found";
        switch (factionID)
        {
            case 1:
                {
                    title = "Alpha Faction";
                    break;
                }
            case 2:
                {
                    title = "Beta Faction";
                    break;
                }
            case 3:
                {
                    title = "Gamma Faction";
                    break;
                }
            case 4:
                {
                    title = "Omega Faction";
                    break;
                }
            case 5:
                {
                    title = "Epsilon Faction";
                    break;
                }
        }
        return title;
    }

    public string GetPopUpDescription(int factionID)
    {
        string primaryDescription = "Error Description not Found";
        string alignment = "Error Alignment not Found";
        string opposition = "Error Opposition not Found";
        string resource = "Error Resource not Found";

        switch (factionID)
        {
            case 1:
                {
                    primaryDescription = "A society that favours fair trade.";
                    alignment = "Good";
                    opposition = "Gamma";
                    resource = "Engine Parts";
                    break;
                }
            case 2:
                {
                    primaryDescription = "A peaceful cilization that wishes to cure all injury and disease.";
                    alignment = "Good";
                    opposition = "Omega";
                    resource = "Armour Parts";
                    break;
                }
            case 3:
                {
                    primaryDescription = "A monopolistic society that likes to prevent trade with anyone but themselves.";
                    alignment = "Evil";
                    opposition = "Alpha";
                    resource = "Scrap";
                    break;
                }
            case 4:
                {
                    primaryDescription = "A warmonging civilization that wants complete control over the galaxy.";
                    alignment = "Evil";
                    opposition = "Beta";
                    resource = "Weapon Parts";
                    break;
                }
            case 5:
                {
                    primaryDescription = "An organization of smugglers and bounty hunters just looking to make a quick buck.";
                    alignment = "Neutral";
                    opposition = "Everyone";
                    resource = "Everything";
                    break;
                }
        }
        string fullDesciption = primaryDescription + "\nAlignment: " + alignment + "\nPrimary Opposition: " + opposition + "\nPrimary Resource: " + resource;
        return fullDesciption;
    }
}
