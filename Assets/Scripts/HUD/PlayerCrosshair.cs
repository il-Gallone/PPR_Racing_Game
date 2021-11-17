using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PlayerCrosshair : MonoBehaviour
{
    public SpriteRenderer crosshair;
    public PlayerController player;

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
        if (player == null)
        {
            player = GameObject.FindObjectOfType<PlayerController>();
        }

        if (!PauseMenu.gamePaused && player != null)
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));//Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 adjustPost = cursorPos - player.transform.position;


            float newMag = adjustPost.magnitude * 1.4f;

            adjustPost.Normalize();

            adjustPost *= newMag;
            adjustPost += player.transform.position;





            //transform.position = cursorPos;
            transform.position = new Vector3(adjustPost.x, adjustPost.y, 10f);

            //float camVertical = Camera.main.orthographicSize * 2;
            //float camHorizontal = camVertical * Camera.main.aspect;

            //Vector2 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //offset = new Vector2((camHorizontal / Screen.width), (camVertical / Screen.height));

            //transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            //                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + offset;
        }
    }
}
//TODO maybe this work?>https://www.youtube.com/watch?v=0jTPKz3ga4w&ab_channel=CodeMonkey