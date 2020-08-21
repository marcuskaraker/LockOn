using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool hasStartedGame;

    public GameObject[] enemyPrefabs;
    public Vector2 spawnInterval = new Vector2(1, 10);

    public int score;

    public float newSpawnPointInterval = 30;
    public float newSpawnRateMultiplier = 0.5f;
    public List<Transform> spawnPoints;
    private int currentSpawnPointIndex;

    private float currentSpawnInterval;
    private float spawnTimer;

    private List<GameObject> enemies = new List<GameObject>();

    public Camera MainCamera { get; private set; }
    public GameObject Player { get; private set; }
    public Destructible PlayerDestructible { get; private set; }
    public bool IsGameOver { get; private set; }
    public int HighScore { get; private set; }

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
        PlayerDestructible = Player.GetComponent<Destructible>();
        MainCamera = Camera.main;

        PlayerDestructible.onDeath.AddListener(delegate { GameOver(); });
        PlayerDestructible.onHurt.AddListener(delegate { HitShake(); });

        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        if (!hasStartedGame)
        {
            Destroy(Player);
        }
    }

    private void Update()
    {
        UpdateEnemySpawner();
        UpdateSpawnPoints();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerDestructible.Hurt(1000f);
        }
    }

    private void UpdateSpawnPoints()
    {
        if (currentSpawnPointIndex + 1 >= spawnPoints.Count)
        {
            return;
        }

        // Enable a new spawnpoint.
        if (Time.time > newSpawnPointInterval * (currentSpawnPointIndex + 1))
        {
            currentSpawnPointIndex++;
            spawnInterval *= newSpawnRateMultiplier;
            spawnPoints[currentSpawnPointIndex].gameObject.SetActive(true);
        }
    }

    private void UpdateEnemySpawner()
    {
        if (IsGameOver)
        {
            return;
        }

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
        SpawnEnemyAt(spawnPoints[Random.Range(0, currentSpawnPointIndex + 1)].position);       
    }

    public void SpawnEnemyAt(Vector2 position)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], position, Quaternion.identity);
        enemies.Add(spawnedEnemy);

        Destructible enemyDestructible = spawnedEnemy.GetComponent<Destructible>();
        enemyDestructible.onDeath.AddListener(delegate { RemoveEnemyFromData(spawnedEnemy); });
        enemyDestructible.onDeath.AddListener(delegate { AddScore(); });
        enemyDestructible.onHurt.AddListener(delegate { HitShake(); });
    }

    public void ClearEnemyNullValues()
    {
        enemies.RemoveAll(x => x == null);
    }

    public GameObject GetEnemyClosestToPosition(Vector2 pos, GameObject exclude = null)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null || enemy == exclude)
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

    public void RemoveEnemyFromData(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public GameObject GetEnemyByIndex(int index)
    {
        return enemies[index]; 
    }

    public void HitShake()
    {
        EZCameraShake.CameraShaker.Instance.ShakeOnce(4, 4, 0f, 0.2f);
    }

    public void AddScore()
    {
        score++;
    }

    public void GameOver()
    {
        IsGameOver = true;

        if (score > HighScore)
        {
            HighScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        hasStartedGame = true;
        Restart();
    }
}
