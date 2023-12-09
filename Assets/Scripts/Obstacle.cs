using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            if (!collidedScooter.IsThrown) return;

            Vector3 boostDirection = new Vector3(.5f, 1f, 0f);
            collidedScooter.Throw(boostDirection, 20f);
            collidedScooter.RestartDesruction();
            Destroy(gameObject);
        }
    }
}
