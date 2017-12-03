using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float acceleration = 1f;
    public float maxSpeed = 5f;
    public int damage = 1;
    private float timeSinceStarted;
    private GameObject currentTarget;
    private float timeSinceTargetChosen = 0;
    private Rigidbody2D bulletRB;

    // Use this for initialization
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        timeSinceStarted = Time.time;
        //FindTarget();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        /*
        if (Time.time - timeSinceTargetChosen > 1f)
        {
            FindTarget();
        }

        MoveTowardsTarget();
        */
        if (Time.time - timeSinceStarted > 5)
        {
            //TODO: Apply bullet fizzle animation
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "targetable")
        {
            otherGameObject.GetComponent<EnemyBehavior>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (otherGameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        
    }

    private void MoveTowardsTarget()
    {
        Vector2 lookAt = currentTarget.GetComponent<Rigidbody2D>().position - bulletRB.position;
        transform.up = lookAt;
        lookAt = lookAt.normalized * acceleration * Time.deltaTime;

        if ((lookAt + bulletRB.velocity).magnitude < maxSpeed)
        {
            bulletRB.velocity = new Vector2(bulletRB.velocity.x + lookAt.x, bulletRB.velocity.y + lookAt.y);
        }
        else
        {
            Vector2 velocityDirection = new Vector2(bulletRB.velocity.x + lookAt.x, bulletRB.velocity.y + lookAt.y).normalized;
            bulletRB.velocity = velocityDirection * Mathf.Sqrt(maxSpeed);
        }
    }

    private void FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("targetable");
        currentTarget = targets[0];
        float currentTargetDistance = (currentTarget.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position).magnitude;

        foreach (var target in targets)
        {
            float newDistance = (target.GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position).magnitude;
            if (newDistance < currentTargetDistance)
            {
                currentTarget = target;
            }
        }
    }
}