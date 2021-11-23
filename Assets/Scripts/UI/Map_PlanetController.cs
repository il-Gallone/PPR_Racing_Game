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
        }
        planetName.text = name;
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
