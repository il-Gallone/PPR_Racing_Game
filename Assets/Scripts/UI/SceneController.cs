using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static void UpdateScene(int sceneIndex)
    {
        GameManager.instance.SaveData();
        if (sceneIndex >= 0) 
        {
            if (sceneIndex == 0 && GameManager.instance.stats.health <= 0)
            {
                GameManager.instance.NewSave();
            }
            SceneManager.LoadScene(sceneIndex);
        }
        else
            Application.Quit();
    }

    public static void LoadSceneByName(string sceneName)
    {
        GameManager.instance.SaveData();
        if (sceneName == "")
        {
            Debug.Log("Please enter a scene name to load");
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
