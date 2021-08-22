using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QAManager : MonoBehaviour
{

    public static float levelTimer = 0f;
    public int currentScene = -1;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (currentScene > -1)
            Debug.Log(SceneManager.GetSceneByBuildIndex(currentScene).name + " active for: " + levelTimer + " seconds.");
        
        levelTimer = 0f;

        currentScene = scene.buildIndex;
        
    }

    // Update is called once per frame
    void Update()
    {
        levelTimer += Time.deltaTime;
    }
}
