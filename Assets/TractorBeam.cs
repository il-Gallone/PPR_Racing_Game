using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public float beamForce = 5f;

    public List<Transform> items = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.stats.currentModuleID != "Tractor Beam")
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scrap") || collision.CompareTag("Objective") || collision.CompareTag("Resource"))
        {
            items.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Scrap") || collision.CompareTag("Objective") || collision.CompareTag("Resource"))
        {
            items.Remove(collision.transform);
            print("object removed");
        }
    }
    private void Update()
    {
        foreach(Transform item in items)
        {
            if (item)
                item.position = Vector2.Lerp(item.position, transform.position, Time.deltaTime * beamForce);
        }
        
    }
}
