using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Map_PlanetController : MonoBehaviour
{
    public GameObject planetInfo;

    public string sceneToLoad = "Planet1";
    public int planetDifficulty = 1;

    //TODO - add nice fade-in/fade-out animations?

    private void Start()
    {
        // set planet info 

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
        if (IsMouseOverUI() && planetInfo.activeInHierarchy == false)
        {
            planetInfo.SetActive(true);
        } 
        else if (!IsMouseOverUI() && planetInfo.activeInHierarchy)
        {
            planetInfo.SetActive(false);
        }
    }
}
