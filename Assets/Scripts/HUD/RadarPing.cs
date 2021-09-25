using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    SpriteRenderer renderer;
    float pulse = 0;
    public bool directed;
    public Transform radarObject;
    public Transform targetCamera;

    // Start is called before the first frame update
    void Start()
    {
        radarObject = transform.parent;
        targetCamera = Camera.main.transform;
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!directed || (directed && Vector2.Distance(radarObject.position, targetCamera.position) >= 9.5f))
        {
            pulse += Time.deltaTime;
            if (pulse >= 1)
            {
                pulse--;
                renderer.enabled = !renderer.enabled; 
            }
        }
        else
        {
            renderer.enabled = false;
        }
        if (directed)
        {
            Vector2 direction = (radarObject.position - targetCamera.position);
            transform.position = (Vector2)targetCamera.position + direction * 9.5f / direction.magnitude;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
