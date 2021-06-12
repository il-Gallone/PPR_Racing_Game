using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetPlayer;
    public float maxDistance;
    public float maxAngle;
    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(targetPlayer.transform.position, new Vector3(transform.position.x, transform.position.y, targetPlayer.transform.position.z));
        float speedMultiplier = distance / maxDistance;
        Vector2 targetDir = (targetPlayer.transform.position - transform.position).normalized;
        rigid2D.velocity = targetDir * speedMultiplier * (targetPlayer.GetComponent<Rigidbody2D>().velocity.magnitude+distance);
        float angleDif = targetPlayer.transform.eulerAngles.z - transform.eulerAngles.z;
        if (angleDif > 180) angleDif -= 360;
        else if (angleDif < -180) angleDif += 360;
        float angledMultiplier = Mathf.Abs(angleDif) / maxAngle;
        rigid2D.angularVelocity = Mathf.Sign(angleDif) * angledMultiplier * (Mathf.Abs(targetPlayer.GetComponent<Rigidbody2D>().angularVelocity)+Mathf.Abs(angleDif));
    }
}
