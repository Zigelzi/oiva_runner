using System.Collections;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private GameObject _scooterPrefab;
    [SerializeField] private float _minSpawnTime = 3f;
    [SerializeField] private float _maxSpawnTime = 6f;
    [SerializeField] private int _scootersPerObstacle = 2;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(40f, 0, 0);
    [SerializeField] private float _spawnDistance = 5f;
    [SerializeField] private Vector3 _scooterMinOffset = new Vector3(10, 0, 0);

    private Obstacle _currentObstacle;
    private Vector3 _currentObstaclePosition;
    private float _spawnTimer = 0f;
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnObstacleWithScooters();
    }

    private void Update()
    {
        if (Vector3.Distance(_player.transform.position, _currentObstaclePosition) <= _spawnDistance)
        {
            SpawnObstacleWithScooters();
        }
    }

    private void SpawnObstacleWithScooters()
    {
        if (!_player) return;
        if (!_scooterPrefab) return;
        if (!_obstaclePrefab) return;

        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(60f, 120f), 0);
        _currentObstacle = Instantiate(_obstaclePrefab, _player.position + _spawnOffset, spawnRotation, transform);
        _currentObstaclePosition = _currentObstacle.transform.position;

        Scooter currentScooter = null;
        for (int i = 0; i < _scootersPerObstacle; i++)
        {
            currentScooter = SpawnScooter(currentScooter);
        }

    }

    private Scooter SpawnScooter(Scooter previousScooter)
    {
        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(60f, 120f), 0);
        GameObject instantiatedScooter = null;

        if (previousScooter == null)
        {
            Vector3 spawnPosition = _currentObstacle.transform.position - _spawnOffset + _scooterMinOffset;
            instantiatedScooter = Instantiate(_scooterPrefab, spawnPosition, spawnRotation, transform);
        }
        else
        {
            float distanceToObstacle = _currentObstacle.transform.position.x - previousScooter.transform.position.x;
            float spawnPosX = _currentObstacle.transform.position.x - distanceToObstacle / 2f;
            Vector3 spawnPosition = new Vector3(spawnPosX, previousScooter.transform.position.y, previousScooter.transform.position.z);
            instantiatedScooter = Instantiate(_scooterPrefab, spawnPosition, spawnRotation, transform);
        }

        return instantiatedScooter.GetComponent<Scooter>();
    }

    private IEnumerator SpawnTimer()
    {
        _spawnTimer = Random.Range(_minSpawnTime, _maxSpawnTime);
        yield return new WaitForSeconds(_spawnTimer);
        SpawnObstacleWithScooters();
    }

}
