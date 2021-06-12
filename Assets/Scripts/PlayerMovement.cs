using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public float acceleration;
    public float handling;
    public float speedThreshold;
    public float torqueThreshold;


    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.AddForce(acceleration * Time.deltaTime * transform.up * Input.GetAxis("Vertical"));
        rigid2D.AddTorque(handling * Time.deltaTime * -Input.GetAxis("Horizontal"));
        if (rigid2D.velocity.magnitude > speedThreshold)
        {
            rigid2D.drag = Mathf.Pow(2, (rigid2D.velocity.magnitude - speedThreshold) * 0.01f + 1);
        }
        else
        {
            rigid2D.drag = 1;
        }
        if(Mathf.Abs(rigid2D.angularVelocity) > torqueThreshold)
        {
            rigid2D.angularDrag = Mathf.Abs(rigid2D.angularVelocity) - torqueThreshold+1f;
        }
        else
        {
            rigid2D.angularDrag = 1f;
        }
    }
}
