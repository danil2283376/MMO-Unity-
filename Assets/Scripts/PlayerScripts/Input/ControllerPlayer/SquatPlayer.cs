using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SquatPlayer : MonoBehaviour
{
    public float idleHeight = 2f;
    public float squatHeight = 1f;
    private CharacterController _controller;

    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Squat();
    }

    private void Squat()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            _controller.height = squatHeight;
        else
            _controller.height = idleHeight;
    }
}
