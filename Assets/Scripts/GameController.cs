using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public interface IHasSwappableChild
{
    void Swap(GameObject gameObject);
}

public class GameController : MonoBehaviour
{
    public UIController uiController;
    public GameObject player;
    public GameObject weapons;
    public List<GameObject> enemies = new List<GameObject>();

    private GameObject[] hardPointsUI;
    private GameObject[] hardPoints;


    private bool isSlow = false; //test

    private void Awake()
    {
        InitializeHardPoints();
        GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
    }

    private void InitializeHardPoints()
    {
        //Initialize the hardpoints.
        int numHardPoints = player.GetComponent<PlayerStats>().numHardPoints;
        float playerRadius = player.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        player.GetComponent<HardPointAdjuster>().CreateHardPoints(numHardPoints, playerRadius);  //player's hardpoints
        weapons.GetComponent<HardPointAdjuster>().CreateHardPoints(numHardPoints, playerRadius); // UI's hardpoints

        //TODO: Load them with items (tag: desereliazation)

        hardPointsUI = GameObject.FindGameObjectsWithTag("WeaponSlot");
        hardPoints = GameObject.FindGameObjectsWithTag("HardPoint");

        Assert.IsTrue(hardPointsUI.Length == hardPointsUI.Length);

        //Give the player hardpoints a reference to the UI hardpoints.
        for (int i = 0; i < hardPointsUI.Length; i++)
        {
            hardPoints[i].GetComponentInChildren<ItemSlotReference>().itemSlot = hardPointsUI[i];
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DisplayTooltip(Item item, bool setActive)
    {
        //Debug.Log("on gamecontroller, " + item);
        uiController.DisplayTooltip(item, setActive);
    }

    public void UpdateHealth(int amount)
    {
        uiController.UpdateHealth(amount);
    }

    public void ToggleSlowness()
    {
        if (isSlow)
        {
            Time.timeScale = 1.0f;
            isSlow = false;
        }
        else
        {
            Time.timeScale = .05f;
            isSlow = true;
        }
    }

    public void RegisterEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
