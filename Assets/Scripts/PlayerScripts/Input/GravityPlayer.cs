using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GravityPlayer : MonoBehaviour
{
    public Transform groundChecker;
    public LayerMask groundMask;
    public float gravity = -9.8f;
    public bool isDeactivate { get; set; } = true;

    [HideInInspector] public bool _isGround;
    [HideInInspector] public Vector3 _velocity;

    private readonly float _resetVelocity = 0f;
    public float groundDistance = 0.4f;

    private void FixedUpdate()
    {
        _isGround = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (_isGround == true && _velocity.y < 0)
            _velocity.y = _resetVelocity;
    }
}
