using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private float _maxThrowForce = 20f;
    [SerializeField] private float _throwSelectionSpeed = 1f;
    [SerializeField] private Transform _carryingPosition;

    private Scooter _currentScooter;
    private Vector3 _currentThrowDirection = Vector3.right;
    private Coroutine _currentThrowForceRoutine;
    private Coroutine _currentThrowDirectionRoutine;
    private int _currentThrowDistance = 0;
    private float _currentThrowForce = 0f;
    private Energy _energy;
    private bool _isSelectingThrowDirection = false;

    public Transform CarryingPosition { get { return _carryingPosition; } }
    public Scooter CurrentScooter { get { return _currentScooter; } }
    public Vector3 CurrentThrowDirection { get { return _currentThrowDirection; } }
    public float CurrentThrowForcePercentage { get { return _currentThrowForce / _maxThrowForce; } }
    public int CurrentThrowDistance { get { return _currentThrowDistance; } }
    public bool IsSelectingThrowDirection { get { return _isSelectingThrowDirection; } }

    public UnityEvent onScooterPickup;
    public UnityEvent onScooterThrow;

    private void Awake()
    {
        _energy = GetComponent<Energy>();
        _isSelectingThrowDirection = false;
    }
    private void OnEnable()
    {
        _energy.onEnergyDepleted.AddListener(Disable);
        Scooter.onScooterDestroy += UpdateThrowDistance;
    }

    private void OnDisable()
    {
        _energy.onEnergyDepleted.RemoveListener(Disable);
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
            StartVaryingThrowforce();
        }
    }

    public void Interact()
    {
        if (!_currentScooter) return;
        if (!enabled) return;

        if (!_isSelectingThrowDirection)
        {
            StopVaryingThrowForce();
            StartVaryingThrowDirection();
        }
        else
        {
            Throw();
            _currentThrowForce = 0;
        }
    }

    public void Throw()
    {
        if (_currentScooter == null) return;
        if (!enabled) return;

        _currentScooter.Throw(_currentThrowDirection, _currentThrowForce, gameObject.transform);
        _currentScooter = null;
        StopVaryingThrowDirection();
        onScooterThrow?.Invoke();
    }

    private void StartVaryingThrowforce()
    {
        _currentThrowForceRoutine = StartCoroutine(VaryThrowForce());
    }

    private void StopVaryingThrowForce()
    {
        StopCoroutine(_currentThrowForceRoutine);
    }

    private void StartVaryingThrowDirection()
    {
        _currentThrowDirectionRoutine = StartCoroutine(VaryThrowDirection());
        _isSelectingThrowDirection = true;
    }

    private void StopVaryingThrowDirection()
    {
        StopCoroutine(_currentThrowDirectionRoutine);
        _isSelectingThrowDirection = false;
        _currentThrowDirection = Vector3.right;
    }

    private IEnumerator VaryThrowForce()
    {
        while (true)
        {
            while (_currentThrowForce < _maxThrowForce)
            {
                _currentThrowForce += Mathf.Lerp(0, _maxThrowForce, _throwSelectionSpeed * Time.deltaTime);
                yield return null;
            }
            while (_currentThrowForce > 0)
            {
                _currentThrowForce -= Mathf.Lerp(0, _maxThrowForce, _throwSelectionSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }

    private IEnumerator VaryThrowDirection()
    {
        _currentThrowDirection = Vector3.right;
        while (true)
        {
            while (_currentThrowDirection.y <= 1)
            {
                _currentThrowDirection.y += Mathf.Lerp(0, 1, _throwSelectionSpeed * Time.deltaTime);
                yield return null;
            }
            while (_currentThrowDirection.y > 0)
            {
                _currentThrowDirection.y -= Mathf.Lerp(0, 1, _throwSelectionSpeed * Time.deltaTime);
                yield return null;
            }
        }
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
