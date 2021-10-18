using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyBase enemyBase;

    public bool isEnabled = true;

    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
    }

    public void EnableEnemy()
    {
        print("enemy enabled");
        enemyBase.enabled = true;
        isEnabled = true;
        //if (GetComponentInChildren<Animator>().CompareTag("Electricity"))
        //{
        //    Destroy(GetComponentInChildren<Animator>())
        //}
    }
    //public IEnumerator EnableEnemy(float disableTime)
    //{
    //    yield return new WaitForSeconds(disableTime);

    //    print("enemy enabled");
    //    enemyBase.enabled = true;
    //}
}
