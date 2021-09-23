using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakBullet : BulletController
{
    public Sprite[] flakSprites;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = Random.Range(-180, 180);
        bulletSprite.sprite = flakSprites[Random.Range(0, flakSprites.Length)];
        rb.velocity = transform.up * speed;

        // temporary solution to de-cluttering the scene
        Invoke("SelfDestruct", selfDestructTime);
    }

}
