using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int _scoreAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Scooter>(out Scooter collidedScooter))
        {
            Debug.Log($"Scored {_scoreAmount} points!");
            Destroy(gameObject);

        }
    }
}
