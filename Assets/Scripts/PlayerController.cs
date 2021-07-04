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

    //public GameObject currentWeapon;
    public GameObject[] availableWeapons;

    // Start is called before the first frame update
    void Start()
    {
        engineMultiplier = 1 + PlayerPrefs.GetInt("PlayerEngineLevel")*0.1f;
        armourMultiplier = 1 + PlayerPrefs.GetInt("PlayerArmourLevel")*0.1f;
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        maxHP *= armourMultiplier;
        maxEnergy *= engineMultiplier;
        HP = maxHP;
        energy = maxEnergy;

        // set selected weapon - make sure availableWeapons is in correct order
        //currentWeapon = availableWeapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")];
        Instantiate(availableWeapons[PlayerPrefs.GetInt("PlayerCurrentWeapon")], transform);
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            objectiveCount++;
            int partChance = Random.Range(0, 3);
            if(partChance == 0)
            {
                GameManager.instance.weaponPartsCollected++;
            }
            if (partChance == 1)
            {
                GameManager.instance.enginePartsCollected++;
            }
            if (partChance == 2)
            {
                GameManager.instance.armourPartsCollected++;
            }
            if (objectiveCount >= GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>().objectiveCount)
            {
                PlayerPrefs.SetInt("PlayerScrapTotal", PlayerPrefs.GetInt("PlayerScrapTotal") + GameManager.instance.scrapCollected);
                PlayerPrefs.SetInt("PlayerEngineUpgrades", PlayerPrefs.GetInt("PlayerEngineUpgrades")+GameManager.instance.enginePartsCollected);
                PlayerPrefs.SetInt("PlayerWeaponUpgrades", PlayerPrefs.GetInt("PlayerWeaponUpgrades") + GameManager.instance.weaponPartsCollected);
                PlayerPrefs.SetInt("PlayerArmourUpgrades", PlayerPrefs.GetInt("PlayerArmourUpgrades") + GameManager.instance.armourPartsCollected);
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
            HP -= collision.gameObject.GetComponent<BulletController>().damage / armourMultiplier;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Explosion"))
        {
            HP -= collision.gameObject.GetComponent<ExplosionController>().damage / armourMultiplier;
        }
    }
}
