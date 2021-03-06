using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Controls the player's weapon
    public float turnSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform shootPos;

    public bool automatic = true;

    public float shootInterval = .5f, minSpread = 0f, maxSpread = 0f;
    public int projectileCount = 1;

    float timeSinceLastShot;

    private void Update()
    {
        // Rotate weapon to mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = mousePos - transform.position;
        newPos = new Vector3(newPos.x, newPos.y, 0);

        // smoother rotation; doesn't work as ideal as possible right now -
        // weapon rotates slower when mouse is close
        transform.up = Vector3.Lerp(transform.up, newPos, Time.deltaTime * turnSpeed);
        //transform.up = newPos;

        // Shooting check
        timeSinceLastShot += Time.deltaTime;

        if (automatic && timeSinceLastShot >= shootInterval && Input.GetButton("Fire1")) // for automatic fire
        {
            for (int i = 0; i < projectileCount; i++)
            {
                shoot();
            }
            
        }
        if (timeSinceLastShot >= shootInterval && Input.GetButtonDown("Fire1"))          // for semi-auto
        {
            for(int i = 0; i < projectileCount; i++)
            {
                shoot();
            }
        }
    }

    private void shoot()
    {
        timeSinceLastShot = 0;

        float spread = Random.Range(minSpread, maxSpread);

        GameObject bullet = Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);

        bullet.transform.Rotate(0, 0, spread);

        //print("shoot");
    }
}
