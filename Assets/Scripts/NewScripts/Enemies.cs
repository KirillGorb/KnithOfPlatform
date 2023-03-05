using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform characterTransform;
    public float spawnRadius = 10f;
    public float enemySpeed = 5f;
    public int maxEnemies = 5;

    private List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        // Check if we need to spawn a new enemy
        if (enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }

        // Remove any dead enemies
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        spawnPosition += characterTransform.position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemies.Add(enemy);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.characterTransform = characterTransform;
        enemyMovement.speed = enemySpeed;
    }
}

public class EnemyMovement : MonoBehaviour
{
    public Transform characterTransform;
    public float speed = 5f;

    void Update()
    {
        Vector3 direction = characterTransform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }
}
