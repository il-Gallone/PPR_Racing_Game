using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip audioToPlay;
    public AudioSource audioSource;

    public float minPitch = .9f, maxPitch = 1.1f;

    public void PlayAudioRandomPitch()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        audioSource.PlayOneShot(audioToPlay);
    }
}
