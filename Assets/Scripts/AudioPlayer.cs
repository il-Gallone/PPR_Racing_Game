using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] audioToPlay;
    public AudioSource audioSource;

    public float minPitch = .9f, maxPitch = 1.1f;

    public void PlayAudioRandomPitch()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        int randomIndex = Random.Range(0, audioToPlay.Length);

        audioSource.PlayOneShot(audioToPlay[randomIndex]);
        
    }

    public void PlayClipAtPoint_Random()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        int randomIndex = Random.Range(0, audioToPlay.Length);

        AudioSource.PlayClipAtPoint(audioToPlay[randomIndex], transform.position);
    }

    public void PlayClipAtPoint_Random(AudioClip[] clips, float volume)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        int randomIndex = Random.Range(0, clips.Length);

        AudioSource.PlayClipAtPoint(clips[randomIndex], Camera.main.transform.position, volume);
    }
}
