using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {
    public float acceleration = 1f;
    public float maxSpeed = 5f;
    public int health = 5;

    public int damage = 5;

    private GameController gameController;
    Rigidbody2D playerRB;
    Rigidbody2D enemyRB;
	// Use this for initialization
	void Start () {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        enemyRB = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        gameController.RegisterEnemy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherGameObject = collision.gameObject;

        if (otherGameObject.tag == "Player")
        {
            otherGameObject.GetComponent<playerPhysics>().Hurt(damage);
            Destroy(gameObject);
        }
        else if (otherGameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        gameController.RemoveEnemy(gameObject);
    }
    private void FixedUpdate()
    {
        Vector2 lookAt = playerRB.position - enemyRB.position;
        transform.up = lookAt;
        lookAt = lookAt.normalized * acceleration * Time.deltaTime;
        
        if ((lookAt + enemyRB.velocity).magnitude < maxSpeed)
        {
            enemyRB.velocity = new Vector2(enemyRB.velocity.x + lookAt.x, enemyRB.velocity.y + lookAt.y);
        }
        else
        {
            Vector2 velocityDirection = new Vector2(enemyRB.velocity.x + lookAt.x, enemyRB.velocity.y + lookAt.y).normalized;
            enemyRB.velocity = velocityDirection * Mathf.Sqrt(maxSpeed);
        }
    }

    public void TakeDamage(int amount)
    {
        health += -amount;
        if (health <= 0)
        {
            //TODO: Add death animation
            Destroy(gameObject);
        }
    }
}
