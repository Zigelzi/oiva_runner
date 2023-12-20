using System.Collections.Generic;
using UnityEngine;

public class StreetSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _streetPrefabs;
    [SerializeField] private int _initialNumberOfStreets = 3;
    [SerializeField] private Scooter _scooterPrefab;
    [SerializeField] private Transform _scooterSpawn;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(25, 0, 5);

    private void Awake()
    {
        if (_streetPrefabs.Count <= 0) return;
        SpawnStreet(_initialNumberOfStreets);
    }

    private void OnEnable()
    {
        Street.onStreetDestroy += SpawnAdditionalLevels;
    }

    private void OnDisable()
    {
        Street.onStreetDestroy -= SpawnAdditionalLevels;
    }

    private void SpawnAdditionalLevels()
    {
        SpawnStreet(1, 100);
    }

    private void SpawnStreet(int spawnCount, int additionalXOffset = 0)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int spawnIndex = Random.Range(0, _streetPrefabs.Count);
            Vector3 spawnPosition = new Vector3(
                transform.position.x + (_spawnOffset.x * i) + additionalXOffset,
                transform.position.y + _spawnOffset.y,
                _spawnOffset.z);
            Transform instantiatedStreet = Instantiate(_streetPrefabs[spawnIndex], spawnPosition, Quaternion.identity, transform);
            SpawnScooter(instantiatedStreet);
        }
    }

    private void SpawnScooter(Transform parent)
    {
        if (!_scooterPrefab) return;
        if (!_scooterSpawn) return;

        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(60f, 120f), 0);
        Vector3 spawnPosition = new Vector3(parent.position.x, parent.position.y, parent.position.z - 5f);
        Instantiate(_scooterPrefab, spawnPosition, spawnRotation, _scooterSpawn);

    }
}
