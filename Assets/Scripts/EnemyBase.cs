using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float HP;
    public float maxSpeed;
    public float acceleration;
    public float handling;
    public GameObject target;
    public GameObject weapon;
    public Rigidbody2D rigid2D;
    public GameObject scrapPrefab;

    private void Start()
    {
        EnemyManager.numOfEnemiesInScene++;
        //print(EnemyManager.numOfEnemiesInScene);
    }

    private void OnDestroy()
    {
        EnemyManager.numOfEnemiesInScene--;
        if (HP <= 0) // prevent on quit or on scene loading errors
            Instantiate(scrapPrefab, transform.position, transform.rotation);
        //print(EnemyManager.numOfEnemiesInScene);
    }

    public virtual void HPReachedZero()
    {
        Debug.Log("Enemy destroyed");
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            HP -= collision.gameObject.GetComponent<BulletController>().damage;
            collision.GetComponent<BulletController>().DisableBullet();
            if (HP <= 0)
            {
                HPReachedZero();
            }
        }
        if (collision.CompareTag("Explosion"))
        {
            HP -= collision.gameObject.GetComponent<ExplosionController>().damage;
            if (HP <= 0)
            {
                HPReachedZero();
            }
        }
    }
}
