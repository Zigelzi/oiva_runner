using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private float _maxThrowForce = 20f;
    [SerializeField] private float _throwForceSelectionSpeed = 1f;
    [SerializeField] private Transform _carryingPosition;

    private Scooter _currentScooter;
    private Coroutine _currentThrowForceRoutine;
    private float _currentThrowForce = 0f;

    public Scooter CurrentScooter { get { return _currentScooter; } }
    public float CurrentThrowForcePercentage { get { return _currentThrowForce / _maxThrowForce; } }
    public UnityEvent onScooterPickup;
    public UnityEvent onScooterThrow;

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentScooter != null) return;

        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter newScooter))
        {
            _currentScooter = newScooter;
            newScooter.Follow(_carryingPosition);
            StartVaryingThrowforce();
        }
    }

    public void Throw()
    {
        if (_currentScooter == null) return;
        Vector3 direction = new Vector3(Random.Range(.5f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
        _currentScooter.Throw(direction, _currentThrowForce, gameObject.transform);
        _currentScooter = null;
        StopVaryingThrowForce();
        onScooterThrow?.Invoke();
    }

    private void StartVaryingThrowforce()
    {
        _currentThrowForceRoutine = StartCoroutine(VaryThrowForce());
    }

    private void StopVaryingThrowForce()
    {
        StopCoroutine(_currentThrowForceRoutine);
        _currentThrowForce = 0;
    }

    private IEnumerator VaryThrowForce()
    {
        while (true)
        {
            while (_currentThrowForce < _maxThrowForce)
            {
                _currentThrowForce += Mathf.Lerp(0, _maxThrowForce, _throwForceSelectionSpeed * Time.deltaTime);
                yield return null;
            }
            while (_currentThrowForce > 0)
            {
                _currentThrowForce -= Mathf.Lerp(0, _maxThrowForce, _throwForceSelectionSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
