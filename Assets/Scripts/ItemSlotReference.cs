using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotReference : MonoBehaviour {
    //Reference to the UI equipped itemSlot
    public GameObject itemSlot;
    public Item item;

    private void Start()
    {
        item = itemSlot.GetComponentInChildren<Item>();
        UpdateTurret();
    }
    private void Update()
    {
        
        if (item != itemSlot.GetComponentInChildren<Item>())
        {
            //Item has changed
            UpdateTurret();
        }
        
    }

    private void UpdateTurret()
    {
        item = itemSlot.GetComponentInChildren<Item>();
        if (item == null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = item.GetComponent<Image>().sprite;
            gameObject.GetComponent<ProjectileLauncher>().StartFiring();
        }
    }
}
