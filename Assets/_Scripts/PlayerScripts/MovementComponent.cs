///----------------------------------------------------------------------------------
///   Endless Horde Zombie Shooter
///   MovementComponent.cs
///   Author            : Vineet Kumar
///   Last Modified     : 2022/01/19
///   Description       : Movement and Animation Component of Player Character
///   Revision History  : 2nd ed. Adding Animation using the created blendtree
///                     and the parameters set in it for X and Y
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


    // Look
    private Vector2 lookInput = Vector2.zero;
    public float aimSensitivity = 1f;
    public GameObject followTarget;

    // Animator and animator hashes
    [Header("Animation")]
    private Animator _playerAnimator;
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("IsJumping");
    public readonly int isRunningHash = Animator.StringToHash("IsRunning");

    /// <summary>
    /// Awake gets called first, so better to get references from here than Start()
    /// </summary>
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        m_rb = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Aiming/Looking
        // if we aim up, down, adjust animations to have a mask that will let us properly animate Aim
        // horizontal
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);

        // vertical
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        // Clamp the rotation <- look for a better way using cinemachine
        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 70)
        {
            angles.x = 70;
        }

        followTarget.transform.localEulerAngles = angles;

        // Rotate the player based on look
        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);

        followTarget.transform.localEulerAngles = Vector3.zero;

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

        // Animation
        _playerAnimator.SetFloat(movementXHash, inputVector.x);
        _playerAnimator.SetFloat(movementYHash, inputVector.y);
    }

    /// <summary>
    /// Run for sprinting using Player Input System
    /// </summary>
    /// <param name="value"></param>
    public void OnRun(InputValue value)
    {
        _playerController.isRunning = value.isPressed;

        // Animation
        _playerAnimator.SetBool(isRunningHash, _playerController.isRunning);
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

        // Animation
        _playerAnimator.SetBool(isJumpingHash, _playerController.isJumping);
    }

    public void OnAim(InputValue value)
    {
        _playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
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
        
        // if colliding with an object (compare tag later)
        _playerController.isJumping = false;

        // set jumping animation to false
        _playerAnimator.SetBool(isJumpingHash, false);
    }
}