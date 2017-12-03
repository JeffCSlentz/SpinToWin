using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Item : MonoBehaviour {

    public enum ItemType
    {
        Gun,
        RocketLauncher,
        Lazer,
        Melee
    }
    public string itemName;
    public ItemType itemType;
    public GameObject bullet; //i dont like this but idk where else to put it.
    public List<ItemAttribute> attributes = new List<ItemAttribute>();
}
