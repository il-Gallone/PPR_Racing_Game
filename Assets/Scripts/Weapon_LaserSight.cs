using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_LaserSight : MonoBehaviour
{
    public float maxRayDistance = 200f;

    public Transform firePoint;
    public LineRenderer lineRenderer;

    void ShootLaser()
    {
        if (Physics2D.Raycast(firePoint.position, firePoint.up))
        {
            RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up);
            Draw2DRay(firePoint.position, hit.point);
        }
        else
        {
            Draw2DRay(firePoint.position, firePoint.up * maxRayDistance);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
        //Debug.Log(startPos);
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }
}
