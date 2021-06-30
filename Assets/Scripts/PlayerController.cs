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

    public float maxHP;
    public float maxEnergy;
    public float HP;
    public float energy;

    int objectiveCount = 0;

    public Text objectiveCountText;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        HP = maxHP;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (energy > 0)
        {
            rigid2D.AddForce(acceleration * Time.deltaTime * transform.up * Input.GetAxis("Vertical"));
            rigid2D.angularVelocity = (handling * -Input.GetAxis("Horizontal"));
            energy -= (acceleration * Mathf.Abs(Input.GetAxis("Vertical") * 0.25f) + rigid2D.angularVelocity * 0.1f) * Time.deltaTime;
        }
        if (rigid2D.velocity.magnitude > speedThreshold)
        {
            rigid2D.drag = Mathf.Pow(2, (rigid2D.velocity.magnitude - speedThreshold) * 0.01f + 1);
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
            SceneController.UpdateScene(3);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HP -= collision.relativeVelocity.magnitude* 5;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            objectiveCount++;
            if(objectiveCount >= GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourceManager>().objectiveCount)
            {
                SceneController.UpdateScene(2);
            }

            Destroy(collision.gameObject);

            objectiveCountText.text = "Objectives: " + objectiveCount;
        }
        if(collision.CompareTag("EnemyBullet"))
        {
            HP -= collision.gameObject.GetComponent<BulletController>().damage;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Explosion"))
        {
            HP -= collision.gameObject.GetComponent<ExplosionController>().damage;
        }
    }
}
