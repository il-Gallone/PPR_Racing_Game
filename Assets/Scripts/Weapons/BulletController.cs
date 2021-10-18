using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool stopMoving = false;
    public bool canCollideWithEnemy = true;

    public float speed = 50f, damage = 50f, energyDamage = 50f, selfDestructTime = 5f;

    public float miningPrecision = .5f; // between 0-1; percentage of how much of the resources are left in-tact

    public SpriteRenderer bulletSprite;
    protected Collider2D bulletCollider;

    public GameObject shieldHit, hitParticle;
    

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
        if (bulletSprite)
            bulletSprite.enabled = false;
        rb.velocity = Vector2.zero;
        stopMoving = true;

    }

    protected void PlayHitEffects(Collider2D collision)
    {
        if (shieldHit)
        {
            GameObject temp = Instantiate(shieldHit, transform.position, transform.rotation);
            temp.GetComponent<FollowObjectStrict>().target = collision.gameObject.transform;
            Destroy(temp, temp.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        }

        if (hitParticle)
        {
            hitParticle.GetComponent<ParticleSystem>().startColor = bulletSprite.color;

            GameObject particle = Instantiate(hitParticle, transform.position, transform.rotation);
        }
    }

    protected void OnHit(Collider2D collision)
    {
        if (collision.CompareTag("EnergyAsteroid") || collision.CompareTag("RepairAsteroid"))
            return;

        if (!collision.CompareTag("Player") && CompareTag("Bullet"))
        {
            DisableBullet();
            PlayHitEffects(collision);

        }

        if (collision.CompareTag("Player") && CompareTag("EnemyBullet"))
        {
            DisableBullet();
            PlayHitEffects(collision);

        }

        if (collision.GetComponentInChildren<RadarPing>())
        {
            if (collision.GetComponentInChildren<RadarPing>().CompareTag("Enemy") && canCollideWithEnemy)
            {
                DisableBullet();
                PlayHitEffects(collision);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rebounder"))
        {
            canCollideWithEnemy = true;
            rb.velocity = collision.transform.up * speed;
            PlayHitEffects(collision);

            return;
        }

        OnHit(collision);
    }
}
