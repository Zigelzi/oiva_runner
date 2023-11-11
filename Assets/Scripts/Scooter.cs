using UnityEngine;

public class Scooter : MonoBehaviour
{


    private Rigidbody _rb;
    private Transform _playerFollowTransform;

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
        _rb.AddForce(direction * forceAmount, ForceMode.Impulse);
    }

}
