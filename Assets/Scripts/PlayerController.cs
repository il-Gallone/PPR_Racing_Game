using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public float acceleration;
    public float handling;
    public float speedThreshold;

    public float engineMultiplier = 1;
    public float armourMultiplier = 1;

    public float maxHP;
    public float maxEnergy;
    public float HP;
    public float energy;

    public int objectiveCount = 0;

    public Text objectiveCountText;

    public GameObject currentWeapon;
    public GameObject[] availableWeapons;

    public float moduleResource;
    public float moduleCooldown;
    public string currentModule;

    AudioPlayer audioPlayer;
    public AudioClip[] bulletImpacts, objectivePickupSound;
    public bool isAnimationRunning = true;
    public bool isFadingIn = true;
    public Animator hyperspeed;

    public GameObject textPopupPrefab;

    public Animator screenFlash, hpBar, energyBar;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioPlayer>();

        engineMultiplier = 1 + PlayerPrefs.GetInt("PlayerEngineLevel")*0.1f;
        armourMultiplier = 1 + PlayerPrefs.GetInt("PlayerArmourLevel")*0.1f;
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        maxHP *= armourMultiplier;
        maxEnergy *= engineMultiplier;
        HP = maxHP;
        energy = maxEnergy;

        // set selected weapon - make sure availableWeapons is in correct order
        for(int i = 0; i < availableWeapons.Length; i++)
        {
            if (availableWeapons[i].name == GameManager.instance.stats.currentWeaponID)
            {
                currentWeapon = availableWeapons[i];
                break;
            }
            else
            {
                currentWeapon = availableWeapons[0];
            }
        }
        Instantiate(currentWeapon, transform);
        if(GameManager.instance.stats.currentModuleID != "Shield Generator")
        {
            GameObject.FindGameObjectWithTag("Shield").SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimationRunning && isFadingIn)
        {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 6f / 9f * Time.deltaTime);
            if(gameObject.GetComponent<SpriteRenderer>().color.a >= 1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                hyperspeed.gameObject.SetActive(false);
                isAnimationRunning = false;
                isFadingIn = false;
            }
        }
        if (isAnimationRunning && !isFadingIn)
        {
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 6f / 9f * Time.deltaTime);
            if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                SceneController.UpdateScene(0);
            }
        }
        if (!isAnimationRunning)
        {
            if (energy > 0)
            {
                rigid2D.AddForce(acceleration * Time.deltaTime * transform.up * Input.GetAxis("Vertical") * engineMultiplier);
                rigid2D.angularVelocity = handling * -Input.GetAxis("Horizontal") * engineMultiplier;
                energy -= (acceleration * Mathf.Abs(Input.GetAxis("Vertical") * 0.25f) + rigid2D.angularVelocity * 0.1f) * Time.deltaTime / engineMultiplier;

                energyBar.SetFloat("Energy", energy);
            }
            if (rigid2D.velocity.magnitude > speedThreshold * engineMultiplier)
            {
                rigid2D.drag = Mathf.Pow(2, (rigid2D.velocity.magnitude - speedThreshold * engineMultiplier) * 0.01f + 1);
            }
            else
            {
                rigid2D.drag = 1;
            }
            if (Vector3.Distance(transform.position, Vector3.zero) > GameManager.instance.levelLimits)
            {
                {
                    HP -= (Vector3.Distance(transform.position, Vector3.zero) - GameManager.instance.levelLimits) * Time.deltaTime;
                    ScreenFlash();
                }
            }
            if (moduleCooldown > 0)
            {
                moduleCooldown -= Time.deltaTime;
            }
            if (GameManager.instance.stats.currentModuleID == "Shield Generator")
            {
                if (moduleCooldown <= 0 && moduleResource < 20)
                {
                    moduleResource += Time.deltaTime;
                    if(moduleResource >= 20)
                    {
                        moduleResource = 20;
                    }
                }
                if (moduleResource > 0)
                {
                    GameObject.FindGameObjectWithTag("Shield").GetComponent<SpriteRenderer>().color = new Color((10 - moduleResource) / 10, (moduleResource - 5) / 10, (moduleResource - 10) / 10);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Shield").GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
                }
            }
            if(GameManager.instance.stats.currentModuleID == "Speed Booster")
            {
                if(moduleResource == 1 && moduleCooldown < 10)
                {
                    moduleResource = 0;
                    acceleration /= 3;
                    speedThreshold /= 2;
                }
                if(Input.GetButtonDown("Module") && moduleCooldown <= 0)
                {
                    moduleResource = 1;
                    acceleration *= 3;
                    speedThreshold *= 2;
                    moduleCooldown = 15;
                }
            }
            if (HP <= 0 || energy <= 0)
            {
                GameManager.instance.scrapCollected = 0;
                GameManager.instance.weaponPartsCollected = 0;
                GameManager.instance.enginePartsCollected = 0;
                GameManager.instance.armourPartsCollected = 0;
                hyperspeed.gameObject.SetActive(true);
                hyperspeed.Play("Hyperspeed");
                isAnimationRunning = true;
            }
            if (objectiveCount >= GameObject.FindGameObjectWithTag("GenerationManager").GetComponent<GenerationManager>().objectiveCount)
            {
                GameManager.instance.stats.scrap += GameManager.instance.scrapCollected;
                GameManager.instance.stats.engineParts += GameManager.instance.enginePartsCollected;
                GameManager.instance.stats.weaponParts += GameManager.instance.weaponPartsCollected;
                GameManager.instance.stats.armourParts += GameManager.instance.armourPartsCollected;
                GameManager.instance.scrapCollected = 0;
                GameManager.instance.weaponPartsCollected = 0;
                GameManager.instance.enginePartsCollected = 0;
                GameManager.instance.armourPartsCollected = 0;
                hyperspeed.gameObject.SetActive(true);
                hyperspeed.Play("Hyperspeed");
                isAnimationRunning = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScreenFlash();
        if (GameManager.instance.stats.currentModuleID == "Shield Generator" && moduleResource > 0)
        {
            if(moduleResource < collision.relativeVelocity.magnitude * 5)
            {
                HP -= collision.relativeVelocity.magnitude * 5 - moduleResource;
                moduleResource = 0;
                moduleCooldown = 2;
            }
            if (moduleResource > collision.relativeVelocity.magnitude * 5)
            {
                moduleResource -= collision.relativeVelocity.magnitude * 5;
                moduleCooldown = 2;
            }
        }
        else
            HP -= collision.relativeVelocity.magnitude* 5 / armourMultiplier;

        audioPlayer.PlayAudioRandomPitch();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            objectiveCount++;
            int partChance = Random.Range(0, 100);
            if(partChance < GameManager.instance.weaponPartChance)
            {
                GameManager.instance.weaponPartsCollected++;
                PopupText("+1 Weapon Part", Color.blue, 2f);
            }
            else if (partChance < GameManager.instance.weaponPartChance + GameManager.instance.enginePartChance)
            {
                GameManager.instance.enginePartsCollected++;
                PopupText("+1 Engine Part", Color.blue, 2f);
            }
            else if (partChance < GameManager.instance.weaponPartChance + GameManager.instance.enginePartChance + GameManager.instance.armourPartChance)
            {
                GameManager.instance.armourPartsCollected++;
                PopupText("+1 Armour Part", Color.blue, 2f);
            }
            else
            {
                int randomNum = Random.Range(20, 81);
                GameManager.instance.scrapCollected += randomNum;
                PopupText("+" + randomNum + " Scrap", Color.yellow, 2f);
            }

            audioPlayer.PlayClipAt(objectivePickupSound, .6f);
            Destroy(collision.gameObject);

            objectiveCountText.text = "Objectives: " + objectiveCount;
        }
        if(collision.CompareTag("EnemyBullet"))
        {
            ScreenFlash();
            audioPlayer.PlayAudioRandomPitch(bulletImpacts, 1.65f, 1.9f);
            if(GameManager.instance.stats.currentModuleID == "Shield Generator" && moduleResource > 0)
            {
                if(moduleResource >= collision.gameObject.GetComponent<BulletController>().energyDamage * 2)
                {
                    moduleResource -= collision.gameObject.GetComponent<BulletController>().energyDamage * 2;
                    collision.gameObject.GetComponent<BulletController>().energyDamage = 0;
                    moduleCooldown = 2;
                }
                if (moduleResource >= collision.gameObject.GetComponent<BulletController>().damage)
                {
                    moduleResource -= collision.gameObject.GetComponent<BulletController>().damage;
                    collision.gameObject.GetComponent<BulletController>().damage = 0;
                    moduleCooldown = 2;
                }
                if (moduleResource < collision.gameObject.GetComponent<BulletController>().energyDamage * 2)
                {
                    collision.gameObject.GetComponent<BulletController>().energyDamage -= moduleResource/2;
                    moduleResource = 0;
                    moduleCooldown = 2;
                }
                if (moduleResource < collision.gameObject.GetComponent<BulletController>().damage)
                {
                    collision.gameObject.GetComponent<BulletController>().damage -= moduleResource;
                    moduleResource = 0;
                    moduleCooldown = 2;
                }
            }
            HP -= collision.gameObject.GetComponent<BulletController>().damage / armourMultiplier;
            energy -= collision.gameObject.GetComponent<BulletController>().energyDamage;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Explosion"))
        {
            ScreenFlash();
            if (GameManager.instance.stats.currentModuleID == "Shield Generator" && moduleResource > 0)
            {
                if (moduleResource < collision.gameObject.GetComponent<ExplosionController>().damage)
                {
                    HP -= collision.gameObject.GetComponent<ExplosionController>().damage- moduleResource / armourMultiplier;
                    moduleResource = 0;
                    moduleCooldown = 2;
                }
                if (moduleResource >= collision.gameObject.GetComponent<ExplosionController>().damage)
                {
                    moduleResource -= collision.gameObject.GetComponent<ExplosionController>().damage;
                    moduleCooldown = 2;
                }
            }
            else
                HP -= collision.gameObject.GetComponent<ExplosionController>().damage / armourMultiplier;
        }
    }

    public void PopupText(string text, Color color, float Yoffset)
    {
        GameObject textPopup = Instantiate(textPopupPrefab, transform.position + (Camera.main.transform.up* Yoffset), transform.rotation);

        textPopup.GetComponent<TextMeshPro>().text = text;
        textPopup.GetComponent<TextMeshPro>().color = color;
    }

    public void ScreenFlash()
    {
        screenFlash.SetTrigger("TakeDamage");
        hpBar.SetFloat("HP", HP);
    }
}
