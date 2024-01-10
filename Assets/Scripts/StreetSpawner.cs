using System.Collections.Generic;
using UnityEngine;

public class StreetSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _streetPrefabs;
    [SerializeField] private int _initialNumberOfStreets = 3;
    [SerializeField] private GameObject[] _propPrefabs;
    [SerializeField, Range(0, 3f)] private float _propSpawnPositionVariance = 2f;
    [SerializeField] private float[] _propSpawnWeights = { 1, 2 };
    [SerializeField] private Scooter _scooterPrefab;
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private Transform _propSpawn;
    [SerializeField] private Vector3 _spawnOffset = new Vector3(25, 0, 5);

    private void Awake()
    {
        if (_streetPrefabs.Count <= 0) return;
        SpawnStreet(_initialNumberOfStreets, true);
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
        SpawnStreet(1, false);
    }



    private void SpawnStreet(int spawnCount, bool isInitial)
    {
        float additionalXOffset = isInitial ? 0 : (_initialNumberOfStreets - 1) * _spawnOffset.x;
        for (int i = 0; i < spawnCount; i++)
        {
            int spawnIndex = Random.Range(0, _streetPrefabs.Count);
            Vector3 spawnPosition = new Vector3(
                transform.position.x + (_spawnOffset.x * i) + additionalXOffset,
                transform.position.y + _spawnOffset.y,
                _spawnOffset.z);
            Transform instantiatedStreet = Instantiate(_streetPrefabs[spawnIndex], spawnPosition, Quaternion.identity, transform);

            if (_propPrefabs.Length < 1) continue;

            // Prevent spawning prop on top of player in the beginning.
            if (i == 0 && additionalXOffset == 0) continue;
            GameObject propToSpawn = _propPrefabs[ChoosePropIndexByWeights()];
            SpawnProp(instantiatedStreet, propToSpawn);

        }
    }

    private void SpawnProp(Transform parent, GameObject prefab)
    {
        if (!_propSpawn) return;

        float parentPositionOffset = 5f;
        float spawnPositionVariance = Random.Range(-_propSpawnPositionVariance, _propSpawnPositionVariance);
        float spawnZPosition = parent.position.z - parentPositionOffset + spawnPositionVariance;

        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(60f, 120f), 0);
        Vector3 spawnPosition = new Vector3(parent.position.x, parent.position.y, spawnZPosition);
        Instantiate(prefab, spawnPosition, spawnRotation, _propSpawn);

    }

    private int ChoosePropIndexByWeights()
    {
        if (_propSpawnWeights.Length < 1) return 0;

        float totalWeight = 0;

        foreach (float weight in _propSpawnWeights)
        {
            totalWeight += weight;
        }

        float point = Random.Range(0, totalWeight);

        for (int i = 0; i < _propSpawnWeights.Length; i++)
        {
            if (point < _propSpawnWeights[i])
            {
                return i;
            }
            else
            {
                point -= _propSpawnWeights[i];
            }
        }

        return _propSpawnWeights.Length - 1;
    }
}
