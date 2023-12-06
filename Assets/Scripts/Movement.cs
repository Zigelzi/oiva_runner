using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;

    private int _currentDistanceTravelled = 0;
    private Goal _goal;
    private Vector3 _startingPosition;

    public int CurrentDistanceTravelled { get { return _currentDistanceTravelled; } }
    private void Awake()
    {
        _startingPosition = transform.position;
        _goal = FindAnyObjectByType<Goal>();
    }

    private void OnEnable()
    {
        _goal.onGoalReach.AddListener(Disable);
    }

    private void OnDisable()
    {
        _goal.onGoalReach.RemoveListener(Disable);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _movementSpeed * Time.deltaTime);
        GetTravelDistance();
    }

    private void GetTravelDistance()
    {
        _currentDistanceTravelled = (int)Vector3.Distance(_startingPosition, transform.position);

    }

    private void Disable()
    {
        enabled = false;
    }
}
