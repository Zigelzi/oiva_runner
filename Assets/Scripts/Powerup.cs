using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _boostDuration = 3f;
    [SerializeField] private float _boostStrenght = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Throwing>(out Throwing throwing))
        {
            throwing.StartThrowBoost(_boostDuration, _boostStrenght);
            Destroy(gameObject);
        }
    }
}
