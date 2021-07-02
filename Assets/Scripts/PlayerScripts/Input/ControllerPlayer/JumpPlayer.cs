using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class JumpPlayer : MonoBehaviour
{
    public float jumpHeight = 3f;

    [HideInInspector] private GravityPlayer _gravityPlayer;

    private void Start()
    {
        _gravityPlayer = gameObject.GetComponent<GravityPlayer>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _gravityPlayer._isGround == true)
            Jump();
    }

    private void Jump()
    {
        // Formul jumps
        _gravityPlayer._velocity.y = Mathf.Sqrt(jumpHeight * (_gravityPlayer.gravity * -2));
    }
}
