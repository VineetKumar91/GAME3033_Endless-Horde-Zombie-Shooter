///----------------------------------------------------------------------------------
///   Endless Horde Zombie Shooter
///   PlayerController.cs
///   Author            : Vineet Kumar
///   Last Modified     : 2022/01/19
///   Description       : Player Controller bools for checking movement and action
///   Revision History  : 1st ed. Set jump, run, fire and reload bools to refer
///----------------------------------------------------------------------------------

using System;
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
    public bool inInventory;
    public InventoryComponent Inventory;
    public GameUIController UIController;

    private void Awake()
    {
        if (Inventory == null)
        {
            Inventory = GetComponent<InventoryComponent>();
        }

        if (UIController == null)
        {
            UIController = FindObjectOfType<GameUIController>();
        }
    }

    public void OnInventory(InputValue value)
    {
        if (inInventory)
        {
            inInventory = false;
        }
        else
        {
            inInventory = true;
        }

        OpenInventory(inInventory);
    }

    private void OpenInventory(bool open)
    {
        if (open)
        {
            UIController.EnableInventoryMenu();
        }
        else
        {
            UIController.EnableGameMenu();
        }
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
