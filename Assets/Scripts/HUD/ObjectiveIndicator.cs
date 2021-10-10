using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveIndicator : MonoBehaviour
{
    public float rotateSpeed = 1f;

    Transform target;
    Transform player;

    public void UpdateArrayDelayed(float delay)
    {
        Invoke("UpdateArray", delay);
    }

    public void UpdateArray()
    {
        GameObject[] objectives = GameObject.FindGameObjectsWithTag("Objective");

        float shortestDist = 1000f;
        for(int i = 0; i < objectives.Length; i++)
        {
            if (Vector2.Distance(objectives[i].transform.position, player.position) < shortestDist)
            {
                shortestDist = Vector2.Distance(objectives[i].transform.position, player.position);
                target = objectives[i].GetComponent<Transform>();
            }
        }

        //transform.position = target.transform.position; // for testing
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // delay to allow everything else to load first
        Invoke("UpdateArray", .5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            transform.up = Vector2.Lerp(transform.up,target.position - player.position, Time.deltaTime * rotateSpeed);
        }
            
    }
}
