using UnityEngine;

public class Scooter : MonoBehaviour
{
    [SerializeField] private float _despawnDuration = 2f; // Seconds.

    private Rigidbody _rb;
    private Transform _playerFollowTransform;
    Vector3 _playerPositionOnThrow;

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

    public void Throw(Vector3 direction, float forceAmount)
    {
        _playerFollowTransform = null;
        _rb.isKinematic = false;
        _playerPositionOnThrow = GameObject.FindGameObjectWithTag("Player").transform.position;
        _rb.AddForce(direction * forceAmount, ForceMode.Impulse);
        Invoke("DestroyAfterFlying", _despawnDuration);
    }

    private void DestroyAfterFlying()
    {
        float throwDistance = Vector3.Distance(_playerPositionOnThrow, transform.position);
        Destroy(gameObject);
    }

}
