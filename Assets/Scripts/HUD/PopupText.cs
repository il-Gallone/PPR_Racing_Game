using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public GameObject popup;

    public float destroyTime = 3f, randomOffsetAmountX=1f, randomOffsetAmountY = .5f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);

        // offset the position a random amount so that popups close to each other don't overlap so much
        float randomX = Random.Range(-randomOffsetAmountX, randomOffsetAmountX);
        float randomY = Random.Range(-randomOffsetAmountY, randomOffsetAmountY);
        //Vector3 offset = new Vector3(randomX, randomY, 0);

        transform.position += Camera.main.transform.up * randomY;
        transform.position += Camera.main.transform.right * randomX;
    }

    private void Update()
    {
        popup.transform.rotation = Camera.main.transform.rotation;
    }
}
