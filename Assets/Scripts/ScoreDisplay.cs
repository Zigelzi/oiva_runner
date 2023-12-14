using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _travelDistance;
    [SerializeField] private TMP_Text _scooterThrowDistance;

    private Movement _movement;
    private Throwing _throwing;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _movement = player.GetComponent<Movement>();
        _throwing = player.GetComponent<Throwing>();

        InitialiseScores();
    }

    private void Update()
    {
        if (_travelDistance)
        {
            _travelDistance.text = _movement.CurrentDistanceTravelled.ToString("0");
        }
        if (_scooterThrowDistance)
        {
            _scooterThrowDistance.text = _throwing.CurrentThrowDistance.ToString();
        }

    }

    private void InitialiseScores()
    {
        if (_travelDistance)
        {
            _travelDistance.text = "0";
        }
        if (_scooterThrowDistance)
        {
            _scooterThrowDistance.text = "0";
        }
    }
}
