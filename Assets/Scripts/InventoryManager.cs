using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public GameObject[] inventorySlots;

    
    public void Start()
    {
        //THIS IS ASSUMING SLOTS ARE FIXED DURING START();
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        //System.Array.Reverse(inventorySlots);
    }

    //Add an itemIcon to the last open slot.
    public void Add(GameObject itemIcon)
    {
        //I think this is ok.
        foreach (var slot in inventorySlots)
        {
            if (slot.GetComponentInChildren<Item>() == null)
            {
                itemIcon.transform.SetParent(slot.GetComponent<Transform>());
                itemIcon.transform.position = slot.GetComponent<Transform>().position;
                return;
            }
        }

        //Inventory is full, notify player.
        Debug.Log("INVENTORY IS FULL");
        return;
    }
}
