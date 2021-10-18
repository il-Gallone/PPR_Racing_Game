using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebounderShot : BulletController
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }
}
