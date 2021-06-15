using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public float acceleration;
    public float handling;
    public float speedThreshold;
    public float torqueThreshold;

    public float maxHP;
    public float maxEnergy;
    public float HP;
    public float energy;

    int objectiveCount = 0;

    public Text objectiveCountText;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        HP = maxHP;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (energy > 0)
        {
            rigid2D.AddForce(acceleration * Time.deltaTime * transform.up * Input.GetAxis("Vertical"));
            rigid2D.AddTorque(handling * Time.deltaTime * -Input.GetAxis("Horizontal"));
            energy -= (acceleration * Mathf.Abs(Input.GetAxis("Vertical")) + handling * Mathf.Abs(Input.GetAxis("Horizontal")) * 0.1f) * Time.deltaTime;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            objectiveCount++;

            Destroy(collision.gameObject);

            objectiveCountText.text = "Objectives: " + objectiveCount;
        }
    }
}
