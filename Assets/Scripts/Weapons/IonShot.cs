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
        if (!collision.gameObject)
            return;
        if (collision.GetComponentInChildren<RadarPing>().CompareTag("Enemy"))
        {
            GameObject electricity = Instantiate(electricityPrefab, collision.transform);
            Destroy(electricity, disableTime);

            //apply random rotation
            electricity.transform.Rotate(0, 0, Random.Range(0, 360));

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
