using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float energy = 10f;
    public float health = 0f, asteroidHealth = 1f;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rigid2D;

    public GameObject pickupPrefab;

    AudioPlayer audioPlayer;
    public AudioClip[] destroySounds, hitSounds;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //play hit sound
            audioPlayer.PlayClipAt(hitSounds, .25f);

            //check asteroid health
            asteroidHealth -= collision.GetComponent<BulletController>().damage;
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
            for(int i =0; i<renderers.Length; i++)
            {
                renderers[i].color = new Color(health/totalResources, energy/totalResources, energy/totalResources);
            }

            //play destroy sound
            audioPlayer.PlayClipAt(destroySounds, 2f);

            // add explosion before destroying?
            collision.GetComponent<BulletController>().DisableBullet();
            Destroy(gameObject);
        }





    }
}
