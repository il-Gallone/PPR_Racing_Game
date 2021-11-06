using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

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
            //Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //transform.position = cursorPos;
            //transform.position = new Vector3(cursorPos.x, cursorPos.y, 10f);

            float camVertical = Camera.main.orthographicSize * 2;
            float camHorizontal = camVertical * Camera.main.aspect;

            Vector2 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = new Vector2((camHorizontal / Screen.width), (camVertical / Screen.height));

            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + offset;
        }
    }
}
