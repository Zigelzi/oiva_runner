using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _travelDistance;
    [SerializeField] private TMP_Text _scooterThrowDistance;

    private void Awake()
    {
        InitialiseScores();
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
