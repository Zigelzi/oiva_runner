using System.Collections;
using UnityEngine;

public class ScooterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _scooterPrefab;
    [SerializeField] private float _minSpawnTime = 3f;
    [SerializeField] private float _maxSpawnTime = 6f;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(10f, 0, 0);

    private float _spawnTimer = 0f;
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        SpawnScooter();
    }

    private void SpawnScooter()
    {
        if (!_player) return;
        if (!_scooterPrefab) return;

        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(60f, 120f), 0);
        Instantiate(_scooterPrefab, _player.position + _spawnOffset, spawnRotation, transform);
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        _spawnTimer = Random.Range(_minSpawnTime, _maxSpawnTime);
        yield return new WaitForSeconds(_spawnTimer);
        SpawnScooter();
    }

}
