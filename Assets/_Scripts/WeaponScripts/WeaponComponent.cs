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

[System.Serializable]
public struct WeaponStats
{

}


public class WeaponComponent : MonoBehaviour
{

    public Transform gripLocation;

    protected WeaponHolder weaponHolder;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
