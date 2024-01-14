using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private Vector3 _boostDirection = new Vector3(.5f, 1f, 0f);
    [SerializeField] private float _boostStrength = 10f;
    [SerializeField] private Powerup _powerupPrefab;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            if (!collidedScooter.IsThrown) return;

            collidedScooter.Boost(_boostDirection, _boostStrength);
            if (_powerupPrefab)
            {
                Instantiate(_powerupPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
