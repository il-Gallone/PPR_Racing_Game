using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AudioManager : MonoBehaviour
{
    public static UI_AudioManager ui_audioManager;

    public AudioClip[] clickSounds, hoverSounds, upgradeSounds;
    AudioPlayer audioPlayer;


    private void Awake()
    {
        if (ui_audioManager == null)
        {
            ui_audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();        
    }

    public void playClick()
    {
        audioPlayer.audioToPlay = clickSounds;
        audioPlayer.PlayAudioRandomPitch();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
