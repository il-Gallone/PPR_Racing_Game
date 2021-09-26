using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : BulletController
{
    GameObject[] targets;

    Transform target;

    public float turnSpeed = 100f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        targets = GameObject.FindGameObjectsWithTag("Enemy");

        int shortestDist=0; //index of the target with the shortest distance
        for (int i=0; i < targets.Length; i++)
        {
            float distance = Vector2.Distance(transform.position, targets[i].transform.position);
            if (distance < Vector2.Distance(transform.position, targets[shortestDist].transform.position))
            {
                shortestDist = i;
            }
        }
        if (targets.Length > 0)
        {
            target = targets[shortestDist].GetComponent<Transform>();
            Debug.Log("Enemy: " + target.name);
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
