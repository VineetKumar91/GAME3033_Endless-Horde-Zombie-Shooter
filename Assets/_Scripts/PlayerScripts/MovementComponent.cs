///----------------------------------------------------------------------------------
///   Endless Horde Zombie Shooter
///   MovementComponent.cs
///   Author            : Vineet Kumar
///   Last Modified     : 2022/01/19
///   Description       : Movement Component of Player Character - James
///   Revision History  : 1st ed. Setting movement, jump and run
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    private Rigidbody m_rb;

    // Movement values
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;

    // Components
    private PlayerController _playerController;

    // References
    private Vector2 inputVector = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;

    /// <summary>
    /// Awake gets called first, so better to get references from here than Start()
    /// </summary>
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        m_rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // movement won't happen during jumping
        if (_playerController.isJumping)
        {
            return;
        }

        // if input vector magnitude is 0 or less,
        if (!(inputVector.magnitude > 0))
        {
            moveDirection = Vector3.zero;
        }

        // get direction of movement with transforms forward and right
        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        // set current speed depending on running to walk or run speed
        float currentSpeed = _playerController.isRunning ? runSpeed : walkSpeed;

        // get movement direction and adjust position
        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);
        transform.position += movementDirection;
    }

    /// <summary>
    /// Movement using Player Input System
    /// </summary>
    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    /// <summary>
    /// Run for sprinting using Player Input System
    /// </summary>
    /// <param name="value"></param>
    public void OnRun(InputValue value)
    {
        _playerController.isRunning = value.isPressed;
    }

    /// <summary>
    /// Jumping on Y axis using Player Input System
    /// </summary>
    /// <param name="value"></param>
    public void OnJump(InputValue value)
    {
        _playerController.isJumping = value.isPressed;

        // Impulse force upwards
        m_rb.AddForce((transform.up + moveDirection) * jumpForce, ForceMode.Impulse);
    }


    /// <summary>
    /// Collision just detected ?
    /// Collision Enter
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Ground") && !_playerController.isJumping)
        {
            return;
        }

        _playerController.isJumping = false;
    }
}