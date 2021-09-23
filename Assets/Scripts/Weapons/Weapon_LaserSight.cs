using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_LaserSight : MonoBehaviour
{
    public float maxRayDistance = 200f;

    public Transform firePoint;
    LineRenderer lineRenderer;

    WeaponController weaponController;

    private void Awake()
    {
        weaponController = GetComponent<WeaponController>();
        lineRenderer = GetComponent<LineRenderer>();

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0, 0);
        curve.AddKey(1, (weaponController.maxSpread / maxRayDistance)*4);

        lineRenderer.widthCurve = curve;
    }

    void ShootLaser()
    {
        //if (Physics2D.Raycast(firePoint.position, firePoint.up))
        //{
        //    RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.up);

        //    if (hit.collider.CompareTag("EnergyAsteroid") || hit.collider.CompareTag("RepairAsteroid") || hit.collider.CompareTag("Swarmer"))
        //        Draw2DRay(firePoint.position, hit.point);
        //    else
        //        Draw2DRay(firePoint.position, firePoint.up * maxRayDistance);
        //}
        //else
        //{
        //    Draw2DRay(firePoint.position, firePoint.up * maxRayDistance);
        //}

        //Draw2DRay(firePoint.position, firePoint.up * maxRayDistance);

        lineRenderer.SetPosition(1, new Vector3(0, maxRayDistance, 1));
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        //after lots of debugging I realised none of this is even necessary, considering it should just be a straight line and rotate with the parent object
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
