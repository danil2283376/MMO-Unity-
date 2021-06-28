using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public Transform groundChecker;
    public LayerMask groundMask;

    private CharacterController _controller;
    private bool _isGround;
    private Vector3 _velocity;
    private readonly float _resetVelocity = 0f;
    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        _isGround = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (_isGround == true && _velocity.y < 0)
            _velocity.y = _resetVelocity;
        MovePlayer();
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && _isGround == true)
            JumpPlayer();
        SquatPlayer();
        SprintPlayer();
    }

    private void JumpPlayer() 
    {
        // Formul jumps
        _velocity.y = Mathf.Sqrt(jumpHeight * (gravity * -2));
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        // free fall formula
        _controller.Move(move * speed * Time.deltaTime);
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void SquatPlayer()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            _controller.height = 1f;
        else
            _controller.height = 2f;
    }

    private void SprintPlayer()
    {
        if (Input.GetKey("left shift"))
            speed = 20f;
        else
            speed = 10f;
    }
}
