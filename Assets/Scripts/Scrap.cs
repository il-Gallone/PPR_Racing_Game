using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : Collectible
{
    public ScrapParent parent;

    public Rigidbody2D rigid2D;

    private void Start()
    {
        rigid2D.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        rigid2D.angularVelocity = Random.Range(-45f, 45f);
    }

    public override void OnPickup(GameObject player)
    {
        GameManager.instance.scrapCollected += Random.Range(2, 9);
        parent.scrapTotal--;
    }
}
