using UnityEngine;
using UnityEngine.Events;

public class Throwing : MonoBehaviour
{
    [SerializeField] private float _throwForce = 10f;
    [SerializeField] private Transform _carryingPosition;

    private Scooter _currentScooter;

    public UnityEvent onScooterPickup;
    public UnityEvent onScooterThrow;

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentScooter != null) return;

        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter newScooter))
        {
            _currentScooter = newScooter;
            newScooter.Follow(_carryingPosition);
            onScooterPickup?.Invoke();
        }
    }

    public void Throw()
    {
        if (_currentScooter == null) return;
        Vector3 direction = new Vector3(Random.Range(.5f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
        _currentScooter.Throw(direction, _throwForce, gameObject.transform);
        _currentScooter = null;
        onScooterThrow?.Invoke();
    }
}
