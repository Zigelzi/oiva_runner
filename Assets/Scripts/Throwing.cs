using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private Transform _carryingPosition;
    [SerializeField] private float _maxThrowForce = 20f;
    [SerializeField] private float _maxTimeToInteract = .5f;
    [SerializeField] private float _throwForceIncrement = 2f;
    [SerializeField]
    private Vector3 _throwDirection = new Vector3(1, 1, 0);

    private Scooter _currentScooter;
    private int _currentThrowDistance = 0;
    private float _currentThrowForce = 0f;
    private float _currentInteractionTimeRemaining = -1f;
    private Goal _goal;
    private int _interactionCount = 0;
    private Status _playerStatus;


    public Transform CarryingPosition { get { return _carryingPosition; } }
    public Scooter CurrentScooter { get { return _currentScooter; } }
    public float CurrentThrowForcePercentage { get { return _currentThrowForce / _maxThrowForce; } }
    public int CurrentThrowDistance { get { return _currentThrowDistance; } }

    public UnityEvent onScooterPickup;
    public UnityEvent onScooterThrow;

    private void Awake()
    {
        //_goal = FindAnyObjectByType<Goal>();
        _playerStatus = GetComponent<Status>();

        _currentInteractionTimeRemaining = _maxTimeToInteract;
    }
    private void OnEnable()
    {
        //_goal.onGoalReach.AddListener(Disable);
        _playerStatus.onObstacleHit.AddListener(Disable);
        Scooter.onScooterDestroy += UpdateThrowDistance;
    }

    private void Update()
    {
        if (_interactionCount > 0)
        {
            _currentInteractionTimeRemaining -= Time.deltaTime;
        }
        if (_currentInteractionTimeRemaining <= 0)
        {
            Throw();
        }
    }

    private void OnDisable()
    {
        //_goal.onGoalReach.RemoveListener(Disable);
        _playerStatus.onObstacleHit.RemoveListener(Disable);
        Scooter.onScooterDestroy -= UpdateThrowDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentScooter != null) return;

        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter newScooter))
        {
            if (newScooter.IsThrown) return;

            _currentScooter = newScooter;
            newScooter.Follow(_carryingPosition);
        }
    }

    public void Interact()
    {
        if (!_currentScooter) return;
        if (!enabled) return;

        if (_interactionCount > 0)
        {
            _currentInteractionTimeRemaining = _maxTimeToInteract;
        }
        _interactionCount += 1;
        _currentThrowForce += _throwForceIncrement;
        _currentThrowForce = Mathf.Min(_currentThrowForce, _maxThrowForce);
    }

    public void Throw()
    {
        if (_currentScooter == null) return;
        if (!enabled) return;

        _currentScooter.Throw(_throwDirection, _currentThrowForce);
        _currentScooter = null;
        _currentThrowForce = 0f;
        _interactionCount = 0;
        _currentInteractionTimeRemaining = _maxTimeToInteract;
        onScooterThrow?.Invoke();
    }

    private void Disable()
    {
        // Add behaviour that needs to be stopped when game ends.
    }

    private void UpdateThrowDistance(int additionalDistance)
    {
        _currentThrowDistance += additionalDistance;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(_carryingPosition.position, _carryingPosition.position + _throwDirection);
    }
}
