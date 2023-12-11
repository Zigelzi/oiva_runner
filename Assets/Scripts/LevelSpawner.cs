using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _levelPrefabs;
    [SerializeField] private List<Transform> _currentLevels;
    [SerializeField] private int _numberOfLevels = 3;

    private void Awake()
    {
        if (_levelPrefabs.Count <= 0) return;
        for (int i = 0; i < _numberOfLevels; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x + (10 * i), transform.position.y, 5);
            Transform instantiatedLevel = Instantiate(_levelPrefabs[0], spawnPosition, Quaternion.identity, transform);
            _currentLevels.Add(instantiatedLevel);
        }
    }

    private void Update()
    {

    }
}
