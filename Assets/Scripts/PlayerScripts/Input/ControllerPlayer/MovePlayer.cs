using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour
{
    public float wasteOfStamina = 10f;
    public float sprintSpeed = 20f;
    public float speed = 10f;
    public float speedDownStamina = 2f;

    [HideInInspector] GravityPlayer gravityPlayer;

    private CharacterController _controller;
    private float _oldSpeed;
    private Vector3 _oldPosition;
    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        gravityPlayer = gameObject.GetComponent<GravityPlayer>();
        _oldSpeed = speed;
    }

    private void FixedUpdate()
    {
        Move();
        SprintPlayer();
        _oldPosition = transform.position;
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        speed = _oldSpeed;
        Vector3 move = transform.right * x + transform.forward * z;
        // free fall formula
        _controller.Move(move * speed * Time.deltaTime);
        gravityPlayer._velocity.y += gravityPlayer.gravity * Time.deltaTime;
        _controller.Move(gravityPlayer._velocity * Time.deltaTime);
    }

    private void SprintPlayer()
    {
        StaminaPlayer staminaPlayer = gameObject.GetComponent<StaminaPlayer>();
        if (Input.GetKey("left shift") && staminaPlayer.maxStamina > 0 && _oldPosition != transform.position)
        {
            speed = sprintSpeed;
            staminaPlayer.DownStamina(wasteOfStamina * Time.deltaTime);
            if (staminaPlayer.CurrentStamina == 0)
                staminaPlayer.LackOfStamina(speedDownStamina * Time.deltaTime);
        }
        else
        {
            speed = _oldSpeed;
            staminaPlayer.UpStamina(wasteOfStamina * Time.deltaTime);
        }
    }
}
