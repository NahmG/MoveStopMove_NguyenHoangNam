using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class CameraFollow : SerializedMonoBehaviour
{
    [SerializeField]
    Dictionary<CAMERA_TYPE, CameraData> datas = new();
    [SerializeField]
    float speed = 15;

    Transform target;
    CameraData currentCam;
    float mult = 1;
    public Camera cam;
    Obstacle lastObs;

    void Awake()
    {
        Reset();
    }

    void LateUpdate()
    {
        if (target == null || currentCam == null) return;

        Vector3 targetPosition = target.position + currentCam.position * mult;
        Quaternion targetRotation = Quaternion.Euler(currentCam.rotation);

        transform.rotation = targetRotation;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

        CheckObjectInCam();
    }

    void CheckObjectInCam()
    {
        Vector3 dir = target.position - cam.transform.position;
        float dist = dir.magnitude;

        if (Physics.Raycast(cam.transform.position, dir.normalized, out RaycastHit hit, dist, LayerMask.GetMask(CONSTANTS.OBSTACLE_LAYER)))
        {
            if (hit.collider.TryGetComponent(out Obstacle obs) && lastObs != obs)
            {
                lastObs?.UnFocus();
                obs.OnFocus();
                lastObs = obs;
            }
        }
        else
        {
            lastObs?.UnFocus();
            lastObs = null;
        }
    }

    public void SetTarget(Transform target) => this.target = target;
    public void Reset() => mult = 1;
    public void Scale(float mult) => this.mult = mult;
    public void ChangeCamera(CAMERA_TYPE Id) => currentCam = datas[Id];
}

public enum CAMERA_TYPE
{
    MENU,
    SHOP,
    GAME_PLAY
}

[Serializable]
public class CameraData
{
    public Vector3 position;
    public Vector3 rotation;
}