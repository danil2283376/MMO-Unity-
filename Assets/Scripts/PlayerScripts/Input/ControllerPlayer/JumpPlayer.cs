using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class JumpPlayer : MonoBehaviour
{
    public float jumpHeight = 3f;
    public float staminaOnJump = 5f;

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
        StaminaPlayer staminaPlayer = gameObject.GetComponent<StaminaPlayer>();
        _gravityPlayer._velocity.y = Mathf.Sqrt(jumpHeight * (_gravityPlayer.gravity * -2));
        Debug.Log(staminaPlayer.CurrentStamina - staminaOnJump);
        if ((staminaPlayer.CurrentStamina - staminaOnJump) < 0)
        {
            float newStaminaOnJump = (staminaPlayer.CurrentStamina - staminaOnJump) * -1;
            staminaPlayer.DownStamina(staminaOnJump);
            staminaPlayer.LackOfStamina(newStaminaOnJump);
        }
        else if (staminaPlayer.CurrentStamina > 0)
            staminaPlayer.DownStamina(staminaOnJump);
    }
}
