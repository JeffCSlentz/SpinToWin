using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Text playerUIHealthText;
    public Text usefulInfo;
    public GameObject tooltipPanel;
    public GameObject inventory;
    public GameObject weapon; //TESTING DONT NEED LATER
    private GameObject player;

    public List<RaycastResult> hoveredObjects; //test

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        UpdateUsefulInfo();

    }
    private void UpdateUsefulInfo()
    {
        usefulInfo.text = "";
        String myString = "";

        myString += "DIRVELOCITY = " + player.GetComponent<playerPhysics>().GetComponent<Rigidbody2D>().velocity.ToString() + "\n";
        myString += "VELOCITY = " + player.GetComponent<playerPhysics>().GetComponent<Rigidbody2D>().velocity.magnitude + "\n";
        myString += "INPUT =    " + player.GetComponent<playerPhysics>().GetInput() + "\n";
        myString += "FPS =      " + (1.0f / Time.deltaTime) + "\n";

        if (weapon != null)
        {
            myString += "sprite size = " + "noway"  + "\n";
            myString += "weapon rotation = " + "wah" + "\n";
            myString += "end of gun = " + "ruf" + "\n";
        }

        if (hoveredObjects != null)
        {
            foreach (var item in hoveredObjects)
            {
                myString += item.gameObject.name + "\n";
            }
        }

        usefulInfo.text = myString;
    }

    public void ToggleInventory()
    {
        if (inventory.activeSelf)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
        inventory.SetActive(!inventory.activeSelf);
    }

    public void DisplayTooltip(Item item, bool setActive)
    {
        if (setActive)
        {
            tooltipPanel.SetActive(true);

            //Turn on the tooltip
            tooltipPanel.gameObject.SetActive(true);

            string generatedTooltipText = "";

            //Build the tooltip text
            generatedTooltipText += item.itemName + "\n";

            foreach (var attribute in item.attributes)
            {
                generatedTooltipText += attribute.GetStringRepresentation();
            }

            //Set the tooltip text
            tooltipPanel.GetComponentInChildren<Text>().text = generatedTooltipText;
        } else
        {
            tooltipPanel.SetActive(false);
        }
        
    }


    public void UpdateHealth(int amount)
    {
        playerUIHealthText.text = amount.ToString();
    }
}
