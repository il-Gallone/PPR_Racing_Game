using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int objectiveCount = 0;

    public Text objectiveCountText;

    public GameObject currentWeapon;
    public GameObject[] availableWeapons;

    AudioPlayer audioPlayer;
    public AudioClip[] bulletImpacts;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (energy > 0)
        {
            rigid2D.AddForce(acceleration * Time.deltaTime * transform.up * Input.GetAxis("Vertical")*engineMultiplier);
            rigid2D.angularVelocity = handling * -Input.GetAxis("Horizontal")*engineMultiplier;
            energy -= (acceleration * Mathf.Abs(Input.GetAxis("Vertical") * 0.25f) + rigid2D.angularVelocity * 0.1f) * Time.deltaTime/engineMultiplier;
        }
        if (rigid2D.velocity.magnitude > speedThreshold*engineMultiplier)
        {
            rigid2D.drag = Mathf.Pow(2, (rigid2D.velocity.magnitude - speedThreshold*engineMultiplier) * 0.01f + 1);
        }
        else
        {
            rigid2D.drag = 1;
        }
        if (Vector3.Distance(transform.position, Vector3.zero) > GameManager.instance.levelLimits)
        {
            {
                HP -= (Vector3.Distance(transform.position, Vector3.zero) - GameManager.instance.levelLimits) * Time.deltaTime;
            }
        }
        if (HP <= 0 || energy <= 0)
        {
            GameManager.instance.scrapCollected = 0;
            GameManager.instance.weaponPartsCollected = 0;
            GameManager.instance.enginePartsCollected = 0;
            GameManager.instance.armourPartsCollected = 0;
            SceneController.UpdateScene(0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HP -= collision.relativeVelocity.magnitude* 5/armourMultiplier;

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
            }
            else if (partChance < GameManager.instance.weaponPartChance + GameManager.instance.enginePartChance)
            {
                GameManager.instance.enginePartsCollected++;
            }
            else if (partChance < GameManager.instance.weaponPartChance + GameManager.instance.enginePartChance + GameManager.instance.armourPartChance)
            {
                GameManager.instance.armourPartsCollected++;
            }
            else
            {
                GameManager.instance.scrapCollected += Random.Range(20, 81);
            }
            if (objectiveCount >= GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>().objectiveCount)
            {
                GameManager.instance.stats.scrap += GameManager.instance.scrapCollected;
                GameManager.instance.stats.engineParts += GameManager.instance.enginePartsCollected;
                GameManager.instance.stats.weaponParts += GameManager.instance.weaponPartsCollected;
                GameManager.instance.stats.armourParts += GameManager.instance.armourPartsCollected;
                GameManager.instance.scrapCollected = 0;
                GameManager.instance.weaponPartsCollected = 0;
                GameManager.instance.enginePartsCollected = 0;
                GameManager.instance.armourPartsCollected = 0;
                SceneController.UpdateScene(0);
            }

            Destroy(collision.gameObject);

            objectiveCountText.text = "Objectives: " + objectiveCount;
        }
        if(collision.CompareTag("EnemyBullet"))
        {
            audioPlayer.PlayAudioRandomPitch(bulletImpacts, 1.65f, 1.9f);

            HP -= collision.gameObject.GetComponent<BulletController>().damage / armourMultiplier;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Explosion"))
        {
            HP -= collision.gameObject.GetComponent<ExplosionController>().damage / armourMultiplier;
        }
    }
}
