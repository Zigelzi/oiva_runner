using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _travelDistance;
    [SerializeField] private TMP_Text _scooterThrowDistance;

    private Movement _movement;

    private void Awake()
    {
        _movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        InitialiseScores();
    }

    private void OnEnable()
    {
        _movement.onTravelDistanceChanged.AddListener(UpdateTravelDistance);
    }

    private void OnDisable()
    {
        _movement.onTravelDistanceChanged.RemoveListener(UpdateTravelDistance);
    }

    private void InitialiseScores()
    {
        if (_travelDistance)
        {
            _travelDistance.text = "0 m";
        }
        if (_scooterThrowDistance)
        {
            _scooterThrowDistance.text = "0";
        }
    }

    private void UpdateTravelDistance(int newDistance)
    {
        _travelDistance.text = newDistance.ToString() + " m";
    }
}
