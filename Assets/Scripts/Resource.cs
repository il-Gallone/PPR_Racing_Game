using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public float energy = 10f;
    public float health = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            // resource gets damaged depending on the weapon
            energy *= collision.GetComponent<BulletController>().miningPrecision;
            health *= collision.GetComponent<BulletController>().miningPrecision;
        }

        // add to player energy/health here?
        if (tag == "EnergyAsteroid")
        {

        } 
        else if (tag == "RepairAsteroid")
        {

        }

        // add explosion before destroying?

        Destroy(collision.gameObject);
        Destroy(gameObject);

    }
}
