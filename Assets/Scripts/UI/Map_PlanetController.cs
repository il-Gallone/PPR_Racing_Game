using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map_PlanetController : MonoBehaviour
{
    public GameObject planetInfo;

    CanvasGroup canvasGroup;
    public int planetID;
    public PlanetStats stats;
    public Text planetName;
    public Text planetDescription;

    bool fadeIn = false;
    public float fadeSpeed = 5f;

    private void Start()
    {
        // set planet info?
        PlanetUpdate();
        canvasGroup = planetInfo.GetComponent<CanvasGroup>();
    }
    public void PlanetUpdate()
    {
        stats = MapManager.instance.planets[planetID];
        MapManager.instance.planetButtons[planetID] = this;
        string name = "Neutral Territory";
        switch (stats.planetFaction)
        {
            case 1:
                {
                    name = "Alpha " + stats.planetName;
                    break;
                }
            case 2:
                {
                    name = "Beta " + stats.planetName;
                    break;
                }
            case 3:
                {
                    name = "Gamma " + stats.planetName;
                    break;
                }
            case 4:
                {
                    name = "Omega " + stats.planetName;
                    break;
                }
            case 5:
                {
                    name = "Epsilon " + stats.planetName;
                    break;
                }
            case 6:
                {
                    name = "Merchant " + stats.planetName;
                    break;
                }
            case 7:
                {
                    name = "Boss " + stats.planetName;
                    break;
                }
        }
        planetName.text = name;
        string favour = "Neutral";
        if (stats.planetFaction == 1)
        {
            if (GameManager.instance.stats.faction1Favour <= 15)
            {
                favour = "Enemy";
            }
            else if (GameManager.instance.stats.faction1Favour <= 35)
            {
                favour = "Unfriendly";
            }
            if (GameManager.instance.stats.faction1Favour >= 65)
            {
                favour = "Friendly";
            }
            if (GameManager.instance.stats.faction1Favour >= 90)
            {
                favour = "Allied";
            }
        }
        if (stats.planetFaction == 2)
        {
            if (GameManager.instance.stats.faction2Favour <= 15)
            {
                favour = "Enemy";
            }
            else if (GameManager.instance.stats.faction2Favour <= 35)
            {
                favour = "Unfriendly";
            }
            if (GameManager.instance.stats.faction2Favour >= 65)
            {
                favour = "Friendly";
            }
            if (GameManager.instance.stats.faction2Favour >= 90)
            {
                favour = "Allied";
            }
        }
        if (stats.planetFaction == 3)
        {
            if (GameManager.instance.stats.faction3Favour <= 15)
            {
                favour = "Enemy";
            }
            else if (GameManager.instance.stats.faction3Favour <= 35)
            {
                favour = "Unfriendly";
            }
            if (GameManager.instance.stats.faction3Favour >= 65)
            {
                favour = "Friendly";
            }
            if (GameManager.instance.stats.faction3Favour >= 90)
            {
                favour = "Allied";
            }
        }
        if (stats.planetFaction == 4)
        {
            if (GameManager.instance.stats.faction4Favour <= 15)
            {
                favour = "Enemy";
            }
            else if (GameManager.instance.stats.faction4Favour <= 35)
            {
                favour = "Unfriendly";
            }
            if (GameManager.instance.stats.faction4Favour >= 65)
            {
                favour = "Friendly";
            }
            if (GameManager.instance.stats.faction4Favour >= 90)
            {
                favour = "Allied";
            }
        }
        if (stats.planetFaction == 5)
        {
            if (GameManager.instance.stats.faction5Favour <= 15)
            {
                favour = "Enemy";
            }
            else if (GameManager.instance.stats.faction5Favour <= 35)
            {
                favour = "Unfriendly";
            }
            if (GameManager.instance.stats.faction5Favour >= 65)
            {
                favour = "Friendly";
            }
            if (GameManager.instance.stats.faction5Favour >= 90)
            {
                favour = "Allied";
            }
        }
        string description = "Favour: " + favour + "\n" + "Weapon Parts: " + stats.planetWeaponPartChance.ToString() + "%\n"
            + "Engine Parts: " + stats.planetEnginePartChance.ToString() + "%\n"
            + "Armour Parts: " + stats.planetArmourPartChance.ToString() + "%\n"
            + "Scrap: " + (100 - stats.planetWeaponPartChance - stats.planetEnginePartChance - stats.planetArmourPartChance).ToString() + "%";
        planetDescription.text = description;
    }

    public void ChangePlanet()
    {
        MapManager.targetedPlanet = planetID;
    }
    public void ChangeScene()
    {
        MapManager.targetedFaction = stats.planetFaction;
        GameManager.instance.mapGenerationSeed = stats.planetMapSeed;
        GameManager.instance.primaryEnemySpawn = stats.planetDefaultEnemy;
        GameManager.instance.levelLimits = stats.planetLevelLimits;
        GameManager.instance.weaponPartChance = stats.planetWeaponPartChance;
        GameManager.instance.enginePartChance = stats.planetEnginePartChance;
        GameManager.instance.armourPartChance = stats.planetArmourPartChance;
    }

    private bool IsMouseOverUI()
    {
        // idea adapted from https://www.youtube.com/watch?v=ptmum1FXiLE&t=93s
        // returns true when mouse is hovering over THIS gameObject

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // the following code fades in/out UI

        if (fadeIn)
        {
            planetInfo.SetActive(true);

            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
        }
        else if (!fadeIn)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
        }

        if (IsMouseOverUI())
        {
            fadeIn = true;
        } 
        else if (!IsMouseOverUI())
        {
            fadeIn = false;
        }
    }
}
