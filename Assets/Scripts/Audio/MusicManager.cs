using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic, raidMusic;

    int prevScene = 0, currentScene = 0;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneChanged;
    }

    void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        prevScene = currentScene;
        currentScene = scene.buildIndex;

        if (scene.buildIndex== 0/*main menu*/ && prevScene != 1 /*map scene*/)
        {
            audioSource.clip = menuMusic;
            audioSource.Play();
        } else if (scene.buildIndex>=4) // raid levels should be above build index 4. Probably a better way to do this but it works for now
        {
            audioSource.clip = raidMusic;
            audioSource.Play();
        }
        
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
    }
}
