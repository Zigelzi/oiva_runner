using TMPro;
using UnityEngine;

public class ThrowDisplaySpawner : MonoBehaviour
{
    [SerializeField] private TMP_Text _throwDistancePrefab;

    private void OnEnable()
    {
        Scooter.onScooterDestroy += SpawnThrowDistanceText;
    }

    private void OnDisable()
    {
        Scooter.onScooterDestroy -= SpawnThrowDistanceText;
    }

    private void SpawnThrowDistanceText(int distance)
    {
        if (!_throwDistancePrefab) return;

        TMP_Text distanceInstance = Instantiate(_throwDistancePrefab, gameObject.transform);
        distanceInstance.text = $"+ {distance} m";
        Destroy(distanceInstance.gameObject, 1f);
    }
}
