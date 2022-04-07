///----------------------------------------------------------------------------------
///   Endless Horde Zombie Shooter
///   PlayerController.cs
///   Author            : Vineet Kumar
///   Last Modified     : 2022/01/19
///   Description       : Player Controller bools for checking movement and action
///   Revision History  : 1st ed. Set jump, run, fire and reload bools to refer
///----------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement
    public bool isJumping;
    public bool isRunning;

    // Combat
    public bool isFiring;
    public bool isReloading;
    public bool isAiming;

    // Inventory
    public InventoryComponent Inventory;

    public void OnInventory(InputValue value)
    {
        Debug.Log("Inventory Button Pressed");
        Inventory.gameObject.SetActive(!Inventory.gameObject.activeSelf);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    
    //}
    //
    //// Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
