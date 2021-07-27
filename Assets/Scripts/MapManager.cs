using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static string sceneToLoad;

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
