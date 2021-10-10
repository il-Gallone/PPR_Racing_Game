using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    // based off https://www.youtube.com/watch?v=ACf1I27I6Tk

    public static ScreenShake Instance { get; private set; }

    private CinemachineVirtualCamera virtualCam;
    private float shakeTimer, shakeTimerTotal, startingIntensity;

    private void Awake()
    {
        Instance = this;
        virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCam(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = 
            virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin perlin =
                    virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                perlin.m_AmplitudeGain = 
                    Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
            }
        }
    }
    /*
    Transform camholder;

    private void Start()
    {
        camholder = Camera.main.GetComponentInParent<Transform>();
    }

    public static IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.position;

        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1f) * magnitude;
            float y = Random.Range(-1, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }
    */
}
