using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapParent : MonoBehaviour
{
    public int scrapTotal;

    // Update is called once per frame
    void Update()
    {
        if(scrapTotal <=0)
        {
            Destroy(gameObject);
        }
    }
}
