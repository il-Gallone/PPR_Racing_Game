using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Controls the player's weapon
    public float turnSpeed = 5f;

    public GameObject[] bulletPrefab;
    public Transform shootPos;

    public bool isPlayerWeapon = true;

    public bool automatic = true;


    public float shootInterval = .5f, minSpread = 0f, maxSpread = 0f;
    public int projectileCount = 1;

    float original_shootInterval;
    bool original_automatic;

    public float timeSinceLastShot;

    [Header("Screen Shake Settings")]
    public float shakeIntensity = .5f, shakeDuration = .25f;

    public AudioPlayer gunAudioPlayer;

    private void Start()
    {
        original_shootInterval = shootInterval;
        original_automatic = automatic;
    }

    private void Update()
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
                ScreenShake.Instance.ShakeCam(shakeIntensity, shakeDuration);
            }
            else if (timeSinceLastShot >= shootInterval && Input.GetButtonDown("Fire1"))          // for semi-auto
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    shoot();
                    
                }
                PlayShootSound();
                ScreenShake.Instance.ShakeCam(shakeIntensity, shakeDuration);
            }
        }
    }

    public void StartOvercharger(float fireRate_increase, float duration)
    {
        Invoke("EndOvercharger", duration);

        shootInterval = fireRate_increase * original_shootInterval;
        automatic = true;
    }
    protected void EndOvercharger()
    {
        shootInterval = original_shootInterval;
        automatic = original_automatic;
    }
    protected void RotateWeapon()
    {
        // Rotate weapon to mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = mousePos - transform.position;
        newPos = new Vector3(newPos.x, newPos.y, 0);

        // smoother rotation; doesn't work as ideal as possible right now -
        // weapon rotates slower when mouse is close
        transform.up = Vector3.Lerp(transform.up, newPos, Time.deltaTime * turnSpeed);
        //transform.up = newPos;
    }

    public virtual void shoot()
    {
        timeSinceLastShot = 0;

        float spread = Random.Range(minSpread, maxSpread);

        int randomBullet = Random.Range(0, bulletPrefab.Length);
        GameObject bullet = Instantiate(bulletPrefab[randomBullet], shootPos.position, shootPos.rotation);

        bullet.transform.Rotate(0, 0, spread);
        //print("shoot");
        
    }

    public void PlayShootSound()
    {
        if (gunAudioPlayer)
        {
            gunAudioPlayer.PlayClipAt(.1f);
        }
        else
            Debug.Log("ERROR: Please add the AudioPlayer script reference on the weapon prefab");
    }
}
