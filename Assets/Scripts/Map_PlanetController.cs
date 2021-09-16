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

    public string sceneToLoad = "SCENE_NAME";
    public int planetDifficulty = 1;
    public int planetLevelLimits = 25;
    public int planetWeaponPartChance;
    public int planetEnginePartChance;
    public int planetArmourPartChance;
    public int planetFaction; //1 = Alpha, 2 = Beta, 3 = Gamma, 4 = Omega, 5 = Epsilon
    public int planetMapSeed; //0 = Alpha Homeworld, 1 = Beta Homeworld, 2 = Gamma Homeworld, 3 = Omega Homeworld, 4 = Epsilon Hideout
    public int planetDefaultEnemy; //0 = Swarmer, 1 = Detonator, 2 = Collector, 3 = CollectorPacifist

    bool fadeIn = false;
    public float fadeSpeed = 5f;

    private void Start()
    {
        // set planet info?
        canvasGroup = planetInfo.GetComponent<CanvasGroup>();
    }

    public void ChangeScene()
    {
        if (sceneToLoad == "" || sceneToLoad == null)
        {
            Debug.Log("Please enter a scene name to load");
            return;
        }
        //SceneManager.LoadScene(sceneToLoad);
        MapManager.sceneToLoad = sceneToLoad;
        MapManager.targetedFaction = planetFaction;
        GameManager.instance.mapGenerationSeed = planetMapSeed;
        GameManager.instance.primaryEnemySpawn = planetDefaultEnemy;
        GameManager.instance.levelLimits = planetLevelLimits;
        GameManager.instance.weaponPartChance = planetWeaponPartChance;
        GameManager.instance.enginePartChance = planetEnginePartChance;
        GameManager.instance.armourPartChance = planetArmourPartChance;
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
