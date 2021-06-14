using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouse : MonoBehaviour
{
    public Vector2 speedMouse;
    public int limitRotation;
    
    private Camera _firstPerson;
    private GameObject _player;
    private float _xRot;
    private float _yRot;

    //public float speed = 0.1f;
    private float _angleX = 0.0f;
    private float _angleY = 0.0f;
    private float mouseX = 0.0f;
    private float mouseY = 0.0f;

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * speedMouse.x;
        mouseY = Input.GetAxis("Mouse Y") * speedMouse.y;
    }

    void LateUpdate()
    {
        _angleX += mouseX;
        _angleY += mouseY;
        _angleY = Mathf.Clamp(_angleY, limitRotation * -1, limitRotation);
        transform.eulerAngles = new Vector3(-_angleY, _angleX, 0.0f);
    }
}
