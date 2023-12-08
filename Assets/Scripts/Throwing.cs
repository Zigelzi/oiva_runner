using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private Transform _carryingPosition;
    [SerializeField] private float _maxThrowForce = 20f;
    [SerializeField] private float _timeToThrow = 2f;
    [SerializeField] private float _throwThresholdTime = .5f;
    [SerializeField] private float _throwForceIncrement = 2f;

    private Scooter _currentScooter;
    private int _currentThrowDistance = 0;
    private float _currentThrowForce = 0f;
    private Coroutine _currentThrowTimer;
    [SerializeField] private float _currentTimeToThrow = -1f;
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
        _goal = FindAnyObjectByType<Goal>();
        _playerStatus = GetComponent<Status>();

        _currentTimeToThrow = _throwThresholdTime;
    }
    private void OnEnable()
    {
        _goal.onGoalReach.AddListener(Disable);
        _playerStatus.onObstacleHit.AddListener(Disable);
        Scooter.onScooterDestroy += UpdateThrowDistance;
    }

    private void Update()
    {
        if (_interactionCount > 0)
        {
            _currentTimeToThrow -= Time.deltaTime;
        }
        if (_currentTimeToThrow <= 0)
        {
            Throw();

            if (_currentThrowTimer == null) return;
            StopCoroutine(_currentThrowTimer);
        }
    }

    private void OnDisable()
    {
        _goal.onGoalReach.RemoveListener(Disable);
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
            _currentThrowTimer = StartCoroutine(StartThrowTimer());
        }
    }

    public void Interact()
    {
        if (!_currentScooter) return;
        if (!enabled) return;

        if (_interactionCount > 0)
        {
            _currentTimeToThrow = _throwThresholdTime;
        }
        _interactionCount += 1;
        _currentThrowForce += _throwForceIncrement;
        _currentThrowForce = Mathf.Min(_currentThrowForce, _maxThrowForce);
    }

    public void Throw()
    {
        if (_currentScooter == null) return;
        if (!enabled) return;

        float yDirection = Random.Range(1f, 2f);
        Vector3 throwDirection = new Vector3(1, yDirection, 0);
        _currentScooter.Throw(throwDirection, _currentThrowForce);
        _currentScooter = null;
        _currentThrowForce = 0f;
        _interactionCount = 0;
        _currentTimeToThrow = _throwThresholdTime;
        onScooterThrow?.Invoke();
    }

    private IEnumerator StartThrowTimer()
    {
        yield return new WaitForSeconds(_timeToThrow);
        Throw();
    }

    private void Disable()
    {
        StopAllCoroutines();
    }

    private void UpdateThrowDistance(int additionalDistance)
    {
        _currentThrowDistance += additionalDistance;

    }
}
