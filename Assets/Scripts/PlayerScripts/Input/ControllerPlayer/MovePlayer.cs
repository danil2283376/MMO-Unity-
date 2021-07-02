using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovePlayer : MonoBehaviour
{
    public float wasteOfStamina = 5f;
    public float sprintSpeed = 20f;
    public float speed = 10f;

    [HideInInspector] GravityPlayer gravityPlayer;

    private CharacterController _controller;
    private float _oldSpeed;
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
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        // free fall formula
        _controller.Move(move * speed * Time.deltaTime);
        gravityPlayer._velocity.y += gravityPlayer.gravity * Time.deltaTime;
        _controller.Move(gravityPlayer._velocity * Time.deltaTime);
    }

    private void SprintPlayer()
    {
        StaminaPlayer staminaPlayer = gameObject.GetComponent<StaminaPlayer>();
        if (Input.GetKey("left shift") && staminaPlayer.maxStamina > 0)
        {
            speed = sprintSpeed;
            staminaPlayer.DownStamina(wasteOfStamina * Time.deltaTime);
            if (staminaPlayer.CurrentStamina == 0)
                staminaPlayer.LackOfStamina();
        }
        else
        {
            speed = _oldSpeed;
            staminaPlayer.UpStamina(wasteOfStamina * Time.deltaTime);
        }
    }
}
