using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float energy = 10f;
    public float health = 0f, asteroidHealth = 50f;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rigid2D;

    public GameObject pickupPrefab;

    AudioPlayer audioPlayer;
    public AudioClip[] destroySounds, hitSounds;

    public GameObject explosionEffect;

    public float shakeIntensity = .4f, shakeDuration = .5f;

    private void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        SpriteManager spriteManager = GameObject.FindGameObjectWithTag("SpriteManager").GetComponent<SpriteManager>();
        spriteRenderer.sprite = spriteManager.asteroids[Random.Range(0, spriteManager.asteroids.Length)];
        transform.eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
        rigid2D.velocity = new Vector2(Random.Range(-0.2f,0.2f),Random.Range(-0.2f, 0.2f));
        rigid2D.velocity /= gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        rigid2D.angularVelocity = Random.Range(-45f, 45f);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > GameManager.instance.levelLimits + 5)
        {
            transform.position -= transform.position / transform.position.magnitude * (GameManager.instance.levelLimits + 5)*2;
        }
    }

    private void OnDestroy()
    {
        if (asteroidHealth <= 0)
            ScreenShake.Instance.ShakeCam(.2f, .25f);
    }

    public void LaserHit(float damage, float miningPrecision)
    {
        // check asteroid health
        asteroidHealth -= damage;
        if (asteroidHealth > 0)
        {
            return;
        }

        // resource gets damaged depending on the weapon
        GameObject pickup = Instantiate(pickupPrefab, transform.position, transform.rotation);
        pickup.GetComponent<ResourcePickup>().hpGiven = health * miningPrecision;
        pickup.GetComponent<ResourcePickup>().energyGiven = energy * miningPrecision;
        float totalResources = energy + health;
        SpriteRenderer[] renderers = pickup.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(health / totalResources, energy / totalResources, energy / totalResources);
        }

        //play destroy sound
        audioPlayer.PlayClipAt(destroySounds, .2f);

        // add explosion before destroying
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().startLifetime); // destroy object when done

        Destroy(gameObject);

        asteroidHealth = 0;
    }

    void OnHit(Collider2D collision, bool disable)
    {
        //play hit sound
        audioPlayer.PlayClipAt(hitSounds, .25f);

        //check asteroid health
        asteroidHealth -= collision.GetComponent<BulletController>().damage;
        if (disable)
            collision.GetComponent<BulletController>().DisableBullet();
        if (asteroidHealth > 0)
        {
            return;
        }

        asteroidHealth = 0;

        // resource gets damaged depending on the weapon
        GameObject pickup = Instantiate(pickupPrefab, transform.position, transform.rotation);
        pickup.GetComponent<ResourcePickup>().hpGiven = health * collision.GetComponent<BulletController>().miningPrecision;
        pickup.GetComponent<ResourcePickup>().energyGiven = energy * collision.GetComponent<BulletController>().miningPrecision;
        float totalResources = energy + health;
        SpriteRenderer[] renderers = pickup.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].color = new Color(health / totalResources, energy / totalResources, energy / totalResources);
        }

        //play destroy sound
        audioPlayer.PlayClipAt(destroySounds, .2f);

        // add explosion before destroying
        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().startLifetime); // destroy object when done

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            OnHit(collision, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rebounder"))
        {
            OnHit(collision.collider, false);
        }
    }
}
