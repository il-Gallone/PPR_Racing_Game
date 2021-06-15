using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float energy = 10f;
    public float health = 0f;

    public GameObject pickupPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
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
            // add explosion before destroying?
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }





    }
}
