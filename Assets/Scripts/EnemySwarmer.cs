using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwarmer : EnemyBase
{
    public GameObject[] swarm;
    public float attackRadius = 4;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateSwarm(true);
        target = GameObject.FindGameObjectWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetDir = target.transform.position - transform.position;
        targetDir /= targetDir.magnitude;
        if(Vector3.Distance(target.transform.position, transform.position) > attackRadius + 0.5f)
        {
            rigid2D.AddForce(targetDir * acceleration * Time.deltaTime);
        }
        if(Vector3.Distance(target.transform.position, transform.position) < attackRadius - 0.5f)
        {
            rigid2D.AddForce(targetDir * -acceleration * Time.deltaTime);
        }
        for(int i = 0; i < swarm.Length; i++)
        {
            float swarmSpread = 2*attackRadius*Mathf.Sin(Mathf.PI/swarm.Length);
            if (swarm[i] != gameObject)
            {
                if (Vector3.Distance(swarm[i].transform.position, transform.position) < swarmSpread - 0.3f)
                {
                    Vector2 moveDir = transform.position - swarm[i].transform.position;
                    moveDir /= moveDir.magnitude;
                    rigid2D.AddForce(moveDir * acceleration * Time.deltaTime);
                }
            }
        }
        if(rigid2D.velocity.magnitude > maxSpeed)
        {
            rigid2D.velocity /= rigid2D.velocity.magnitude;
            rigid2D.velocity *= maxSpeed;
        }
        float velocityAngle = Mathf.Atan2(rigid2D.velocity.y, rigid2D.velocity.x) * Mathf.Rad2Deg;
        float forwardAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float angleDif = velocityAngle - forwardAngle;
        if (angleDif > 180) angleDif -= 360;
        else if (angleDif < -180) angleDif += 360;
        rigid2D.angularVelocity = handling * angleDif * Time.deltaTime;

        float weaponForwardAngle = Mathf.Atan2(weapon.transform.up.y, weapon.transform.up.x) * Mathf.Rad2Deg;
        float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        float weaponAngleDif = targetAngle - weaponForwardAngle;
        if (weaponAngleDif > 180) weaponAngleDif -= 360;
        else if (weaponAngleDif < -180) weaponAngleDif += 360;
        weapon.transform.eulerAngles += new Vector3(0, 0, Mathf.Sign(weaponAngleDif) * weapon.GetComponent<WeaponController>().turnSpeed)*Time.deltaTime;
        if (weapon.GetComponent<WeaponController>().shootInterval < weapon.GetComponent<WeaponController>().timeSinceLastShot && 
            Vector3.Distance(target.transform.position, transform.position) < attackRadius + 2)
        {
            weapon.GetComponent<WeaponController>().shoot();
        }
    }

    public void UpdateSwarm(bool updateCaller)
    {
        if (HP <= 0)
        {
            tag = "DeadEnemy";
        }
        swarm = GameObject.FindGameObjectsWithTag("Swarmer");
        if (updateCaller)
        {
            for (int i = 0; i < swarm.Length; i++)
            {
                swarm[i].GetComponent<EnemySwarmer>().UpdateSwarm(false);
            }
        }
        if (gameObject.CompareTag("DeadEnemy"))
        {
            //Instantiate(scrapPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public override void HPReachedZero()
    {
        UpdateSwarm(true);
    }
}
