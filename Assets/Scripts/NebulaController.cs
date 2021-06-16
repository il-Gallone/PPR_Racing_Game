using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaController : MonoBehaviour
{

    public ParticleSystem nebula;
    public Transform radar;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem.ShapeModule ps = nebula.shape;
        ps.radius = GameManager.instance.levelLimits + 5;
        radar.transform.localScale = new Vector3(2 * GameManager.instance.levelLimits, 2 * GameManager.instance.levelLimits , 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
