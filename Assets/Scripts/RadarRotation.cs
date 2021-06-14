using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRotation : MonoBehaviour
{
    public Transform targetPlayer;
    public Transform targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = targetPlayer.eulerAngles- targetCamera.eulerAngles;
    }
}
