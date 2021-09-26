using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool stopMoving = false;

    public float speed = 50f, damage = 50f, energyDamage = 50f, selfDestructTime = 5f;

    public float miningPrecision = .5f; // between 0-1; percentage of how much of the resources are left in-tact

    public SpriteRenderer bulletSprite;
    protected Collider2D bulletCollider;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();

        rb.velocity = transform.up * speed;

        // temporary solution to de-cluttering the scene
        Destroy(gameObject, selfDestructTime);
    }

    // disable bullet rather than immediately destroying, 
    // so that trail can catch up instead of dissapearing immediately and looking strange
    public void DisableBullet()
    {
        bulletCollider.enabled = false;
        bulletSprite.enabled = false;
        rb.velocity = Vector2.zero;
        stopMoving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            DisableBullet();
        }
    }
}
