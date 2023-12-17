using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _levelPrefabs;
    [SerializeField] private int _numberOfLevels = 3;
    [SerializeField] private Scooter _scooterPrefab;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(25, 0, 5);

    private void Awake()
    {
        if (_levelPrefabs.Count <= 0) return;
        SpawnLevel(_numberOfLevels);
    }

    private void OnEnable()
    {
        LevelMover.onLevelDestroy += SpawnAdditionalLevels;
    }

    private void OnDisable()
    {
        LevelMover.onLevelDestroy -= SpawnAdditionalLevels;
    }

    private void SpawnAdditionalLevels()
    {
        SpawnLevel(1, 100);
    }

    private void SpawnLevel(int spawnCount, int additionalXOffset = 0)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int spawnIndex = Random.Range(0, _levelPrefabs.Count);
            Vector3 spawnPosition = new Vector3(
                transform.position.x + (_spawnOffset.x * i) + additionalXOffset,
                transform.position.y + _spawnOffset.y,
                _spawnOffset.z);
            Transform instantiatedLevel = Instantiate(_levelPrefabs[spawnIndex], spawnPosition, Quaternion.identity, transform);
            SpawnScooter(instantiatedLevel);
        }
    }

    private void SpawnScooter(Transform parent)
    {
        if (!_scooterPrefab) return;

        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(60f, 120f), 0);
        Vector3 spawnPosition = new Vector3(parent.position.x, parent.position.y, parent.position.z - 5f);
        Instantiate(_scooterPrefab, spawnPosition, spawnRotation, parent);

    }
}
