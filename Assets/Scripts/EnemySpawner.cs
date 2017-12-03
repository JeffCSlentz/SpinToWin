using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    float secondsPerEnemy = 2f;
    bool isSpawning = true;
    public int numEnemies = 1;

    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawning());
    }

    IEnumerator Spawning()
    {
        while (isSpawning && numEnemies > 0)
        {
            SpawnEnemy();
            numEnemies--;
            yield return new WaitForSeconds(secondsPerEnemy);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnEnemy()
    {
        GameObject clone;
        clone = Instantiate(enemy, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //gameController.RegisterEnemy(clone);
    }
}