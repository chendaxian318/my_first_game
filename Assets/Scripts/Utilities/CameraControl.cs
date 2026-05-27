using UnityEngine;
using Unity.Cinemachine;
using System;
public class CameraControl : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO afterSceneLoadedEvent;
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impulseSource;
    public VoidEventSO cameraShakeEvent;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }
    private void OnEnable()
    {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised += OnAfterSceneLoadedEvent;
    }

    private void OnCameraShakeEvent()
    {
        impulseSource.GenerateImpulse();
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
        afterSceneLoadedEvent.OnEventRaised -= OnAfterSceneLoadedEvent;
    }
    //TODO：场景切换后更改
    private void OnAfterSceneLoadedEvent()
    {

        GetNewCameraBounds();
    }
    private void GetNewCameraBounds()
    {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj == null) return;
        
        confiner2D.BoundingShape2D = obj.GetComponent<Collider2D>();

        confiner2D.InvalidateBoundingShapeCache();

    }
}
