using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            if (!collidedScooter.IsThrown) return;

            Vector3 boostDirection = new Vector3(1, 2, 0);
            collidedScooter.Throw(boostDirection, 80f);
            collidedScooter.RestartDesruction();
            Destroy(gameObject);
        }
    }
}
