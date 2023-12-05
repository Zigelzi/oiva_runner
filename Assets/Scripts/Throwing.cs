using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private Transform _carryingPosition;
    [SerializeField] private float _maxThrowForce = 20f;
    [SerializeField] private float _timeToThrow = 2f;
    [SerializeField] private float _throwForceIncrement = 2f;

    private Scooter _currentScooter;
    private int _currentThrowDistance = 0;
    private float _currentThrowForce = 0f;
    private Goal _goal;

    public Transform CarryingPosition { get { return _carryingPosition; } }
    public Scooter CurrentScooter { get { return _currentScooter; } }
    public float CurrentThrowForcePercentage { get { return _currentThrowForce / _maxThrowForce; } }
    public int CurrentThrowDistance { get { return _currentThrowDistance; } }

    public UnityEvent onScooterPickup;
    public UnityEvent onScooterThrow;

    private void Awake()
    {
        _goal = FindAnyObjectByType<Goal>();
    }
    private void OnEnable()
    {
        _goal.onGoalReach.AddListener(Disable);
        Scooter.onScooterDestroy += UpdateThrowDistance;
    }

    private void OnDisable()
    {
        _goal.onGoalReach.AddListener(Disable);
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
            StartCoroutine(StartThrowTimer());
        }
    }

    public void Interact()
    {
        if (!_currentScooter) return;
        if (!enabled) return;

        _currentThrowForce += _throwForceIncrement;
        Debug.Log(_currentThrowForce);
    }

    public void Throw()
    {
        if (_currentScooter == null) return;
        if (!enabled) return;

        float xDirection = Random.Range(.2f, 1);
        float yDirection = Random.Range(.2f, 1);
        Vector3 throwDirection = new Vector3(xDirection, yDirection, 0);
        _currentScooter.Throw(throwDirection, _currentThrowForce);
        _currentScooter = null;
        _currentThrowForce = 0f;
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
        enabled = false;
    }

    private void UpdateThrowDistance(int additionalDistance)
    {
        _currentThrowDistance += additionalDistance;

    }
}
