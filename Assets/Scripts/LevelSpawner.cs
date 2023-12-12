using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _levelPrefabs;
    [SerializeField] private int _numberOfLevels = 3;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(25, 0, 5);

    private void Awake()
    {
        if (_levelPrefabs.Count <= 0) return;
        SpawnLevels(_numberOfLevels);
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
        SpawnLevels(1, 100);
    }

    private void SpawnLevels(int spawnCount, int additionalXOffset = 0)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            int spawnIndex = Random.Range(0, _levelPrefabs.Count);
            Vector3 spawnPosition = new Vector3(
                transform.position.x + (_spawnOffset.x * i) + additionalXOffset,
                transform.position.y + _spawnOffset.y,
                _spawnOffset.z);
            Instantiate(_levelPrefabs[spawnIndex], spawnPosition, Quaternion.identity, transform);
        }
    }
}
