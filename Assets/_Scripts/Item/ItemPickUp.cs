using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField]
    ItemScript pickupItem;


    [Tooltip("Manual override for drop amount, if left at -1, it will use the amount from scriptable object")]
    [SerializeField]
    private int amount = -1;

    [SerializeField] 
    private MeshRenderer propMeshRenderer;

    [SerializeField] 
    private MeshFilter propMeshFilter;

    private ItemScript ItemInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void InstanstiateItem()
    {
        ItemInstance = Instantiate(pickupItem);
        if (amount > 0)
        {
            ItemInstance.SetAmount(amount);
        }

        ApplyMesh();
    }



    private void ApplyMesh()
    {
        if (propMeshFilter)
        {
            propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        }

        if (propMeshRenderer)
        {
            propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        // add to inventory here
        // get reference to player inventory, add item to it

        Destroy(gameObject);
    }
}
