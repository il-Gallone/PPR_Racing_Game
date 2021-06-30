using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetonator : EnemyBase
{

    public float detonationTime;
    public bool isDetonating = false;
    public float detonationRadius;
    public GameObject explosionPrefab;
    float blinkTime;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetDir = target.transform.position - transform.position;
        targetDir /= targetDir.magnitude;
        float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        float forwardAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float angleDif = targetAngle - forwardAngle;
        if (angleDif > 180) angleDif -= 360;
        else if (angleDif < -180) angleDif += 360;
        if (Vector3.Distance(target.transform.position, transform.position) > detonationRadius + 2f && Mathf.Abs(angleDif)<90)
        {
            rigid2D.AddForce(transform.up * acceleration * Time.deltaTime);
            rigid2D.drag = 1;
        }
        else if (Vector3.Distance(target.transform.position, transform.position) < detonationRadius - 2f || Mathf.Abs(angleDif) > 90)
        {
            rigid2D.AddForce(transform.up * -acceleration * Time.deltaTime);
            rigid2D.drag = 3;
        }
        rigid2D.angularVelocity = handling * angleDif * Time.deltaTime;
        if (Vector3.Distance(target.transform.position, transform.position) < detonationRadius && !isDetonating)
        {
            isDetonating = true;
            weapon.SetActive(true);
        }
        if (isDetonating)
        {
            detonationTime -= Time.deltaTime;
            blinkTime += Time.deltaTime;
            if (blinkTime >= detonationTime)
            {
                blinkTime = 0;
                weapon.SetActive(!weapon.activeInHierarchy);
            }
            if (detonationTime <= 0)
            {
                HPReachedZero();
            }
        }
    }

    public override void HPReachedZero()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        base.HPReachedZero();
    }
}
