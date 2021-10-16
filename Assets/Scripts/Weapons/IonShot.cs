using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonShot : BulletController
{
    //this bullet disables enemy ships temporarily

    public float disableTime = 5f; // the time a hit enemy will stay disabled

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<RadarPing>().CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStatus>().CancelInvoke();
            collision.GetComponent<EnemyStatus>().Invoke("EnableEnemy", disableTime);

            DisableEnemy(collision.gameObject);

            //StopCoroutine(collision.GetComponent<EnemyStatus>().EnableEnemy(disableTime));
            
        }

        base.OnHit(collision);
    }

    void DisableEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyBase>().enabled = false;
    }

    
}
