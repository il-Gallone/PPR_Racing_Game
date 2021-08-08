using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Map_PlanetController : MonoBehaviour
{
    public GameObject planetInfo;

    public string sceneToLoad = "SCENE_NAME";
    public int planetDifficulty = 1;
    public int planetLevelLimits = 25;

    bool fadeIn = false;
    public float fadeSpeed = 5f;

    //TODO - add nice fade-in/fade-out animations?

    private void Start()
    {
        // set planet info?

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
        GameManager.instance.levelLimits = planetLevelLimits;
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
        // the following chuck of code fades in/out the planet info UI
        Color planetInfoColour = planetInfo.GetComponent<Image>().color;
        Color planetInfoTextColour = planetInfo.GetComponentInChildren<Text>().color;

        if (fadeIn)
        {
            planetInfo.SetActive(true);

            planetInfoColour.a += Time.deltaTime * fadeSpeed;
            planetInfoTextColour.a += Time.deltaTime * fadeSpeed;
            if (planetInfoColour.a > 250)
            {
                planetInfoColour.a = 250;
                planetInfoTextColour.a = 250;
            }

            planetInfo.GetComponent<Image>().color = planetInfoColour;
            planetInfo.GetComponentInChildren<Text>().color = planetInfoTextColour;

        }
        else if (!fadeIn)
        {

            planetInfoColour.a -= Time.deltaTime * fadeSpeed;
            planetInfoTextColour.a -= Time.deltaTime * fadeSpeed;

            if (planetInfoColour.a <= 0)
            {
                planetInfoColour.a = 0;
                planetInfoTextColour.a = 0;

                planetInfo.SetActive(false);
            }

            planetInfo.GetComponent<Image>().color = planetInfoColour;
            planetInfo.GetComponentInChildren<Text>().color = planetInfoTextColour;
        }

        if (IsMouseOverUI())// && planetInfo.activeInHierarchy == false)
        {
            fadeIn = true;
            //planetInfo.SetActive(true);
        } 
        else if (!IsMouseOverUI())// && planetInfo.activeInHierarchy)
        {
            fadeIn = false;
            //planetInfo.SetActive(false);
        }
    }
}
