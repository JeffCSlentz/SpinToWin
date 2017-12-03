using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGenerator : MonoBehaviour {
    public GameObject itemIconBase;
    public GameObject bullet;

    //TESTING FIND BETTER WAY
    public Sprite[] possibleTurretSprites;

    public void GenerateItem(int level)
    {
        //Silly fun FIND BETTER WAY
        List<string> savageModifiers = new List<string>
        {
            "Destruction",
            "Annihilation",
            "Carnage",
            "Ruin",
            "Slaughter",
            "Eradication",
            "Fun"
        };

        //Make an empty gameIcon
        //THIS WILL LITTER THE SCENE IF NOT CAREFUL
        GameObject itemIcon = Instantiate(itemIconBase);
        itemIcon.GetComponent<Image>().sprite = possibleTurretSprites[Random.Range(0, possibleTurretSprites.Length)];

        //Make an empty item script and add it to the empty gameicon.
        Item emptyItem = itemIcon.AddComponent(typeof(Item)) as Item;
        itemIcon.AddComponent(typeof(ItemUIHandler)); //Add the item handler.


        //  ~~~~~~~~~~~~~~~~~~~~~
        //  BUILDING THE OBJECT ~
        //  ~~~~~~~~~~~~~~~~~~~~~

        //Set the name
        emptyItem.itemName = "Turret of " + savageModifiers[Random.Range(0, savageModifiers.Count)];

        //Add ammo
        emptyItem.bullet = bullet;

        //Create a new damage attribute.
        DamageItemAttribute damage = ScriptableObject.CreateInstance<DamageItemAttribute>();
        damage.Initialize(1, 0);
        emptyItem.attributes.Add(damage);

        //Create a new health attribute.
        HealthItemAttribute health = ScriptableObject.CreateInstance<HealthItemAttribute>();
        health.Initialize(1, 0);
        emptyItem.attributes.Add(health);


        //  ~~~~~~~~~~~~~~~~~~
        //  ADD TO INVENTORY ~
        //  ~~~~~~~~~~~~~~~~~~

        //BAD PRACTICE, ADDING IT DIRECTLY TO INVENTORY
        GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
        InventoryManager inventoryManager = inventory.GetComponent<InventoryManager>();

        inventoryManager.Add(itemIcon);
    }
}