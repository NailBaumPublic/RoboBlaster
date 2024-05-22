using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private string[] EnemyTags;
    [SerializeField] private string BossTag;
    [SerializeField] private Transform[] EnemySpawnPoints;
    [SerializeField] private float MaxSpawnDelay; 
    [SerializeField] private float CurSpawnDelay;
    [SerializeField] private float BossSpawnInterval = 40f;
    [SerializeField] private float ZOffset = 0.1f;

    private float _bossSpawnTimer;

    void Start()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);

        AdjustDifficulty();
        _bossSpawnTimer = BossSpawnInterval;
    }

    private void Update()
    {
        CurSpawnDelay += Time.deltaTime;
        _bossSpawnTimer -= Time.deltaTime;

        if (CurSpawnDelay > MaxSpawnDelay)
        {
            SpawnEnemy();
            MaxSpawnDelay = Random.Range(1f, 3f);
            CurSpawnDelay = 0;
        }

        if (_bossSpawnTimer <= 0)
        {
            SpawnBoss();
            _bossSpawnTimer = BossSpawnInterval;
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, EnemyTags.Length);
        int randomPoint = Random.Range(0, EnemySpawnPoints.Length);
        Vector3 spawnPosition = EnemySpawnPoints[randomPoint].position;
        spawnPosition.z += Random.Range(-ZOffset, ZOffset);

        GameObject spawnedEnemy = ObjectPoolManager.Instance.SpawnFromPool(EnemyTags[randomEnemy], spawnPosition, EnemySpawnPoints[randomPoint].rotation);
    }

    private void SpawnBoss()
    {
        int randomPoint = Random.Range(0, EnemySpawnPoints.Length);
        Vector3 spawnPosition = EnemySpawnPoints[randomPoint].position;
        spawnPosition.z += Random.Range(-ZOffset, ZOffset);

        GameObject spawnedBoss = ObjectPoolManager.Instance.SpawnFromPool(BossTag, spawnPosition, EnemySpawnPoints[randomPoint].rotation);
    }

    private void AdjustDifficulty()
    {
        switch (DataManager.Instance.Difficult)
        {
            case 0:
                MaxSpawnDelay = 3f;
                BossSpawnInterval = 60f;
                break;
            case 1:
                MaxSpawnDelay = 2f;
                BossSpawnInterval = 40f;
                break;
            case 2:
                MaxSpawnDelay = 1f;
                BossSpawnInterval = 20f;
                break;
        }
    }
}