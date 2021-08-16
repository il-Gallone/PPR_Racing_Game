using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic, raidMusic;

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
        if (scene.buildIndex==0)
        {
            audioSource.clip = menuMusic;
            audioSource.Play();
        } else if (scene.buildIndex>=4)
        {
            audioSource.clip = raidMusic;
            audioSource.Play();
        }
        
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
    }
}
