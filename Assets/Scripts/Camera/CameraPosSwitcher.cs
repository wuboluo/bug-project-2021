using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class CameraPosSwitcher : MonoBehaviour
{
    public static CameraPosSwitcher i;

    private Camera mainCamera;

    private void Awake()
    {
        i = this;
        mainCamera = GetComponent<Camera>();
    }

    public Vector3 ToScreenPos(Vector3 worldPos) => mainCamera.WorldToScreenPoint(worldPos);
    public Vector3 ToWorldPos(Vector3 screenPos) => mainCamera.ScreenToWorldPoint(screenPos);

    bool isShake;

    public void CameraShake(float duration, float strength)
    {
        if (!isShake)
        {
            StartCoroutine(Shake(duration, strength));
        }
    }

    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;
        var startPos = mainCamera.transform.position;

        while (duration > 0)
        {
            mainCamera.transform.position = Random.insideUnitSphere * strength + startPos;
            duration -= Time.deltaTime;
            yield return null;
        }

        isShake = false;
    }
}