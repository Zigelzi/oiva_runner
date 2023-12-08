using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            if (!collidedScooter.IsThrown) return;
            collidedScooter.Throw(Vector3.right, 80f);
            Destroy(gameObject);
        }
    }
}
