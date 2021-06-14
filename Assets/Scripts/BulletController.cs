using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed = 50f, damage = 50f, selfDestructTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.up * speed;

        // temporary solution to de-cluttering the scene
        Invoke("SelfDestruct", selfDestructTime);
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
