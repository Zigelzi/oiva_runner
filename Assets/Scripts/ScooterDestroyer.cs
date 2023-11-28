using UnityEngine;

public class ScooterDestroyer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            if (!collidedScooter.IsThrown) return;
            Destroy(collidedScooter);
        }
    }
}
