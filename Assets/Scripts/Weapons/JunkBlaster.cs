using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkBlaster : WeaponController
{
    public float minScale = .5f, maxScale = 1.5f;
    public float minInterval = .25f, maxInterval = .75f;

    public Sprite[] junkSprites;

    public override void shoot()
    {
        timeSinceLastShot = 0;

        float spread = Random.Range(minSpread, maxSpread);

        int randomBullet = Random.Range(0, bulletPrefab.Length);
        GameObject bullet = Instantiate(bulletPrefab[randomBullet], shootPos.position, shootPos.rotation);

        int randomSprite = Random.Range(0, junkSprites.Length);
        bullet.GetComponent<SpriteRenderer>().sprite = junkSprites[randomSprite];

        bullet.transform.Rotate(0, 0, spread);

        float randomScale = Random.Range(minScale, maxScale);
        bullet.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        float randomInterval = Random.Range(minInterval, maxInterval);
        shootInterval = randomInterval;

        bullet.GetComponent<BulletController>().damage *= randomScale;
        bullet.GetComponent<Rigidbody2D>().angularVelocity = randomScale*100;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gamePaused)
            return;

        // Shooting check
        timeSinceLastShot += Time.deltaTime;
        //Check if weapon is controlled by player
        if (isPlayerWeapon)
        {
            RotateWeapon();

            if (automatic && timeSinceLastShot >= shootInterval && Input.GetButton("Fire1")) // for automatic fire
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    shoot();
                }
                PlayShootSound();
                //ScreenShake.Instance.ShakeCam(shakeIntensity, shakeDuration);
            }
            else if (timeSinceLastShot >= shootInterval && Input.GetButtonDown("Fire1"))          // for semi-auto
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    shoot();

                }
                PlayShootSound();
                //ScreenShake.Instance.ShakeCam(shakeIntensity, shakeDuration);
            }
        }
    }
}
