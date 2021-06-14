using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    SpriteRenderer renderer;
    float pulse = 0;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        pulse += Time.deltaTime;
        if(pulse >=1)
        {
            pulse--;
            renderer.enabled = !renderer.enabled;
        }
    }
}
