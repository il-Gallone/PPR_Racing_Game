using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonShot : BulletController
{
    //this bullet disables enemy ships temporarily

    public float disableTime = 5f; // the time a hit enemy will stay disabled

    public GameObject electricityPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponentInChildren<RadarPing>())
            return;
        if (collision.GetComponentInChildren<RadarPing>().CompareTag("Enemy"))
        {
            //GameObject electricity = new GameObject();
            //if (collision.GetComponent<EnemyStatus>().isEnabled)
            //{
                GameObject electricity =  Instantiate(electricityPrefab, collision.transform);
                Destroy(electricity, disableTime);
            //}
            
            //apply random rotation
            electricity.transform.Rotate(0, 0, Random.Range(0, 360));

            collision.GetComponent<EnemyStatus>().CancelInvoke();
            collision.GetComponent<EnemyStatus>().Invoke("EnableEnemy", disableTime);

            DisableEnemy(collision.gameObject);
            collision.GetComponent<EnemyStatus>().isEnabled = false;
            //StopCoroutine(collision.GetComponent<EnemyStatus>().EnableEnemy(disableTime));

        }

        base.OnHit(collision);
    }

    void DisableEnemy(GameObject enemy)
    {
        enemy.GetComponent<EnemyBase>().enabled = false;
    }

    
}
