using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject enemyPrefab;
    public Vector2 spawnInterval = new Vector2(1, 10);
    public float spawnDistance = 15f;

    private float currentSpawnInterval;
    private float spawnTimer;

    List<GameObject> enemies = new List<GameObject>();

    public GameObject Player { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > currentSpawnInterval)
        {
            // Spawn an enemy
            SpawnEnemyAtRandomPos();
            currentSpawnInterval = Random.Range(spawnInterval.x, spawnInterval.y);
            spawnTimer = 0;
        }
    }

    public void SpawnEnemyAtRandomPos()
    {
        SpawnEnemyAt(Random.insideUnitCircle.normalized * spawnDistance);       
    }

    public void SpawnEnemyAt(Vector2 position)
    {
        enemies.Add(Instantiate(enemyPrefab, position, Quaternion.identity));
        ClearEnemyNullValues();
    }

    public void ClearEnemyNullValues()
    {
        enemies.RemoveAll(x => x == null);
    }

    public GameObject GetEnemyClosestToPosition(Vector2 pos)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }

            float distance = Vector2.Distance(enemy.transform.position, pos);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;

    }
}
