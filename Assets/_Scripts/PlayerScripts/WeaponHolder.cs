using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    private GameObject WeaponToSpawn;


    private PlayerController playerController;

    private Sprite crosshairImage;

    [SerializeField] 
    private GameObject WeaponScoketLocation;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(WeaponToSpawn, WeaponScoketLocation.transform.position,
            WeaponScoketLocation.transform.rotation, WeaponScoketLocation.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
