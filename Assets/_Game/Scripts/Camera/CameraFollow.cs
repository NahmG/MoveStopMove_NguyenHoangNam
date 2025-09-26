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
    public Camera cam;

    float mult = 1;

    void Awake()
    {
        Reset();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + currentCam.position * mult;
        Quaternion targetRotation = Quaternion.Euler(currentCam.rotation);

        transform.rotation = targetRotation;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
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