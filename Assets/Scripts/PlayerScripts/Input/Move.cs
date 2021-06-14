using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speedMove = 0.3f;
    public int maximumSpeed = 10;

    private Rigidbody _rb;
    private int _move;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float speed = _rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.W) && speed < maximumSpeed)
        {
            Vector3 movement = new Vector3(transform.forward.x, 0.0f, transform.forward.z);
            _rb.AddForce(movement * speedMove, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S) && speed < maximumSpeed)
        {
            Vector3 movement = new Vector3(-transform.forward.x, 0.0f, -transform.forward.z);
            _rb.AddForce(movement * speedMove, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A) && speed < maximumSpeed)
            _rb.AddForce(-transform.right * speedMove, ForceMode.Impulse);
        if (Input.GetKey(KeyCode.D) && speed < maximumSpeed)
            _rb.AddForce(transform.right * speedMove, ForceMode.Impulse);
    
    }
}
