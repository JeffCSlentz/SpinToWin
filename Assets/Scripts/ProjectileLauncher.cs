using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {
    public bool isFiring;
    public int shootingForce = 30;
    public float roundsPerSecond = 5f;
    public bool tracksEnemies = true;
    public float fieldOfView = 90f;
    public float rotationSpeed = 100000f;
    private GameObject hardPoint;
    private GameController gameController;
    private bool hasEnemyTargeted = false;
    private GameObject targetEnemy;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        hardPoint = gameObject.transform.parent.gameObject;
    }

    private void Update()
    {
        //starting to think this would be better with a mesh/cone collider BUT OH WELL
        if (targetEnemy == null)
        {
            hasEnemyTargeted = false;
        }
        if(isFiring && tracksEnemies && !hasEnemyTargeted)
        {
            //Finding the closest enemy to the left side of the field of view.
            float highestAllowableAngleFound = -360f;
            foreach (GameObject enemy in gameController.enemies)
            {
                if(enemy != null)
                {
                    //Returns positive if enemy is in the left side of line of sight.
                    float angleBetween = MathHelper.AngleBetween(hardPoint, enemy);

                    if (Mathf.Abs(angleBetween) < (fieldOfView / 2) && angleBetween > highestAllowableAngleFound)
                    {
                        Debug.Log("Found new enemy at " + angleBetween + " degrees");
                        targetEnemy = enemy;
                        highestAllowableAngleFound = angleBetween;
                        hasEnemyTargeted = true;
                    }
                }
            }

            if (hasEnemyTargeted == false)
            {
                ResetPosition();
            }
        }

        if(isFiring && tracksEnemies && hasEnemyTargeted)
        {
            
            if (Mathf.Abs(MathHelper.AngleBetween(hardPoint, targetEnemy)) < (fieldOfView / 2))
            { 
                Debug.Log("enemy is still in field of view");
                //Direction to look.
                LookAtEnemy();
                
                //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.time);
                //transform.LookAt(targetEnemy.transform, Vector3.back);
            }
            else
            {
                targetEnemy = null;
                hasEnemyTargeted = false;
                ResetPosition();
            }
            
        }
    }

    

    public void StartFiring()
    {
        isFiring = true;
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while (isFiring)
        {
            Fire();
            yield return new WaitForSeconds(1/roundsPerSecond);
        }
        
    }

    private void Fire()
    {
        gameObject.GetComponent<Transform>().localPosition = Vector3.up * gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Vector3 endOfGun = gameObject.GetComponent<Transform>().position;
        gameObject.GetComponent<Transform>().localPosition = Vector3.zero;

        GameObject spawnedBullet = Instantiate(gameObject.GetComponent<ItemSlotReference>().item.bullet, endOfGun, gameObject.GetComponent<Transform>().rotation);
        spawnedBullet.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Transform>().up * shootingForce);
    }

    private void LookAtEnemy()
    {
        Debug.Log("looking at enemy");
        Vector3 direction = targetEnemy.transform.position - transform.position;
        
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.back);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 60f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 1);
    }

    private void ResetPosition()
    {
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, 60f);
        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, rotationSpeed * Time.deltaTime);
        //transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, rotationSpeed * Time.deltaTime);
    }
}
