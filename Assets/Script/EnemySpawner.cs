using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private int _maxEnemies = 5;
    [SerializeField] private bool _spawnOnStart = true;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(10f, 0f, 10f);
    [SerializeField] private bool _randomizePosition = true;

    [Header("Advanced Settings")]
    [SerializeField] private bool _stopWhenMaxReached = false;
    [SerializeField] private Transform _spawnParent;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();
    private Coroutine _spawnCoroutine;
    private bool _isSpawning = false;

    void Start()
    {
        if (_spawnOnStart)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
            Debug.Log("EnemySpawner : Démarrage du spawn");
        }
    }

    public void StopSpawning()
    {
        if (_isSpawning && _spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _isSpawning = false;
            Debug.Log("EnemySpawner : Arrêt du spawn");
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            CleanUpDestroyedEnemies();

            if (_spawnedEnemies.Count < _maxEnemies)
            {
                SpawnEnemy();
            }
            else if (_stopWhenMaxReached)
            {
                Debug.Log("EnemySpawner : Nombre maximum d'ennemis atteint. Arrêt du spawn.");
                _isSpawning = false;
                yield break;
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (_enemyPrefab == null)
        {
            Debug.LogError("EnemySpawner : Aucun prefab d'ennemi assigné !");
            return;
        }

        Vector3 spawnPosition = GetSpawnPosition();
        GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

        if (_spawnParent != null)
        {
            newEnemy.transform.SetParent(_spawnParent);
        }

        _spawnedEnemies.Add(newEnemy);
        Debug.Log($"EnemySpawner : Ennemi spawné à {spawnPosition}. Total : {_spawnedEnemies.Count}/{_maxEnemies}");
    }

    private Vector3 GetSpawnPosition()
    {
        if (_randomizePosition)
        {
            float randomX = Random.Range(-_spawnAreaSize.x / 2, _spawnAreaSize.x / 2);
            float randomY = Random.Range(-_spawnAreaSize.y / 2, _spawnAreaSize.y / 2);
            float randomZ = Random.Range(-_spawnAreaSize.z / 2, _spawnAreaSize.z / 2);

            return transform.position + new Vector3(randomX, randomY, randomZ);
        }
        else
        {
            return transform.position;
        }
    }

    private void CleanUpDestroyedEnemies()
    {
        _spawnedEnemies.RemoveAll(enemy => enemy == null);
    }

    public void ClearAllEnemies()
    {
        foreach (GameObject enemy in _spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        _spawnedEnemies.Clear();
        Debug.Log("EnemySpawner : Tous les ennemis ont été détruits");
    }

    public int GetActiveEnemyCount()
    {
        CleanUpDestroyedEnemies();
        return _spawnedEnemies.Count;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _spawnAreaSize);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawCube(transform.position, _spawnAreaSize);
    }
}
