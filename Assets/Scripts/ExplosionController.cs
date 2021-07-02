using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float damage;
    public float MaxSize;
    public CircleCollider2D damageCollider;

    
    // Update is called once per frame
    void Update()
    {
        damageCollider.radius += Time.deltaTime*2;
        if (damageCollider.radius >= MaxSize)
        {
            Destroy(gameObject);
        }
    }
}
