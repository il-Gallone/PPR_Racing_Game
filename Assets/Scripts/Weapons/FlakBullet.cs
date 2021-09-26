using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakBullet : BulletController
{
    public Sprite[] flakSprites;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        rb.angularVelocity = Random.Range(-180, 180);
        bulletSprite.sprite = flakSprites[Random.Range(0, flakSprites.Length)];
        rb.velocity = transform.up * speed;
    }

}
