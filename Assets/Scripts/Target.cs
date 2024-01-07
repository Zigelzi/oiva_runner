using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int _scoreAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            Rigidbody scooterRb = collidedScooter.GetComponent<Rigidbody>();
            Vector3 boostDirection = new Vector3(Random.Range(10, 50), Random.Range(1, 10), 0);
            scooterRb.AddForce(boostDirection, ForceMode.Impulse);
            Destroy(gameObject);

        }
    }
}
