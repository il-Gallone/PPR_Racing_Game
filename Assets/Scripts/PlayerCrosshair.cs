using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCrosshair : MonoBehaviour
{
    public SpriteRenderer crosshair;

    private void OnDestroy()
    {
        Cursor.visible = true;
    }

    private void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gamePaused)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;
        }
    }
}
