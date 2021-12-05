using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : WeaponController
{
    [Header("Laser Settings")]
    public float damage = .1f, maxRange = 10f, width = .05f, widthFadeAmount = .05f, fadeAmount = 5f, miningPrecision = .8f;

    public LineRenderer beam;

    public AudioClip laserStart, laserContinue;

    public GameObject sparksPrefab;
    public float sparkScale = 0.25f;

    bool firing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.gamePaused)
            return;
        
        // fade beam in/out
        if (Input.GetButton("Fire1"))
        {
            beam.startColor = new Color(beam.startColor.r, beam.startColor.g, beam.startColor.b, beam.startColor.a + (fadeAmount * Time.deltaTime));
            beam.endColor = new Color(beam.endColor.r, beam.endColor.g, beam.endColor.b, beam.endColor.a + (fadeAmount * Time.deltaTime));
            if (beam.startWidth < width)
                beam.startWidth = beam.startWidth + (widthFadeAmount * Time.deltaTime);
            else
                beam.startWidth = width;
        }
        else
        {
            beam.startColor = new Color(beam.startColor.r, beam.startColor.g, beam.startColor.b, beam.startColor.a - (fadeAmount * Time.deltaTime));
            beam.endColor = new Color(beam.endColor.r, beam.endColor.g, beam.endColor.b, beam.endColor.a - (fadeAmount * Time.deltaTime));
            if (beam.startWidth > 0)
                beam.startWidth = beam.startWidth - (widthFadeAmount * Time.deltaTime);
            else
                beam.startWidth = 0;
        }

        // determine whether to play sound or not
        if (Input.GetButtonUp("Fire1"))
        {
            firing = false;
            GetComponent<AudioSource>().Stop();
            CancelInvoke();
            gunAudioPlayer.audioToPlay[0] = laserStart;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            firing = true;
            gunAudioPlayer.PlayAudioRandomPitch();
            float offset;
            if (GetComponent<AudioSource>().pitch < 1)
                offset = laserStart.length + (laserStart.length - (laserStart.length * GetComponent<AudioSource>().pitch)) - .1f;
            else if (GetComponent<AudioSource>().pitch > 1)
                offset = laserStart.length - ((laserStart.length * GetComponent<AudioSource>().pitch) - laserStart.length) -.04f;
            else
                offset = laserStart.length;

                Invoke("LaserContinue", offset);
            gunAudioPlayer.audioToPlay[0] = laserContinue;
        }
            

        // Shooting check
        timeSinceLastShot += Time.deltaTime;
        //Check if weapon is controlled by player
        if (isPlayerWeapon)
        {
            RotateWeapon();

            if (timeSinceLastShot >= shootInterval && Input.GetButton("Fire1")) // for automatic fire
            {
                
                for (int i = 0; i < projectileCount; i++)
                {
                    shoot();
                }
                ScreenShake.Instance.ShakeCam(shakeIntensity, shakeDuration);
            }
        }
    }

    void LaserContinue()
    {
        if (firing)
        {
            float offset;
            if (GetComponent<AudioSource>().pitch < 1)
                offset = laserContinue.length + (laserContinue.length - (laserContinue.length * GetComponent<AudioSource>().pitch)) - .1f;
            else if (GetComponent<AudioSource>().pitch > 1)
                offset = laserContinue.length - ((laserContinue.length * GetComponent<AudioSource>().pitch) - laserContinue.length) - .04f;
            else
                offset = laserContinue.length;

            Invoke("LaserContinue", offset);
            gunAudioPlayer.PlayAudioRandomPitch();
        }
    }

    public override void shoot()
    {
        if (Physics2D.Raycast(shootPos.position, shootPos.up, maxRange))
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPos.position, shootPos.up);

            float distance = Vector2.Distance(hit.point, transform.position);

            sparksPrefab.GetComponent<ParticleSystem>().startColor = beam.startColor;

            if (hit.collider.CompareTag("EnergyAsteroid") || hit.collider.CompareTag("RepairAsteroid"))
            {
                beam.SetPosition(1, new Vector3(0, distance, 1));
                hit.transform.GetComponent<Asteroid>().LaserHit(damage * Time.deltaTime, miningPrecision);
                GameObject sparks = Instantiate(sparksPrefab, hit.point, transform.rotation);
                sparks.transform.localScale = new Vector3(sparkScale, sparkScale, sparkScale);
            }
            else if (hit.collider.CompareTag("Swarmer") || hit.collider.CompareTag("Enemy"))
            {
                beam.SetPosition(1, new Vector3(0, distance, 1));
                hit.transform.GetComponent<EnemyBase>().HP -= damage * Time.deltaTime;
                hit.transform.GetComponent<EnemyBase>().CheckHP();
                GameObject sparks = Instantiate(sparksPrefab, hit.point, transform.rotation);
                sparks.transform.localScale = new Vector3(sparkScale, sparkScale, sparkScale);
            }
            else
                beam.SetPosition(1, new Vector3(0, maxRange, 1));
        }
        else
        {
            beam.SetPosition(1, new Vector3(0, maxRange, 1));
        }
    }
}
