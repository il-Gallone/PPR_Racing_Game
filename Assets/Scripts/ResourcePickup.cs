using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : Collectible
{
    public float hpGiven, energyGiven;

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
    }
}
