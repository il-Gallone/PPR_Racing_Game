using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void UpdateScene(int sceneIndex) 
    {
        if (sceneIndex >= 0)
            SceneManager.LoadScene(sceneIndex);
        else
            Application.Quit();
    }

    public static void LoadSceneByName(string sceneName)
    {
        if (sceneName == "")
        {
            Debug.Log("Please enter a scene name to load");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
