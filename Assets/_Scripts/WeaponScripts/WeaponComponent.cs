using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 2nd Feb
public enum WeaponType
{
    None,
    Pistol,
    MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst
}

// 2nd Feb
// All the stats of a weapon, clip size, bullets in clip, fire rate, distance et cetera
[System.Serializable]
public struct WeaponStats
{
    WeaponType weaponType;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public WeaponFiringPattern firingPattern;
}


public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;

    protected WeaponHolder weaponHolder;

    [SerializeField] 
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        //mainCamera = Camera.main;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Initialize the weapon component
    /// 2nd Feb
    /// </summary>
    /// <param name="weaponHolder"></param>
    public void Initialize(WeaponHolder weaponHolder)
    {
        this.weaponHolder = weaponHolder;

    }


    // 2nd feb - weapon functionality 
    /// <summary>
    /// Repeating Weapon
    /// </summary>
    public virtual void StartFiringWeapon()
    {
        isFiring = true;

        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon),weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    /// <summary>
    /// Stop Firing Weapon
    /// </summary>
    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
    }

    protected virtual void FireWeapon()
    {
        Debug.Log("Firing Weapon!!!");
        weaponStats.bulletsInClip--;
    }
}
