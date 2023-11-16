using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _travelDistance;
    [SerializeField] private TMP_Text _scooterThrowDistance;

    private Movement _movement;
    private int _currentThrowDistance = 0;

    private void Awake()
    {
        _movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        _currentThrowDistance = 0;
        InitialiseScores();
    }

    private void OnEnable()
    {
        _movement.onTravelDistanceChanged.AddListener(UpdateTravelDistance);
        Scooter.onScooterDestroy += UpdateThrowDistance;
    }

    private void OnDisable()
    {
        _movement.onTravelDistanceChanged.RemoveListener(UpdateTravelDistance);
        Scooter.onScooterDestroy -= UpdateThrowDistance;
    }

    private void InitialiseScores()
    {
        if (_travelDistance)
        {
            _travelDistance.text = "0 m";
        }
        if (_scooterThrowDistance)
        {
            _scooterThrowDistance.text = _currentThrowDistance.ToString();
        }
    }

    private void UpdateTravelDistance(int newDistance)
    {
        _travelDistance.text = newDistance.ToString() + " m";
    }

    private void UpdateThrowDistance(int additionalDistance)
    {
        _currentThrowDistance += additionalDistance;

        if (_scooterThrowDistance)
        {
            _scooterThrowDistance.text = _currentThrowDistance.ToString();
        }
    }

}
