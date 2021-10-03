using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip menuMusic, raidMusic;

    public int raidLevelsIndex = 2;

    int prevScene = 0, currentScene = 0;

    public AudioMixer masterAudioMixer;

    public Slider master, music, sfx;

    private void Awake()
    {
        // set volume sliders
        //float volume;
        //masterAudioMixer.GetFloat("MasterVolume", out volume);
        //master.value = Mathf.Log10(volume) * 20;

        //masterAudioMixer.GetFloat("MusicVolume", out volume);
        //music.value = Mathf.Log10(volume) * 20;

        //masterAudioMixer.GetFloat("SFXVolume", out volume);
        //sfx.value = Mathf.Log10(volume) * 20;
    }

    public void SetMasterVolume(float volume)
    {
        masterAudioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        masterAudioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSFXVolume(float volume)
    {
        masterAudioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
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
