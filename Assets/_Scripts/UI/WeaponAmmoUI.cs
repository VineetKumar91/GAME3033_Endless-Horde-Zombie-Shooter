using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponAmmoUI : MonoBehaviour
{
    // All TMP components for updating UI
    [Header("Weapon Panel Ammo Updates")]
    [SerializeField] 
    private TextMeshProUGUI TMP_weaponName;
    [SerializeField]
    private TextMeshProUGUI TMP_currentBullets;
    [SerializeField]
    private TextMeshProUGUI TMP_totalBullets;


    [SerializeField] 
    private WeaponComponent weaponComponent;

    // "All things should be perfectly balanced" - Nick Falsitta
    private void OnEnable()
    {
        // += :| wow.
        PlayerEvents.OnWeaponEquipped += OnWeaponEqupped;
    }

    private void OnDisable()
    {
        {
            PlayerEvents.OnWeaponEquipped -= OnWeaponEqupped;
        }
    }

    /// <summary>
    /// Keep same signature as that event
    /// </summary>
    /// <param name="_weaponComponent"></param>
    void OnWeaponEqupped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }
        else
        {
            TMP_weaponName.text = weaponComponent.weaponStats.weaponName;
            TMP_currentBullets.text = weaponComponent.weaponStats.bulletsInClip.ToString();
            TMP_totalBullets.text = weaponComponent.weaponStats.totalBullets.ToString();
        }
    }
}
