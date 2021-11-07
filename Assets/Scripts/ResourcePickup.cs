using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : Collectible
{
    public float hpGiven, energyGiven;

    public AudioClip[] healthSounds, energySounds;

    AudioPlayer audioPlayer;

    int refine_offset = 1;

    private void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        if (GameManager.instance.stats.currentModuleID=="Ore Refiner")
        {
            refine_offset = 2;
        }
    }

    public override void OnPickup(GameObject player)
    {
        if (GameManager.instance.stats.currentModuleID == "Ore Refiner")
        {
            int scrap = Random.Range(2, 9);
            GameManager.instance.scrapCollected += scrap;
            player.GetComponent<PlayerController>().PopupText("+" + scrap + " Scrap", Color.yellow, 1.5f);
        }

        player.GetComponent<PlayerController>().HP += hpGiven / refine_offset;
        if (player.GetComponent<PlayerController>().HP > player.GetComponent<PlayerController>().maxHP)
        {
            player.GetComponent<PlayerController>().HP = player.GetComponent<PlayerController>().maxHP;
        }
        player.GetComponent<PlayerController>().energy += energyGiven / refine_offset;
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
