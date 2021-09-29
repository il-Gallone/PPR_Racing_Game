using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollector : EnemyBase
{
    public float attackRadius = 4;
    public GameObject player;
    public GameObject[] potentialTargets;
    public int objectivesStolen = 0;
    public GameObject objectivePrefab;
    public bool isEscaping = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEscaping)
        {
            FindTarget();
            if (target == null)
            {
                Debug.Log("target missing");
                return;
            }
            Vector2 targetDir = target.transform.position - transform.position;
            targetDir /= targetDir.magnitude;

            rigid2D.AddForce(transform.up * acceleration * Time.deltaTime);
            rigid2D.drag = 1;
            if (rigid2D.velocity.magnitude > maxSpeed)
            {
                rigid2D.velocity /= rigid2D.velocity.magnitude;
                rigid2D.velocity *= maxSpeed;
            }

            float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            float forwardAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
            float angleDif = targetAngle - forwardAngle;
            if (angleDif > 180) angleDif -= 360;
            else if (angleDif < -180) angleDif += 360;
            rigid2D.angularVelocity = Mathf.Sign(angleDif)*Mathf.Min(handling, Mathf.Abs(angleDif));


            float weaponForwardAngle = Mathf.Atan2(weapon.transform.up.y, weapon.transform.up.x) * Mathf.Rad2Deg;
            Vector2 playerDir = player.transform.position - transform.position;
            float playerAngle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
            float weaponAngleDif = playerAngle - weaponForwardAngle;
            if (weaponAngleDif > 180) weaponAngleDif -= 360;
            else if (weaponAngleDif < -180) weaponAngleDif += 360;
            weapon.transform.eulerAngles += new Vector3(0, 0, Mathf.Sign(weaponAngleDif) * weapon.GetComponent<WeaponController>().turnSpeed) * Time.deltaTime;
            if (weapon.GetComponent<WeaponController>().shootInterval < weapon.GetComponent<WeaponController>().timeSinceLastShot &&
                Vector3.Distance(target.transform.position, transform.position) < attackRadius + 2)
            {
                for (int i = 0; i < weapon.GetComponent<WeaponController>().projectileCount; i++)
                {
                    weapon.GetComponent<WeaponController>().shoot();
                }
            }
        }
    }
    public override void HPReachedZero()
    {
        for(int i = 0; i < objectivesStolen; i++)
            Instantiate(objectivePrefab, transform.position, transform.rotation);
        objectivesStolen = 0;
        Escape();
    }

    void Escape()
    {
        GameObject.FindGameObjectWithTag("GenerationManager").GetComponent<GenerationManager>().objectiveCount -= objectivesStolen;
        rigid2D.velocity += (Vector2)transform.up * 8;
        isEscaping = true;
        Destroy(gameObject, 4);
    }


    void FindTarget()
    {
        potentialTargets = GameObject.FindGameObjectsWithTag("Objective");
        float targetDistance = Mathf.Infinity;
        if (potentialTargets.Length == 0)
        {
            Escape();
        }
        for (int i = 0; i < potentialTargets.Length; i++)
        {
            if (Vector2.Distance(potentialTargets[i].transform.position, transform.position) < targetDistance)
            {
                targetDistance = Vector2.Distance(potentialTargets[i].transform.position, transform.position);
                target = potentialTargets[i];
            }
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("Objective"))
        {
            if (!isEscaping)
            {
                objectivesStolen++;
                Destroy(collision.gameObject);
            }
        }
    }
}
