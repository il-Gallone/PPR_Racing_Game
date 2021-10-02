using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectStrict : MonoBehaviour
{
    public Transform target;

    public float shieldHitUpOffest = -.5f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + (transform.up * shieldHitUpOffest);
            //print("FOLLOWING");
        }
            
    }
}
