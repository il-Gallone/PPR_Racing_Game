using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : Collectible
{
    public ScrapParent parent;

    public Rigidbody2D rigid2D;

    public bool isProjectile = false;

    AudioPlayer audioPlayer;

    private void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        if (!isProjectile)
        {
            rigid2D.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            rigid2D.angularVelocity = Random.Range(-45f, 45f);
        }
    }

    public override void OnPickup(GameObject player)
    {

        int randomNum = Random.Range(2, 9);
        GameManager.instance.scrapCollected += randomNum;
        parent.scrapTotal--;

        audioPlayer.PlayClipAt(audioPlayer.audioToPlay, .5f);
        //text popup
        player.GetComponent<PlayerController>().PopupText("+" + " " + randomNum + " Scrap", Color.yellow, 1f);
    }
}
