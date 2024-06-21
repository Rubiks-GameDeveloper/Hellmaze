using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class MouseLook : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler, IDragHandler
{
    [SerializeField] private float horizontalMouseSens;
    [SerializeField] private float verticalMouseSens;
    private float _xRotation;
    [FormerlySerializedAs("_fpsParent")] [SerializeField] private Transform fpsParent;
    private Vector3 _data;
    private bool _isPointExit;
    private Vector2 _startTouchPos;
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void CameraRotator(PointerEventData eventData)
    {
        if (_isPointExit) return;
        var mouseX = eventData.delta.x * horizontalMouseSens * Time.deltaTime;
        var mouseY = eventData.delta.y * verticalMouseSens * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        Debug.Assert(Camera.main != null, "Camera.main != null");
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);
        fpsParent.Rotate(Vector3.up * mouseX);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _startTouchPos = eventData.position;
        _isPointExit = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        CameraRotator(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _startTouchPos = Vector3.zero;
        _isPointExit = true;
    }
}
