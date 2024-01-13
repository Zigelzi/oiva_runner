using UnityEngine;

public class EnvironmentDespawner : MonoBehaviour
{
    [SerializeField] private float _despawnDistance = 25f;
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
        //Debug.Log($"{gameObject.name} z distance from player: {transform.localPosition.x - _player.position.x}");
        if (transform.position.x - _player.position.x <= -_despawnDistance)
        {
            Destroy(gameObject);
        }
    }

}
