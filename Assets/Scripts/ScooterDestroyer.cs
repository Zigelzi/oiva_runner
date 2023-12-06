using UnityEngine;

public class ScooterDestroyer : MonoBehaviour
{
    [SerializeField] private float _destroyDelay = 2f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            if (!collidedScooter.IsThrown) return;
            Destroy(collidedScooter.gameObject, _destroyDelay);
        }
    }
}
