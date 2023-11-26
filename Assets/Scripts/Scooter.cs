using System;
using System.Collections;
using UnityEngine;

public class Scooter : MonoBehaviour
{
    [SerializeField] private float _despawnDuration = 2f; // Seconds.

    private bool _isThrown = false;
    private Rigidbody _rb;
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

    public void Follow(Transform newPlayerFollowTransform)
    {
        _playerFollowTransform = newPlayerFollowTransform;
        _rb.isKinematic = true;
    }

    public void Throw(Vector3 direction, float forceAmount, Transform player)
    {
        _playerFollowTransform = null;
        _rb.isKinematic = false;
        _isThrown = true;
        _rb.AddForce(direction * forceAmount, ForceMode.Impulse);
        StartCoroutine(DestroyAfterFlying(player));
    }

    private IEnumerator DestroyAfterFlying(Transform player)
    {
        yield return new WaitForSeconds(_despawnDuration);
        float distance = Vector3.Distance(transform.position, player.position);
        onScooterDestroy?.Invoke((int)distance);
        Destroy(gameObject);
    }

}
