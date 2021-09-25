using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public bool destroyOnLoad = true;

    public AudioClip[] audioToPlay;
    public AudioSource audioSource;

    public float minPitch = .9f, maxPitch = 1.1f;

    private void Start()
    {
        if (!destroyOnLoad)
            DontDestroyOnLoad(this);    }

    public void PlayAudioRandomPitch()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        int randomIndex = Random.Range(0, audioToPlay.Length);

        audioSource.PlayOneShot(audioToPlay[randomIndex]);
        
    }

    public void PlayAudioRandomPitch(AudioClip[] clips, float minPitch, float maxPitch)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        int randomIndex = Random.Range(0, clips.Length);

        audioSource.PlayOneShot(clips[randomIndex]);

    }

    public void PlayClipAt(AudioClip[] clips, float volume)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);

        int randomIndex = Random.Range(0, clips.Length);

        GameObject tempSound = new GameObject();
        tempSound.transform.position = transform.position;
        AudioSource aSource = tempSound.AddComponent(typeof(AudioSource)) as AudioSource;

        aSource.clip = clips[randomIndex];
        aSource.volume = volume;
        aSource.pitch = randomPitch;

        aSource.Play();
        DontDestroyOnLoad(tempSound); // for when changing scenes
        Destroy(tempSound, clips[randomIndex].length);
    }

    public void PlayClipAt(float volume)
    {

        float randomPitch = Random.Range(minPitch, maxPitch);

        int randomIndex = Random.Range(0, audioToPlay.Length);

        GameObject tempSound = new GameObject();
        tempSound.transform.position = transform.position;
        AudioSource aSource = tempSound.AddComponent(typeof(AudioSource)) as AudioSource;

        aSource.clip = audioToPlay[randomIndex];
        aSource.volume = volume;
        aSource.pitch = randomPitch;

        aSource.Play();
        DontDestroyOnLoad(tempSound); // for when changing scenes
        Destroy(tempSound, audioToPlay[randomIndex].length);
    }

    /*
    public void PlayClipAtPoint_Random(AudioClip[] clips, float volume)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        int randomIndex = Random.Range(0, clips.Length);

        AudioSource.PlayClipAtPoint(clips[randomIndex], Camera.main.transform.position, volume);
    }
    */
}
