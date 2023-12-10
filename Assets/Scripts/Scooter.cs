using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scooter : MonoBehaviour
{
    [SerializeField] private float _despawnDuration = 2f; // Seconds.
    [SerializeField] private float _minForce = 40f;
    [SerializeField] private Vector2 spinAmount = new Vector2(1f, 15f);

    Coroutine _currentDestructionCoroutine;
    private bool _isThrown = false;
    private Rigidbody _rb;
    private Vector3 _throwingPosition;
    private Transform _playerFollowTransform;

    public static event Action<int> onScooterDestroy;

    public bool IsThrown { get { return _isThrown; } }
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_playerFollowTransform == null) return;
        transform.position = _playerFollowTransform.position;
    }

    private void OnDestroy()
    {
        float distance = Vector3.Distance(transform.position, _throwingPosition);
        onScooterDestroy?.Invoke((int)distance);
    }

    #region Public methods
    public void Follow(Transform newPlayerFollowTransform)
    {
        _playerFollowTransform = newPlayerFollowTransform;
        _rb.isKinematic = true;
    }

    public void Throw(Vector3 direction, float forceAmount)
    {
        Vector3 spinDirection = new Vector3(Random.Range(spinAmount.x, spinAmount.y),
            Random.Range(spinAmount.x, spinAmount.y),
            Random.Range(spinAmount.x, spinAmount.y));

        _playerFollowTransform = null;
        _rb.isKinematic = false;
        _isThrown = true;
        _rb.AddForce(direction * (_minForce + forceAmount), ForceMode.Impulse);
        _rb.AddTorque(spinDirection, ForceMode.Impulse);
        _throwingPosition = transform.position;
        _currentDestructionCoroutine = StartCoroutine(DestroyAfterFlying());
    }

    public void RestartDesruction()
    {
        if (_currentDestructionCoroutine != null)
        {
            StopCoroutine(_currentDestructionCoroutine);

        }
        _currentDestructionCoroutine = StartCoroutine(DestroyAfterFlying());
    }
    #endregion

    private IEnumerator DestroyAfterFlying()
    {
        yield return new WaitForSeconds(_despawnDuration);
        Destroy(gameObject);
    }

}
