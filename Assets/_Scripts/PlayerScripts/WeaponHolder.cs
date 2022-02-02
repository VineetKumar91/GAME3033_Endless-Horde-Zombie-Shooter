///----------------------------------------------------------------------------------
///   Endless Horde Zombie Shooter
///   WeaponHolder.cs
///   Author            : Vineet Kumar
///   Last Modified     : 2022/02/02
///   Description       : Overall Weapon controls and Animations
///   Revision History  : 2nd ed. Adjusting animation of grip
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject WeaponToSpawn;


    public PlayerController _playerController;

    private Sprite crosshairImage;

    private Animator animator;
    [SerializeField]
    private GameObject WeaponScoketLocation;

    // 2nd Feb
    [SerializeField]
    private Transform GripIKScoketLocation;
    private WeaponComponent equippedWeapon;

    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");


    private bool wasFiring = false;
    private bool firingPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(WeaponToSpawn, WeaponScoketLocation.transform.position,
            WeaponScoketLocation.transform.rotation, WeaponScoketLocation.transform);

        animator = GetComponent<Animator>();

        // 2nd Feb
        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        GripIKScoketLocation = equippedWeapon.gripLocation;
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Animator IK
    /// </summary>
    /// <param name="layerIndex"></param>
    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKScoketLocation.transform.position);
    }

    public void OnFire(InputValue value)
    {
        // Call the actual fire weapon from Weapon Holder here

        firingPressed = value.isPressed;
        

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    // 2nd Feb
    // Reload
    public void OnReload(InputValue value)
    {
        _playerController.isReloading = value.isPressed;
        animator.SetBool(isReloadingHash, _playerController.isReloading);
    }

    // Start Firing weapon
    private void StartFiring()
    {

        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            return;
        }

        _playerController.isFiring = true;
        animator.SetBool(isFiringHash, true);
        equippedWeapon.StartFiringWeapon();
    }

    // Stop Firing weapon
    private void StopFiring()
    {
        _playerController.isFiring = false;
        animator.SetBool(isFiringHash,false);
        equippedWeapon.StopFiringWeapon();
    }

    // Reload weapon
    public void StartReloading()
    {

    }
}
