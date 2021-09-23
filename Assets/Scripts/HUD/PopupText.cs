using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    public GameObject popup;

    public float destroyTime = 3f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        popup.transform.rotation = Camera.main.transform.rotation;
    }
}
