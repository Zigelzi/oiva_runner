using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private Transform _carryingPosition;
    [SerializeField] private float _initialMaxThrowForce = 150f;
    [SerializeField] private float _maxTimeToInteract = .5f;
    [SerializeField] private float _initialThrowForceIncrement = 2f;
    [SerializeField]
    private Vector3 _throwDirection = new Vector3(1, 1, 0);

    private Scooter _currentScooter;
    [SerializeField] private float _currentMaxThrowForce = -1f;
    private Coroutine _currentBoostCoroutine;
    private int _currentThrowDistance = 0;
    private float _currentThrowForce = 0f;
    private float _currentInteractionTimeRemaining = -1f;
    private int _interactionCount = 0;
    private Status _playerStatus;


    public Transform CarryingPosition { get { return _carryingPosition; } }
    public Scooter CurrentScooter { get { return _currentScooter; } }
    public float CurrentThrowForcePercentage { get { return _currentThrowForce / _initialMaxThrowForce; } }
    public int CurrentThrowDistance { get { return _currentThrowDistance; } }

    public UnityEvent onScooterPickup;
    public UnityEvent onScooterThrow;

    private void Awake()
    {
        _playerStatus = GetComponent<Status>();

        _currentInteractionTimeRemaining = _maxTimeToInteract;
        _currentMaxThrowForce = _initialMaxThrowForce;
    }
    private void OnEnable()
    {
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
        _currentThrowForce += _initialThrowForceIncrement;
        _currentThrowForce = Mathf.Min(_currentThrowForce, _currentMaxThrowForce);
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

    public void StartThrowBoost(float duration, float additionalMaxForce)
    {
        if (_currentBoostCoroutine != null) StopCoroutine(_currentBoostCoroutine);

        _currentBoostCoroutine = StartCoroutine(IncreaseThrowForce(duration, additionalMaxForce));
    }

    private IEnumerator IncreaseThrowForce(float duration, float additionalMaxForce)
    {
        _currentMaxThrowForce = _initialMaxThrowForce + additionalMaxForce;

        yield return new WaitForSeconds(duration);
        _currentMaxThrowForce = _initialMaxThrowForce;
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
