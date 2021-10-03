using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic, raidMusic;

    public int raidLevelsIndex = 2;

    int prevScene = 0, currentScene = 0;

    public AudioMixer masterAudioMixer;

    private void Awake()
    {
        // set volume sliders

    }

    public void SetMasterVolume(float volume)
    {
        masterAudioMixer.SetFloat("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        masterAudioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume(float volume)
    {
        masterAudioMixer.SetFloat("SFXVolume", volume);
    }

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
        } else if (scene.buildIndex>=raidLevelsIndex) // raid levels should be above build index 4. Probably a better way to do this but it works for now
        {
            audioSource.clip = raidMusic;
            audioSource.Play();
        }
        
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
    }
}
