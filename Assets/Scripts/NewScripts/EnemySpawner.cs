using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private int maxEnemies = 5;

    private List<GameObject> _enemies = new List<GameObject>();

    private void Update()
    {
        // Check if we need to spawn a new enemy
        if (_enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }

        // Remove any dead enemies
        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            if (_enemies[i] == null)
            {
                _enemies.RemoveAt(i);
            }
        }
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPosition = Random.insideUnitCircle * spawnRadius;
        spawnPosition += characterTransform.position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        _enemies.Add(enemy);
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
