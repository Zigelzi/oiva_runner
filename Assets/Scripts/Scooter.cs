using UnityEngine;

public class Scooter : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 10f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 bounceDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
            _rb.AddForce(bounceDirection * _bounceForce, ForceMode.Impulse);
        }
    }
}
