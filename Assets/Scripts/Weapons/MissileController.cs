using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : BulletController
{
    GameObject[] targets;

    Transform target;

    public float maxTargetDist = 5f;

    public float turnSpeed = 150f;

    public float detectionDelay = .5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        targets = GameObject.FindGameObjectsWithTag("Enemy");

        Invoke("FindTarget", detectionDelay); // I think sometimes if trying to detect straight away, the 'targets'
                                              // array does not get filled properly
    }

    void FindTarget()
    {
        int shortestDist = 0; //index of the target with the shortest distance
        float distance = maxTargetDist + 1;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i])
            {
                distance = Vector2.Distance(transform.position, targets[i].transform.position);
                if (targets.Length > 0 && targets[shortestDist] != null)
                {
                    if (distance < Vector2.Distance(transform.position, targets[shortestDist].transform.position))
                    {
                        shortestDist = i;
                    }
                }
            }  
        }
        if (targets.Length > 0 && targets[shortestDist] != null)
        {
            if (targets.Length > 0 && Vector2.Distance(transform.position, targets[shortestDist].transform.position) <= maxTargetDist)
            {
                target = targets[shortestDist].GetComponent<Transform>();
                print(Vector2.Distance(transform.position, targets[shortestDist].transform.position));
                //Debug.Log("Enemy: " + target.name);
            }
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target || stopMoving) // if there are no targets
        {
            return;
        }

        //from https://www.youtube.com/watch?v=0v_H3oOR0aU
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * turnSpeed;

        rb.velocity = transform.up * speed;
    }
}
