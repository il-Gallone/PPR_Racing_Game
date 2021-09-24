using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : Collectible
{
    public float hpGiven, energyGiven;

    public AudioClip[] healthSounds, energySounds;

    AudioPlayer audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();
    }

    public override void OnPickup(GameObject player)
    {
        player.GetComponent<PlayerController>().HP += hpGiven;
        if (player.GetComponent<PlayerController>().HP > player.GetComponent<PlayerController>().maxHP)
        {
            player.GetComponent<PlayerController>().HP = player.GetComponent<PlayerController>().maxHP;
        }
        player.GetComponent<PlayerController>().energy += energyGiven;
        if (player.GetComponent<PlayerController>().energy > player.GetComponent<PlayerController>().maxEnergy)
        {
            player.GetComponent<PlayerController>().energy = player.GetComponent<PlayerController>().maxEnergy;

        }

        if (hpGiven>0)
        {
            audioPlayer.PlayClipAt(healthSounds, .4f);
            //popup text
            player.GetComponent<PlayerController>().PopupText("+" + hpGiven + " HP", Color.green, 1);
        }
        else
        {
            audioPlayer.PlayClipAt(energySounds, .4f);
            //popup text
            player.GetComponent<PlayerController>().PopupText("+" + energyGiven + " Energy", Color.cyan, 1);
        }
    }
}
