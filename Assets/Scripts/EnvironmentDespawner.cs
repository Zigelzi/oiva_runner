using UnityEngine;

public class EnvironmentDespawner : MonoBehaviour
{
    [SerializeField] private const float DESPAWN_DISTANCE = 25f;
    private Transform _player;

    private bool _isMovementOverridden = false;

    public bool IsMovementOverridden
    {
        get { return _isMovementOverridden; }
        set { _isMovementOverridden = value; }
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {
        if (transform.position.x - _player.position.x <= -DESPAWN_DISTANCE)
        {
            Destroy(gameObject);
        }
    }

}
